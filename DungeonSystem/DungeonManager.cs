using System;

namespace TeamTextRPG;

public class DungeonManager
{
    // 현재 위치한 던전 정보
    private Dungeon Dungeon_in { get; set; }

    // 현재 전투에 참여한 플레이어 정보
    private Character Player_in;

    // 현재 전투에 참여한 몬스터 정보
    private List<Monster> Monsters_spawn { get; set; }

    // 현재 던전이 끝났는지 확인하는 변수
    private bool IsDungeonOver = false;

    public DungeonManager(Dungeon cur_dungeon, Character player)
    {
        Player_in = player;
        Dungeon_in = cur_dungeon;
        Monsters_spawn = new List<Monster>();

        SpawnDungeonMonster();
    }

    public void DungeonSystem()
    {
        while (!IsDungeonOver)
        {
            DisplayInDungeonBattle();
        }
    }

    private void SpawnDungeonMonster()
    {
        Random rand = new Random();
        int spawn_num = rand.Next(1, 4);

        for (int i = 0; i < spawn_num; i++)
        {
            // 해당 던전에서 출현 가능한 몬스터 List에서 
            // spawn_num의 수만큼 현재 전투에 참여한 몬스터 List에 추가합니다. 
            int random_monster = rand.Next();
            Monsters_spawn.Add(Dungeon_in.Monsters_can_appear[random_monster]);
        }
    }

    private void DisplayInDungeonBattle()
    {
        Console.Clear();
        Console.WriteLine("Battle!!");
        Console.WriteLine();

        // 현재 던전에 존재하는 몬스터 정보 출력
        DisplayMonstersInfo(false);
        Console.WriteLine();

        // 플레이어 현재 정보 출력
        Console.WriteLine("내정보");
        Console.WriteLine($"Lv.{0} {1} ({2})", Player_in.Level, Player_in.Name, Player_in.Job);

        // 행동 선택
        Console.WriteLine();
        Console.WriteLine("1. 공격");
        Console.WriteLine("2. 스킬 사용");
        Console.WriteLine("3. 인벤토리");
        Console.WriteLine("4. 도망가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int result = CheckInput(1, 4);
        switch (result)
        {
            case 1:
                DisplayInSelectAttack();
                break;
            case 2:
                // ~
                break;
            case 3:
                // ~
                break;
            case 4:
                IsDungeonOver = true;
                break;
        }
    }

    private void DisplayInSelectAttack()
    {
        Console.Clear();
        Console.WriteLine("Battle!!");
        Console.WriteLine();

        // 현재 던전에 존재하는 몬스터 정보 출력
        DisplayMonstersInfo(true);
        Console.WriteLine();

        // 플레이어 현재 정보 출력
        Console.WriteLine("[내정보]");
        Console.WriteLine($"Lv.{0} {1} ({2})", Player_in.Level, Player_in.Name, Player_in.Job);

        // 행동 선택
        Console.WriteLine();
        Console.WriteLine("0. 취소");
        Console.WriteLine();
        Console.WriteLine("대상을 선택해주세요.");

        int result = CheckAttackInput(1, Monsters_spawn.Count);
        switch (result)
        {
            case 0:
                break;
            default:
                DisplayBattleSystem();
                break;
        }
    }

    private void DisplayBattleSystem()
    {
        Console.Clear();
        Console.WriteLine("Battle!!");
        Console.WriteLine();

        // 공격 순서 랜덤 배치
        int rouletteNum = Monsters_spawn.Count;
        int[] AttackSequence = new int[rouletteNum];
        for(int i = 0; i < rouletteNum; i++) AttackSequence[i] = i;
        AttackSequence = Shffle(AttackSequence);

        // 공격 과정 표시
        for (int i = 0; i < AttackSequence.Length; i++)
        {
            int randomIdx = AttackSequence[i];

            // 플레이어의 공격
            if (randomIdx == 0)
            {
                DisplayPlayerAttackResult(Monsters_spawn[randomIdx]);
            }
            // 몬스터의 공격
            else
            {
                DisplayMonsterAttackResult(Monsters_spawn[randomIdx]);
            }

            Console.WriteLine("");
        }

        // 결과 검사 
        // 1. 방에 존재하는 모든 몬스터의 체력이 0인가?
        // 2. 플레이어의 Hp가 0인가?

        KeyValuePair<bool, bool> battleResult = CheckBattleEnd();
        if (battleResult.Key)
        {
            DisplayBattleResult(battleResult.Value);
        }
    }

    private void DisplayPlayerAttackResult(Monster monster)
    {
        Player_in.BasicAttack(monster);
    }

    private void DisplayMonsterAttackResult(Monster monster)
    {
        monster.MonsterAttack(Player_in);
    }

    private void DisplayBattleResult(bool isWin)
    {
        Console.Clear();
        Console.WriteLine("Battle!! - Result");
        Console.WriteLine();

        if(isWin)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Victory!!");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You Lose...");
            Console.ResetColor();
        }

        // 플레이어 체력 / 경험치 등을 출력.

        // 행동 선택
        Console.WriteLine();
        Console.WriteLine("0. 다음");
        Console.WriteLine();

        int result = CheckInput(0, 0);
        switch (result)
        {
            case 0:
                IsDungeonOver = true;
                break;
        }

    }

    private void DisplayMonstersInfo(bool showIdx)
    {

        for (int i = 0; i < Monsters_spawn.Count; i++)
        {
            string selectIdx = "";
            if (showIdx) selectIdx += "(i+1).ToString() ";
            if (Monsters_spawn[i].Hp <= 0) Console.ForegroundColor = ConsoleColor.DarkGray;
            
            Console.WriteLine($"{0}Lv.{1} {2} HP {3}", selectIdx, Monsters_spawn[i].Tier, Monsters_spawn[i].Name, Monsters_spawn[i].Hp);
            Console.ResetColor();
        }

    }

    // KeyValuePair<bool, bool>()
    // - Key 현재 게임이 끝났는가?
    // - Value 플레이어가 이겼는가?
    private KeyValuePair<bool, bool> CheckBattleEnd()
    {
        // 플레이어의 체력검사
        if (Player_in.Hp <= 0) return new KeyValuePair<bool, bool>(true, false);

        // 방에 존재하는 몬스터의 체력검사
        foreach (var curMonster in Monsters_spawn)
        {
            if (curMonster.Hp > 0) return new KeyValuePair<bool, bool>(false, true);
        }
        return new KeyValuePair<bool, bool>(true, true);
    }

    private int CheckAttackInput(int min, int max)
    {
        int result;
        while (true)
        {
            string input = Console.ReadLine();
            bool isNumber = int.TryParse(input, out result);
            if (isNumber)
            {
                if (result >= min && result <= max && Monsters_spawn[result - 1].Hp > 0)
                    return result;
            }
            Console.WriteLine("잘못된 입력입니다!!!!");
        }
    }

    private static T[] Shffle<T>(T[] array)
    {
        Random random = new Random();
        int n = array.Length;
        var sortArray = new T[n];

        for (int i = 0; i < n; i++)
        {
            int last = n - 1 - i;

            var index = random.Next(0, last + 1);
            sortArray[i] = array[index];
            array[index] = array[last];
        }

        return sortArray;
    }

    private static int CheckInput(int min, int max)
    {
        int result;
        while (true)
        {
            string input = Console.ReadLine();
            bool isNumber = int.TryParse(input, out result);
            if (isNumber)
            {
                if (result >= min && result <= max)
                    return result;
            }
            Console.WriteLine("잘못된 입력입니다!!!!");
        }
    }
}
