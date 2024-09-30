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

                break;
            case 3:

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

        for (int i = 0; i < AttackSequence.Length; i++)
        {
            // 플레이어의 공격
            if (AttackSequence[i] == 0)
            {
                //~~
            }
            // 몬스터의 공격
            else
            {
                //~~
            }
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

}
