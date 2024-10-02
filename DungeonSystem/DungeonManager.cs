using System;
using System.Threading;
using TeamTextRPG.Jobs;

namespace TeamTextRPG;

public struct Player_in_before
{
    public int Level_before;
    public int Hp_before;
    public int Mp_before;
    public int Exp_before;
}

public class DungeonManager
{
    // 현재 위치한 던전 정보
    private Dungeon Dungeon_in { get; set; }

    // 현재 전투에 참여하기 전 플레이어 정보
    private Player_in_before Player_In_Dungeon_Before_Information;

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

        Player_In_Dungeon_Before_Information = new Player_in_before();
        Player_In_Dungeon_Before_Information.Level_before = Player_in.Level;
        Player_In_Dungeon_Before_Information.Hp_before = Player_in.Hp;
        Player_In_Dungeon_Before_Information.Mp_before = Player_in.Mp;
        Player_In_Dungeon_Before_Information.Exp_before = Player_in.Exp;

        SpawnDungeonMonster();
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

    public void DungeonSystem()
    {
        while (!IsDungeonOver)
        {
            DisplayInDungeonBattle();
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
        Console.WriteLine("2. 스킬");
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
                DisplayInSelectSkill();
                break;
            case 3:
                DisplayEdibleItem();
                break;
            case 4:
                IsDungeonOver = true;
                break;
        }
    }

