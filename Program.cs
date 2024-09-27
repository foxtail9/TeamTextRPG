﻿namespace TeamTextRPG;

class Program
{
    // 구현 완료
    // 화면 만들기 - 메인화면
    // 화면 만들기 - 상태보기
    // 화면 만들기 - 인벤토리
    // 화면 만들기 - 상점
    // 화면 만들기 - 상점 [구매] 
    // 화면 만들기 - 인벤토리 [장착관리]

    // 기능1 [All] - 선택한 화면으로 이동하기
    // 기능2 [Stat] - 캐릭터의 정보  표시 (변경되는 정보를 확인) - 레벨 / 이름 / 직업 / 공격력 / 방어력 / 체력 / Gold
    // 기능2_1 [Stat] - 장비 반영에 따른 정보 - 공격력/방어력
    // 기능3 [Inventory] - 보유 아이템 표시 (인벤토리)
    // 기능4 [Inventory] - 장비 장착
    // 기능5 [Shop] - 상점 리스트
    // 기능6 [Shop] - 구매 기능


    // =====================

    private static Character player;
    private static Item[] itemDb;

    static void Main(string[] args)
    {
        SetData();
        DisplayMainUI();
    }

    static void SetData()
    {
        player = new Character(1, "Chad", "전사", 10, 5, 100, 10000);
        itemDb = new Item[]
        {
            new Item("수련자의 갑옷", 1, 5,"수련에 도움을 주는 갑옷입니다. ",1000),
            new Item("무쇠갑옷", 1, 9,"무쇠로 만들어져 튼튼한 갑옷입니다. ",2000),
            new Item("스파르타의 갑옷", 1, 15,"스파르타의 전사들이 사용했다는 전설의 갑옷입니다. ",3500),
            new Item("낣은 검", 0, 2,"쉽게 볼 수 있는 낡은 검 입니다. ",600),
            new Item("청동 도끼", 0, 5,"어디선가 사용됐던거 같은 도끼입니다. ",1500),
            new Item("스파르타의 창", 0, 7,"스파르타의 전사들이 사용했다는 전설의 창입니다. ",2500)
        };
    }

