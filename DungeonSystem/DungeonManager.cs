using System;
using System.Threading;

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

    // 처치한 몬스터의 수를 기록하는 변수
    private int countMonsterKill = 0;

    public DungeonManager(Dungeon cur_dungeon, Character player)
    {
        Player_in = player;
        Dungeon_in = cur_dungeon;

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
        // 스폰 몬스터 배열 초기화 진행
        Monsters_spawn = new List<Monster>();

        Random rand = new Random();
        int spawn_num = rand.Next(1, 4);

        for (int i = 0; i < spawn_num; i++)
        {
            // 해당 던전에서 출현 가능한 몬스터 List에서 
            // spawn_num의 수만큼 현재 전투에 참여한 몬스터 List에 추가합니다. 

            int random_monster = rand.Next(0, Dungeon_in.Monsters_can_appear.Count);

            Monsters_spawn.Add(new Monster(Dungeon_in.Monsters_can_appear[random_monster]));
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
        DisplayPlayerInfo();

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
                // AddDropItem ~
                // DisplayDropInven ~
                break;
            case 4:
                IsDungeonOver = true;
                break;
        }
    }

    private void DisplayInSelectAttack()
    {
        Console.Clear();
        Console.WriteLine("Battle!! - 공격 선택");
        Console.WriteLine();

        // 현재 던전에 존재하는 몬스터 정보 출력
        DisplayMonstersInfo(true);
        Console.WriteLine();

        // 플레이어 현재 정보 출력
        DisplayPlayerInfo();

        // 행동 선택
        Console.WriteLine();
        Console.WriteLine("0. 취소");
        Console.WriteLine();
        Console.WriteLine("대상을 선택해주세요.");

        int result = CheckAttackInput(0, Monsters_spawn.Count);
        switch (result)
        {
            case 0:
                DisplayInDungeonBattle();
                break;
            default:
                DisplayBattleSystem(result);

                // 결과 검사 
                // 1. 방에 존재하는 모든 몬스터의 체력이 0인가?
                // 2. 플레이어의 Hp가 0인가?

                KeyValuePair<bool, bool> battleResult = CheckBattleEnd();
                if (battleResult.Key)
                {
                    DisplayBattleResult(battleResult.Value);
                    return;
                }

                break;
        }
    }

    private void DisplayBattleSystem(int targetIdx)
    {
        Console.Clear();
        Console.WriteLine("Battle!!");
        Console.WriteLine();

        // 공격 순서 랜덤 배치
        int rouletteNum = Monsters_spawn.Count + 1; // 플레이어도 공격 순서에 껴있으니 포함시킨다.
        int[] AttackSequence = new int[rouletteNum];
        for(int i = 0; i < rouletteNum; i++) AttackSequence[i] = i;
        AttackSequence = Shffle(AttackSequence);

        // 공격 과정 표시
        int turnIdx = 1;
        for (int i = 0; i < AttackSequence.Length; i++)
        {
            int randomIdx = AttackSequence[i];

            // 플레이어의 공격
            if (randomIdx == 0)
            {
                Console.WriteLine($"TURN [{turnIdx}] {Player_in.Name}의 공격!!");
                DisplayPlayerAttackResult(Monsters_spawn[targetIdx - 1]);
                turnIdx++;
                Console.WriteLine();
            }
            // 몬스터의 공격
            else if(randomIdx != 0)
            {
                if(Monsters_spawn[randomIdx - 1].Hp <= 0)
                {
                    continue;
                }
                Console.WriteLine($"TURN [{turnIdx}] {Monsters_spawn[randomIdx - 1].Name}[{randomIdx}]의 공격!!");
                DisplayMonsterAttackResult(Monsters_spawn[randomIdx - 1]);
                turnIdx++;
                Console.WriteLine();
            }
        }

        // 행동 선택
        Console.WriteLine("0. 다음");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int result = CheckAttackInput(0, Monsters_spawn.Count);
        switch (result)
        {
            case 0:
                break;
            default:
                DisplayBattleSystem(result);
                break;
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
        Console.WriteLine();

        // 플레이어 체력 / 경험치 등을 출력.
        Console.WriteLine($"Lv.{Player_in.Level} {Player_in.Name} HP {Player_in.Hp}");
        Console.WriteLine();

        ///
        /// 여기에 처치한 몬스터의 종류 등을 기록하겠습니다!
        ///


        // 행동 선택
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
            if (showIdx) selectIdx += ($"[{(i + 1).ToString()}] ");
            if (Monsters_spawn[i].Hp <= 0) Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.WriteLine($"{selectIdx}Tier.{Monsters_spawn[i].Tier} {Monsters_spawn[i].Name} HP {Monsters_spawn[i].Hp}");
            Console.ResetColor();
        }
    }

    private void DisplayPlayerInfo()
    {
        Console.WriteLine("[내정보]");
        Console.WriteLine($"Lv.{Player_in.Level} {Player_in.Name} ({Player_in.Job}) HP {Player_in.Hp} / {Player_in.MaxHp}");
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
                if(result == 0) return 0;
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