    private void DisplayEdibleItem()
    {
        Console.Clear();
        Console.WriteLine("Inven - 사용아이템");
        Console.WriteLine();

        // 플레이어 현재 정보 출력
        DisplayPlayerInfo();
        Console.WriteLine();

        Player_in.DisplayDropInventory(false);

        Console.WriteLine();
        Console.WriteLine("0. 취소");
        Console.WriteLine("1. 아이템 사용하기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int result = CheckInput(0, Player_in.DropInventoryCount);
        switch (result)
        {
            case 0:
                DisplayInDungeonBattle();
                break;
            default:
                DisplayUseEdibleItem();
                break;
        }
    }

    private void DisplayUseEdibleItem()
    {
        Console.Clear();
        Console.WriteLine("Inven - 사용아이템 선택");
        Console.WriteLine();

        // 플레이어 현재 정보 출력
        DisplayPlayerInfo();
        Console.WriteLine();

        Player_in.DisplayDropInventory(true);

        // 행동 선택
        Console.WriteLine();
        Console.WriteLine("0. 취소");
        Console.WriteLine();
        Console.WriteLine("사용 대상을 선택해주세요.");

        int result = CheckInput(0, Player_in.DropInventoryCount);
        switch (result)
        {
            case 0:
                DisplayEdibleItem();
                break;

            default:
                Player_in.UsePotion(result - 1);
                DisplayUseEdibleItemResult();
                break;
        }
    }

    private void DisplayUseEdibleItemResult()
    {
        Console.Clear();
        Console.WriteLine("Inven - 아이템 사용 결과");
        Console.WriteLine();

        // 플레이어 현재 정보 출력
        DisplayPlayerInfo();
        Console.WriteLine();

        Player_in.DisplayDropInventory(false);

        // 행동 선택
        Console.WriteLine();
        Console.WriteLine("0. 다음");
        Console.WriteLine();

        int result = CheckInput(0, 0);
        switch (result)
        {
            case 0:
                DisplayEdibleItem();
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
                DisplayBattleSystemWithNormal(result);
                TurnEnd();

                break;
        }
    }

    private void DisplayInSelectSkill()
    {
        Console.Clear();
        Console.WriteLine("Battle!! - 스킬 선택");
        Console.WriteLine();

        // 현재 던전에 존재하는 몬스터 정보 출력
        DisplayMonstersInfo(true);
        Console.WriteLine();

        // 플레이어 현재 정보 출력
        DisplayPlayerInfo();

        // 행동 선택
        Console.WriteLine();
        DisplayPlayerSkillInfo(Player_in);

        Console.WriteLine("0. 취소");
        Console.WriteLine();
        Console.WriteLine("스킬을 선택해주세요.");
        
        int result = CheckInput(0, 4);
        string player_job = Player_in.Job;
        List<int> multiIndex;
        if (player_job.Equals("전사"))
        {
            switch(result)
            {
                case 0: DisplayInDungeonBattle(); break;
                case 1:
                    // 강력한 한방
                    multiIndex = CheckMultiInput(1, Monsters_spawn.Count, 1);
                    DisplayBattleSystemWithSkill(multiIndex);
                    TurnEnd();
                    break;
                case 2:
                    // 가드 - 일반 공격 시스템으로 진행
                    Console.WriteLine("대상을 선택해주세요.");
                    int select_index = CheckAttackInput(1, Monsters_spawn.Count);
                    Player_in.UtilitySkill();
                    DisplayBattleSystemWithNormal(select_index);
                    TurnEnd();
                    break;
                case 3:
                    // 아드레날린[패시브]
                    Console.WriteLine("패시브 스킬은 선택할 수 없습니다.");
                    Console.ReadLine();
                    DisplayInSelectSkill();
                    break;
            }
        }
        else if (player_job.Equals("궁수"))
        {
            switch (result)
            {
                case 0: DisplayInDungeonBattle(); break;
                case 1:
                    // 더블샷
                    multiIndex = CheckMultiInput(1, Monsters_spawn.Count, 1);
                    DisplayBattleSystemWithSkill(multiIndex);
                    TurnEnd();
                    break;
                case 2:
                    // 호크아이
                    Console.WriteLine("대상을 선택해주세요.");
                    int select_index = CheckAttackInput(1, Monsters_spawn.Count);
                    Player_in.UtilitySkill();
                    DisplayBattleSystemWithNormal(select_index);
                    TurnEnd();
                    break;
                case 3:
                    // 신속한 이동
                    Console.WriteLine("패시브 스킬은 선택할 수 없습니다.");
                    Console.ReadLine();
                    DisplayInSelectSkill();
                    break;
            }
        }
        else if (player_job.Equals("마법사"))
        {
            switch (result)
            {
                case 0: DisplayInDungeonBattle(); break;
                case 1:
                    // 파이어볼
                    multiIndex = CheckMultiInput(1, Monsters_spawn.Count, 1);
                    DisplayBattleSystemWithSkill(multiIndex);
                    TurnEnd();
                    break;
                case 2:
                    // 워터밤

                    // 현재 필드에 남아있는 몬스터 수 계산 
                    int aliveMonsters = 0;
                    foreach (var curMonster in Monsters_spawn)
                    {
                        if (!curMonster.IsDie) aliveMonsters++;
                    }

                    if (aliveMonsters < 2)
                    {
                        Console.WriteLine("개체 수가 2 이상이 아니어서 사용할 수 없습니다.");
                        Console.ReadLine();
                        DisplayInSelectSkill();
                        break;
                    }

                    multiIndex = CheckMultiInput(1, Monsters_spawn.Count, 2);
                    DisplayBattleSystemWithSkill(multiIndex);
                    TurnEnd();
                    break;
                case 3:
                    // 마나 재생
                    if(Player_in.IsRegenerateMp) Console.WriteLine("마나 재생은 이미 사용하셨습니다.");
                    Player_in.UtilitySkill();
                    DisplayInSelectSkill();
                    break;
            }
        }
    }
    
    // 일반 공격 배틀 시스템
    private void DisplayBattleSystemWithNormal(int targetIdx)
    {
        Console.Clear();
        Console.WriteLine("Battle!!");
        Console.WriteLine();

        // 입력 변수 선언
        int result = -1;

        // 공격 순서 랜덤 배치
        int rouletteNum = Monsters_spawn.Count + 1; // 플레이어도 공격 순서에 포함되므로 인덱스의 수를 늘린다.
        int[] AttackSequence = new int[rouletteNum];
        for(int i = 0; i < rouletteNum; i++) AttackSequence[i] = i;
        AttackSequence = Shffle(AttackSequence);

        // 공격 과정 표시
        int turnIdx = 1;
        for (int i = 0; i < AttackSequence.Length; i++)
        {
            Console.Clear();

            if (Player_in.Job.Equals("전사")) Player_in.PassiveSkill();

            int randomIdx = AttackSequence[i];

            // 플레이어의 차례
            if (randomIdx == 0)
            {
                // 플레이어의 체력이 0 인 경우 즉시 전투 시스템을 중단한다.
                if (Player_in.IsDie)
                {
                    break;
                }
                    
                Console.WriteLine($"TURN [{turnIdx}]");
                Console.WriteLine();
                DisplayPlayerAttackResult(Monsters_spawn[targetIdx - 1]);
                turnIdx++;
                Console.WriteLine();
            }

            // 몬스터의 차례
            else if(randomIdx != 0)
            {
                // 몬스터의 체력이 0 인 경우 차례가 넘어간다.
                if(Monsters_spawn[randomIdx - 1].IsDie)
                {
                    continue;
                }

                // 플레이어의 체력이 0 인 경우 즉시 전투 시스템을 중단한다.
                if (Player_in.IsDie)
                {
                    break;
                }

                Console.WriteLine($"TURN [{turnIdx}]");
                Console.WriteLine();
                DisplayMonsterAttackResult(Monsters_spawn[randomIdx - 1]);
                turnIdx++;
                Console.WriteLine();
            }

            Console.WriteLine("0. 다음");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            result = CheckInput(0, 0);
            switch (result)
            {
                case 0:
                    break;
            }
        }
    }

    // 스킬 공격 배틀 시스템
    private void DisplayBattleSystemWithSkill(List<int> skillIdx)
    {
        Console.Clear();
        Console.WriteLine("Battle!!");
        Console.WriteLine();

        // 입력 변수 선언
        int result = -1;

        // 공격 순서 랜덤 배치
        int rouletteNum = Monsters_spawn.Count + 1; // 플레이어도 공격 순서에 포함되므로 인덱스의 수를 늘린다.
        int[] AttackSequence = new int[rouletteNum];
        for(int i = 0; i < rouletteNum; i++) AttackSequence[i] = i;
        AttackSequence = Shffle(AttackSequence);

        // 공격 과정 표시
        int turnIdx = 1;
        for (int i = 0; i < AttackSequence.Length; i++)
        {
            Console.Clear();

            if (Player_in.Job.Equals("전사")) Player_in.PassiveSkill();

            int randomIdx = AttackSequence[i];

            // 플레이어의 차례
            if (randomIdx == 0)
            {
                // 플레이어의 체력이 0 인 경우 즉시 전투 시스템을 중단한다.
                if (Player_in.IsDie)
                {
                    break;
                }
                    
                Console.WriteLine($"TURN [{turnIdx}]");
                Console.WriteLine();
                DisplayPlayerSkillAttackResult(skillIdx, Monsters_spawn);
                turnIdx++;
                Console.WriteLine();
            }

            // 몬스터의 차례
            else if(randomIdx != 0)
            {
                // 몬스터의 체력이 0 인 경우 차례가 넘어간다.
                if(Monsters_spawn[randomIdx - 1].IsDie)
                {
                    continue;
                }

                // 플레이어의 체력이 0 인 경우 즉시 전투 시스템을 중단한다.
                if (Player_in.IsDie)
                {
                    break;
                }

                Console.WriteLine($"TURN [{turnIdx}]");
                Console.WriteLine();
                DisplayMonsterAttackResult(Monsters_spawn[randomIdx - 1]);
                turnIdx++;
                Console.WriteLine();
            }

            Console.WriteLine("0. 다음");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            result = CheckInput(0, 0);
            switch (result)
            {
                case 0:
                    break;
            }
        }
    }

    // 일반 공격 결과
    private void DisplayPlayerAttackResult(Monster monster)
    {
        Player_in.BasicAttack(monster);
        Player_in.UpdatePlayerExp(monster);
    }

    // 스킬 공격 결과
    private void DisplayPlayerSkillAttackResult(List<int> selectIdxs, List<Monster> monsters)
    {
        Player_in.ActiveSkill(selectIdxs, monsters);

        for(int i = 0; i < monsters.Count; i++)
        {
            Player_in.UpdatePlayerExp(Monsters_spawn[i]);
        }
    }

    private void DisplayMonsterAttackResult(Monster monster)
    {
        monster.MonsterAttack(Player_in);
    }

    // 전투 결과 출력
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
        Player_in.ResetIsRegenerateMp();

        // 던전 몬스터 처치 결과 출력
        int deadMonsterCount = 0;
        foreach(var curMonster in Monsters_spawn)
        {
            if (curMonster.IsDie) deadMonsterCount++;
        }

        Console.WriteLine($"던전에서 몬스터 {deadMonsterCount.ToString()}마리를 잡았습니다.");
        DisplayMonstersInfo(false);
        Console.WriteLine();

        // 캐릭터 정보 출력 
        DisplayPlayerBattleResult();
        Console.WriteLine();

        // 획득 아이템 출력
        Console.WriteLine();
        DisplayGetItem();
        Console.WriteLine();

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
            if (Monsters_spawn[i].IsDie)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"{selectIdx}Tier.{Monsters_spawn[i].Tier} {Monsters_spawn[i].Name} Dead");
            }
            else
            {
                Console.Write($"{selectIdx}Tier.{Monsters_spawn[i].Tier} ");
                WriteColorString(Monsters_spawn[i].Name, ConsoleColor.Green);
                Console.Write(" HP ");
                WriteColorString(Monsters_spawn[i].Hp.ToString(), ConsoleColor.Red);
            }
            Console.ResetColor();
            Console.WriteLine();
        }
    }

    private void DisplayPlayerInfo()
    {
        Console.WriteLine("[내정보]");
        Console.Write($"Lv.{Player_in.Level} ");
        WriteColorString(Player_in.Name, ConsoleColor.Cyan);
        Console.Write($" ({Player_in.Job})");
        Console.WriteLine();

        Console.Write("HP ");
        WriteColorString(Player_in.Hp.ToString(), ConsoleColor.Red);
        Console.Write(" / ");
        WriteColorString(Player_in.MaxHp.ToString(), ConsoleColor.DarkRed);
        Console.WriteLine();

        Console.Write($"MP ");
        WriteColorString(Player_in.Mp.ToString(), ConsoleColor.Blue);
        Console.Write(" / ");
        WriteColorString(Player_in.MaxMp.ToString(), ConsoleColor.DarkBlue);

        Console.WriteLine();
    }

    private void DisplayPlayerBattleResult()
    {
        Console.WriteLine("[캐릭터 정보]");
        // 레벨 변화 표시
        if(Player_In_Dungeon_Before_Information.Level_before < Player_in.Level)
        {
            Console.Write($"Lv.{Player_in.Level} ");
            WriteColorString(Player_in.Name, ConsoleColor.Cyan);
            Console.Write(" -> Lv.");
            WriteColorString(Player_in.Level.ToString(), ConsoleColor.Yellow);
        }
        else
        {
            Console.Write($"Lv.{Player_in.Level} ");
            WriteColorString(Player_in.Name, ConsoleColor.Cyan);
        }
        Console.WriteLine();

        // 체력 변화 표시
        Console.Write("HP ");
        WriteColorString(Player_In_Dungeon_Before_Information.Hp_before.ToString(), ConsoleColor.Red);
        Console.Write(" -> ");
        WriteColorString(Player_in.Hp.ToString(), ConsoleColor.Red);
        Console.WriteLine();

        // 마력 변화 표시
        Console.Write("MP ");
        WriteColorString(Player_In_Dungeon_Before_Information.Mp_before.ToString(), ConsoleColor.Blue);
        Console.Write(" -> ");
        WriteColorString(Player_in.Mp.ToString(), ConsoleColor.Blue);
        Console.WriteLine();

        // 경험치 변화 표시
        Console.Write("exp ");
        WriteColorString(Player_In_Dungeon_Before_Information.Exp_before.ToString(), ConsoleColor.Yellow);
        Console.Write(" -> ");
        WriteColorString(Player_in.Exp.ToString(), ConsoleColor.Yellow);
    }

    // 전투 보상 계산 및 콘솔 출력
    private void DisplayGetItem()
    {
        // 전투 보상 계산 및 콘솔 출력
        CalculateBattleCompensation();
    }

    private void TurnEnd()
    {
        // 결과 검사 
        // 1. 방에 존재하는 모든 몬스터의 체력이 0인가?
        // 2. 플레이어의 Hp가 0인가?

        KeyValuePair<bool, bool> battleResult = CheckBattleEnd();
        if (battleResult.Key)
        {
            DisplayBattleResult(battleResult.Value);
            return;
        }
    }

    // KeyValuePair<bool, bool>()
    // - Key 현재 게임이 끝났는가?
    // - Value 플레이어가 이겼는가?
    private KeyValuePair<bool, bool> CheckBattleEnd()
    {
        // 플레이어의 체력검사
        if (Player_in.IsDie) return new KeyValuePair<bool, bool>(true, false);

        // 방에 존재하는 몬스터의 체력검사
        foreach (var curMonster in Monsters_spawn)
        {
            if (!curMonster.IsDie) return new KeyValuePair<bool, bool>(false, false);
        }
        return new KeyValuePair<bool, bool>(true, true);
    }

    private void CalculateBattleCompensation()
    {
        // 각 몬스터 별로 드랍할 수 있는 장착 / 사용 아이템 / 골드를 획득한다.
        // 이 때, 확률은 다음과 같다. 
        // Gold - 확정 드롭.
        // 장착 아이템 - 10% 확률로 드랍. 드랍할 수 있는 아이템 중 하나가 드롭됨.
        // 섭취 아이템 - 20% 확률로 드랍. 드랍할 수 있는 아이템 중 하나가 드롭됨.

        int getTotalGold = 0;
        int DropProbability = 0;
        Random random = new Random();

        Dictionary<Item, int> dropItem = new Dictionary<Item, int>();
        Dictionary<Drop, int> dropEdibleItem = new Dictionary<Drop, int>();

        for (int i = 0; i < Monsters_spawn.Count; i++)
        {
            // 해당 몬스터를 잡았다면 아래 연산을 수행함.
            if (!Monsters_spawn[i].IsDie) continue;

            // Gold 계산
            getTotalGold += Monsters_spawn[i].Gold;

            // 장착 아이템 확률 계산
            DropProbability = random.Next(0, 100);
            if(DropProbability < 10)
            {
                int dropIdx = random.Next(0, Monsters_spawn[i].DropItem.Count());
                Item curDropItem = Monsters_spawn[i].DropItem[dropIdx]; // ...?
                
                if(dropItem.Count >= 1)
                {
                    foreach(var inItem in dropItem)
                    {
                        if(inItem.Key.Name == curDropItem.Name)
                        {
                            dropItem[inItem.Key]++;
                            break;
                        }
                    }
                }
                else
                {
                    dropItem.Add(curDropItem, 1);
                }

                Player_in.AddItem(curDropItem);
            }

            // 섭취 아이템 확률 계산
            DropProbability = random.Next(0, 100);
            if (DropProbability < 20)
            {
                int dropIdx = random.Next(0, Monsters_spawn[i].EdibleItem.Count());
                Drop curEdibleItem = Monsters_spawn[i].EdibleItem[dropIdx]; // ...?

                if (dropEdibleItem.Count >= 1)
                {
                    foreach (var inItem in dropEdibleItem)
                    {
                        if (inItem.Key.Name == curEdibleItem.Name)
                        {
                            dropEdibleItem[inItem.Key]++;
                            break;
                        }
                    }
                }
                else
                {
                    dropEdibleItem.Add(curEdibleItem, 1);
                }

                Player_in.AddDropItem(curEdibleItem);
            }
        }
        Player_in.AddGold(getTotalGold);

        // 콘솔 출력
        Console.WriteLine("[획득 아이템]");
        WriteColorString(getTotalGold.ToString(), ConsoleColor.Yellow);
        Console.Write(" Gold");
        Console.WriteLine();

        foreach (var inItem in dropItem)
        {
            Console.WriteLine($"{inItem.Key.Name} - {inItem.Value}");
        }

        foreach (var inItem in dropEdibleItem)
        {
            Console.WriteLine($"{inItem.Key.Name} - {inItem.Value}");
        }
        Console.WriteLine();
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
                if (result == 0 && min == 0) return 0;
                else if (result >= min && result <= max && !Monsters_spawn[result - 1].IsDie)
                    return result;
            }
            Console.WriteLine("잘못된 입력입니다!!!!");
        }
    }

    private List<int> CheckMultiInput(int min, int max, int selectMaxNum)
    {
        Console.WriteLine();
        Console.WriteLine("스킬 대상을 입력해주십시오!");

        int result;
        List<int> selectIdxs = new List<int>();

        while (true)
        {
            string input = Console.ReadLine();
            bool isNumber = int.TryParse(input, out result);
            if (isNumber)
            {
                if (result >= min && result <= max)
                {
                    // 선택 대상이 두 명일 때 입력이 중복되지 않는지 검사함.
                    if(selectMaxNum >= 2 && selectIdxs.Contains(result))
                    {
                        Console.WriteLine($"대상이 중복되었습니다!!!! - [{result}]");
                        continue;
                    }

                    // 선택한 몬스터가 살아있는지 검사함.
                    else if (!Monsters_spawn[result - 1].IsDie)
                    {
                        selectIdxs.Add(result);
                    }

                    else
                    {
                        Console.WriteLine($"대상이 죽어있습니다... - [{result}]");
                    }

                    if (selectIdxs.Count == selectMaxNum)
                    {
                        return selectIdxs;
                    }
                }
            }
            else Console.WriteLine("잘못된 입력입니다!!!!");
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

    private void WriteColorString(string str, ConsoleColor color, bool new_line = false)
    {
        Console.ForegroundColor = color;
        if (new_line) Console.WriteLine(str);
        else Console.Write(str);
        Console.ResetColor();
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

    private static void DisplayPlayerSkillInfo(Character player_in)
    {
        switch (player_in.Job)
        {
            case "전사":
                Console.Write("1. 강력한 한방 - MP ");
                player_in.DisplayPlayerColorString("5", ConsoleColor.Blue, true);
                Console.WriteLine("공격력 * 3으로 하나의 적을 공격합니다.");
                Console.Write("2. 가드 - MP ");
                player_in.DisplayPlayerColorString("10", ConsoleColor.Blue, true);
                Console.WriteLine("적의 공격을 무조건 방어합니다.(1회)");
                Console.WriteLine("3. 아드레날린[패시브]");
                Console.WriteLine("체력이 50% 미만일 때 공격력이 1.5배 향상됩니다.");
                break;
            case "궁수":
                Console.Write("1. 더블샷 - MP ");
                player_in.DisplayPlayerColorString("10", ConsoleColor.Blue, true);
                Console.WriteLine("공격력 * 2으로 하나의 적을 공격합니다.");
                Console.Write("2. 호크아이 - MP ");
                player_in.DisplayPlayerColorString("5", ConsoleColor.Blue, true);
                Console.WriteLine("적에게 공격을 무조건 명중 시킵니다.(1회)");
                Console.WriteLine("3. 민첩한 이동[패시브]");
                Console.WriteLine("체력이 50% 미만일 때 공격력이 1.5배 향상됩니다.");
                break;
            case "마법사":
                Console.Write("1. 파이어볼 - MP ");
                player_in.DisplayPlayerColorString("5", ConsoleColor.Blue, true);
                Console.WriteLine("공격력 * 2으로 하나의 적을 공격합니다.");
                Console.Write("2. 워터밤 - MP ");
                player_in.DisplayPlayerColorString("5", ConsoleColor.Blue, true);
                Console.WriteLine("공격력 * 1으로 둘의 적을 공격합니다.");
                Console.WriteLine("3. 마나재생");
                Console.WriteLine("마나를 전부 회복합니다.");
                break;
        }
    }
}
