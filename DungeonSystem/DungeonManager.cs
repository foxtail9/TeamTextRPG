namespace TeamTextRPG;

public class DungeonManager
{
    // 현재 위치한 던전 정보
    private Dungeon Dungeon_in { get; set; }

    // 현재 전투에 참여한 몬스터 정보
    // private List<Monster> Monsters_in_battle { get; set; }

    // Character & Item의 보호 수준 때문에 접근하기가 힘듭니다...
    // 둘의 보호 수준을 바꾸었으면 합니다.
    public DungeonManager(Dungeon cur_dungeon, Character player)
    {
        Dungeon_in = cur_dungeon;
        // Monsters_in_battle = new List<Monster>();
        SettingCurDungeonInfo();
    }

    public void DungeonSystem()
    {
        while (true)
        {
            DisplayInDungeonBattle();
        }
    }

    // void SettingBattleMonsters(List<Monster> monsters_can_appear) 로 바꿔줄 것임.
    private void SettingCurDungeonInfo()
    {
        Random rand = new Random();
        int spawn_num = rand.Next(1, 4);

        for (int i = 0; i < spawn_num; i++)
        {
            // 해당 던전에서 출현 가능한 몬스터 List에서 
            // spawn_num의 수만큼 현재 전투에 참여한 몬스터 List에 추가합니다. 

            // int random_monster = rand.Next();
            // Monsters_in_battle[i].Add(Dungeon_in.monsters_can_appear[random_monster]);
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

        // 플레이어 현재 정보 출력 -> 처리를 어떻게 할 것인지?
        Console.WriteLine("내정보");
        // player.~~~

        // 행동 선택
        Console.WriteLine();
        Console.WriteLine("1. 공격");
        Console.WriteLine("2. 스킬 사용");
        Console.WriteLine("3. 인벤토리");
        Console.WriteLine("4. 도망가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int result = CheckInput(0, 5);
        switch (result)
        {
            case 0:

                break;
        }
    }


    private void DisplayMonstersInfo(bool showIdx)
    {

        /*        for (int i = 0; i < Monsters_in_battle[i].Count; i++)
                {
                    string monster_info = "";
                    if (showIdx) monster_info += "(i+1).ToString() ";

                    // ~ 몬스터 정보 출력
                    // ~ 레벨 / 이름 / 체력
                    // ~ 몬스터 생사 여부에 따라 글씨 색 변경...
                }*/

    }

    int CheckInput(int min, int max)
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