    static void DisplayMainUI()
    {
        Console.Clear();
        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("1. 상태 보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine("3. 상점");
        Console.WriteLine("4. 여관");
        Console.WriteLine("5. 던전");
        Console.WriteLine("6. 퀘스트 게시판");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");


        int result = CheckInput(1, 6);

        switch (result)
        {
            case 1:
                DisplayStatUI();
                break;

            case 2:
                DisplayInventoryUI();
                break;

            case 3:
                DisplayShopUI();
                break;

            case 5:
                DisplayDungeonEntrance();
                break;
        }
    }

    static void DisplayStatUI()
    {
        Console.Clear();
        Console.WriteLine("상태 보기");
        Console.WriteLine("캐릭터의 정보가 표시됩니다.");

        player.DisplayCharacterInfo();

        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int result = CheckInput(0, 0);

        switch (result)
        {
            case 0:
                DisplayMainUI();
                break;
        }
    }

    static void DisplayInventoryUI()
    {
        Console.Clear();
        Console.WriteLine("인벤토리");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");

        player.DisplayInventory(false);

        Console.WriteLine();
        Console.WriteLine("1. 장착 관리");
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int result = CheckInput(0, 1);

        switch (result)
        {
            case 0:
                DisplayMainUI();
                break;

            case 1:
                DisplayEquipUI();
                break;
        }
    }

    static void DisplayEquipUI()
    {
        Console.Clear();
        Console.WriteLine("인벤토리 - 장착관리");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");

        player.DisplayInventory(true);

        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int result = CheckInput(0, player.InventoryCount);

        switch (result)
        {
            case 0:
                DisplayInventoryUI();
                break;

            default:

                int itemIdx = result - 1;
                Item targetItem = itemDb[itemIdx];
                player.EquipItem(targetItem);

                DisplayEquipUI();
                break;
        }
    }

    static void DisplayShopUI()
    {
        Console.Clear();
        Console.WriteLine("상점");
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
        Console.WriteLine();
        Console.WriteLine("[보유 골드]");
        Console.WriteLine($"{player.Gold} G");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");

        for (int i = 0; i < itemDb.Length; i++)
        {
            Item curItem = itemDb[i];

            string displayPrice = (player.HasItem(curItem) ? "구매완료" : $"{curItem.Price} G");
            Console.WriteLine($"- {curItem.ItemInfoText()}  |  {displayPrice}");
        }

        Console.WriteLine();
        Console.WriteLine("1. 아이템 구매");
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int result = CheckInput(0, 1);

        switch (result)
        {
            case 0:
                DisplayMainUI();
                break;

            case 1:
                DisplayBuyUI();
                break;
        }
    }

    static void DisplayBuyUI()
    {
        Console.Clear();
        Console.WriteLine("상점 - 아이템 구매");
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
        Console.WriteLine();
        Console.WriteLine("[보유 골드]");
        Console.WriteLine($"{player.Gold} G");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");

        for (int i = 0; i < itemDb.Length; i++)
        {
            Item curItem = itemDb[i];

            string displayPrice = (player.HasItem(curItem) ? "구매완료" : $"{curItem.Price} G");
            Console.WriteLine($"- {i + 1} {curItem.ItemInfoText()}  |  {displayPrice}");
        }

        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int result = CheckInput(0, itemDb.Length);

        switch (result)
        {
            case 0:
                DisplayShopUI();
                break;

            default:
                int itemIdx = result - 1;
                Item targetItem = itemDb[itemIdx];

                // 이미 구매한 아이템이라면?
                if (player.HasItem(targetItem))
                {
                    Console.WriteLine("이미 구매한 아이템입니다.");
                    Console.WriteLine("Enter 를 눌러주세요.");
                    Console.ReadLine();
                }
                else // 구매가 가능할떄
                {
                    //   소지금이 충분하다
                    if (player.Gold >= targetItem.Price)
                    {
                        Console.WriteLine("구매를 완료했습니다.");
                        player.BuyItem(targetItem);
                    }
                    else
                    {
                        Console.WriteLine("골드가 부족합니다.");
                        Console.WriteLine("Enter 를 눌러주세요.");
                        Console.ReadLine();
                    }

                    //   소지금이 부족핟
                }

                DisplayBuyUI();
                break;
        }
    }


    /// <summary>
    /// 던전을 선택할 때 보여지는 함수입니다.
    /// 선택에 따라 Dungeon class 생성자에 들어가는 인자가 달라집니다.
    /// 작성자 : 김동현
    /// </summary>
    static void DisplayDungeonEntrance()
    {
        Console.Clear();
        Console.WriteLine("<<던전 입구에 도착했습니다>>");
        Console.WriteLine("이곳은 위험한 몬스터가 출몰합니다. 주의해주세요!");
        Console.WriteLine("1.엘프의 숲 -난이도 : 쉬움");
        Console.WriteLine("2.저주받은 땅 -난이도 : 보통");
        Console.WriteLine("3.함락한 성 -난이도 : 어려움");

        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int result = CheckInput(0, 3);

        switch (result)
        {
            case 0:
                DisplayMainUI();
                break;

            // 위 함수 CheckInput에서 올바르지 않은 입력(1~3 제외)은 걸려지므로,
            // default로 설정하였습니다.
            default:
                //Dungeon dungeon = new Dungeon(result);
                //dungeon.DungeonSystem(player);

                DungeonManager DM = new DungeonManager(DungeonDatabase.Dungeons[result], player);
                DM.DungeonSystem();
                break;
        }
    }

    static int CheckInput(int min, int max)
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

    static void DisplayQuestUI()
    {
        List<Quest> allQuests = QuestDatabase.Quests;
        Console.Clear();
        Console.WriteLine("<<퀘스트 게시판>>");
        Console.WriteLine("수행할 퀘스트를 선택해주세요!\n");

        Console.WriteLine("[모든 퀘스트 목록]\n");
        foreach (var quest in allQuests)
        {
            quest.DisplayQuest();
            Console.WriteLine();
        }
    }
}