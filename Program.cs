﻿using Microsoft.VisualBasic;
using System.Numerics;
using TeamTextRPG.Jobs;

namespace TeamTextRPG;

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

    public static Character player;
    public static Item[] itemDb;
    public static Drop[] dropDB;
    public static Item[] questDb;
    public static Character[] invertory;

    static void Main(string[] args)
    {
        intro(300); //300 ~ 500 사이
    }
    static void SetData()
    {
        itemDb = new Item[]
        {
            new Item("커먼","목검", 0, 5, "단단한 목검이다.", 500),
            new Item("커먼","철검", 0, 6, "평범한 철검이다.", 800),
            new Item("언커먼","카타나", 0, 8, "날카로운 카타나다.", 1200),
            new Item("언커먼","재련된 강철검", 0, 12, "강철 검이라고 해서 다 같은 강철 검은 아니다", 1500),
            new Item("레어","초진동검", 0, 15, "첨단 기술이 적용된 검이다.", 2000),
            new Item("레어","카타나", 0, 17, "작열하는 불의 기운을 담고 있는 도.", 2500),
            new Item("커먼","면 셔츠", 1, 3, "면으로 만든 셔츠다. 최소한의 방어력을 제공한다.", 500),
            new Item("언커먼","가죽 조끼", 1, 5, "가죽으로 만든 셔츠다. 낮은 방어력을 제공한다.", 800),
            new Item("언커먼","가죽 스커트", 1, 5, "가죽으로 만든 치마다. 남자도 입을 수 있다.", 800),
            new Item("레어","체인메일", 1, 9, "철사 따위로 만든 고리를 엮은 사슬 형태로 된 갑옷이다.", 1500),
            new Item("레어","플레이트 아머", 1, 12, "고딕 양식 판금 갑옷이다.", 2000),
        };
        dropDB = new Drop[]
        {
            new Drop("HP 포션",0,"체력을 30회복 시킨다", 500),
            new Drop("MP 포션",1,"마나를 30회복 시킨다", 500),
            new Drop("루비", 2,"붉은색의 보석이다. 체력를 10 올려준다",1000),
            new Drop("사파이어", 3,"푸른색의 보석이다. 마나를 10 올려준다",1000)
        };
        questDb = new Item[]
        {
            new Item("커먼","퀘스트로 받은 철검", 0, 10,"강력한 철로 만든 검입니다.", 150),
            new Item("커먼","퀘스트로 받은 면 셔츠", 1, 3,"면으로 만든 셔츠다. 최소한의 방어력을 제공한다.", 500)
        };
        player = SaveLoadManager.LoadGame();

        if (player == null)
        {
            SetPlayerName();
        }
        else
        {
            Console.WriteLine($"{player.Name} 캐릭터로 게임을 시작합니다.");
            Thread.Sleep(3000);
            DisplayMainUI();
        }
    }

    static void SetPlayerName()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("원하시는 이름을 설정해주세요.");
            Console.WriteLine();
            string player_name = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine($"입력하신 이름은{player_name}입니다.");
            Console.WriteLine();
            Console.WriteLine("1. 저장");
            Console.WriteLine("2. 취소");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = CheckInput(1, 2);
            if (input == 1)
            {
                SetPlayerJob(player_name);
                break;
            }
        }
    }
    static void SetPlayerJob(string player_name)
    {
        Console.Clear();
        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("원하시는 직업을 설정해주세요.");
        Console.WriteLine();
        Console.WriteLine("1. 전사");
        Console.WriteLine("2. 궁수");
        Console.WriteLine("3. 마법사");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        int input = CheckInput(1, 3);
        switch (input)
        {
            case 1: player = new Warrior(1, player_name, 51500); break;
            case 2: player = new Archer(1, player_name, 51500); break;
            case 3: player = new Mage(1, player_name, 51500); break;
        }
        for (int i = 0; i < 3; i++)
        {
            player.AddDropItem(dropDB[0]);
            player.AddDropItem(dropDB[1]);
        }
        DisplayMainUI();
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
        Console.WriteLine("7. 저장하기");
        Console.WriteLine("8. 게임종료");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int result = CheckInput(1, 8);

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
            case 4:
                DisplayRestUI();
                break;
            case 5:
                DisplayDungeonEntrance(); //던전 입장UI
                break;
            case 6:
                DisplayQuestUI();//퀘스트 UI
                break;
            case 7:
                DisplaySaveUI();
                break;
            case 8:
                return;
        }
    }
    static void DisplayStatUI()
    {
        Console.Clear();
        Console.WriteLine("상태 보기");
        Console.WriteLine("캐릭터의 정보가 표시됩니다.");

        player.DisplayCharacterInfo();

        Console.WriteLine();

        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.WriteLine();
        Console.WriteLine("0. 나가기");

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
        Console.WriteLine("[장비 아이템 목록]");

        player.DisplayInventory(false);

        Console.WriteLine("[소모 아이템 목록]");

        player.DisplayDropInventory(false);

        Console.WriteLine();
        Console.WriteLine("1. 장착 관리");
        Console.WriteLine("2. 소모 아이템 관리");
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int result = CheckInput(0, 2);

        switch (result)
        {
            case 0:
                DisplayMainUI();
                break;

            case 1:
                DisplayEquipUI();
                break;
            case 2:
                DisplayUsePotionUI();
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
                player.EquipItem(targetItem, itemIdx);

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
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"{player.Gold} G");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");

        for (int i = 0; i < itemDb.Length; i++)
        {
            Item curItem = itemDb[i];

            string displayPrice = (player.HasItem(curItem) ? "구매완료" : $"{curItem.Value} G");
            Console.WriteLine($"- {curItem.ItemInfoText()}  |  {displayPrice}");
        }

        Console.WriteLine();
        Console.WriteLine("1. 아이템 구매");
        Console.WriteLine("2. 아이템 판매");
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int result = CheckInput(0, 2);

        switch (result)
        {
            case 0:
                DisplayMainUI();
                break;

            case 1:
                DisplayBuyUI();
                break;
            case 2:
                DisplaySellUI();
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
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"{player.Gold} G");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");

        for (int i = 0; i < itemDb.Length; i++)
        {
            Item curItem = itemDb[i];

            string displayPrice = (player.HasItem(curItem) ? "구매완료" : $"{curItem.Value} G");
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
                    if (player.Gold >= targetItem.Value)
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
    static void DisplayDrpoUI()
    {
        Console.Clear();
        Console.WriteLine("[아이템 목록]");
        player.DisplayDropInventory(false);
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine("1. 포션사용");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        int result = CheckInput(0, 1);

        switch (result)
        {
            case 0:
                DisplayInventoryUI();
                break;
            case 1:
                DisplayUsePotionUI();
                break;
        }
    }
    static void DisplayUsePotionUI()
    {
        Console.Clear();
        Console.WriteLine("[아이템 목록]");
        player.DisplayDropInventory(true);
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        int result = CheckInput(0, player.DropInventoryCount);

        switch (result)
        {
            case 0:
                DisplayInventoryUI();
                break;
            default :
                int itemIdx = result - 1;
                player.UsePotion(itemIdx);
                DisplayUsePotionUI();
                break;
        }
    }
    static void DisplaySellUI()
    {
        Console.Clear();
        Console.WriteLine("상점 - 아이템 판매");
        Console.WriteLine("불필요한 아이템을 판매할 수 있는 상점입니다.");
        Console.WriteLine();
        Console.WriteLine("[보유 골드]");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"{player.Gold} G");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine("[장비 아이템 목록]");
        player.DisplayInventory(false);
        Console.WriteLine("[소모 아이템 목록]");
        player.DisplayDropInventory(false);
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine("1. 장비 아이템 판매");
        Console.WriteLine("2. 소모아이템 판매");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int result = CheckInput(0, 2);
        switch (result)
        {
            case 0:
                DisplayShopUI();
                break;
            case 1:
                DisplaySeIlItemUI();
                break;
            case 2:
                DisplaySeIlDrpoItemUI();
                break;
        }
    }
    static void DisplaySeIlItemUI()
    {
        Console.Clear();
        Console.WriteLine("상점 - 아이템 판매");
        Console.WriteLine("불필요한 아이템을 판매할 수 있는 상점입니다.");
        Console.WriteLine();
        Console.WriteLine("[보유 골드]");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"{player.Gold} G");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine("[장비 아이템 목록]");
        player.DisplaySellInventory(true);
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        int result = CheckInput(0, player.InventoryCount);
        switch (result)
        {
            case 0:
                DisplayShopUI();
                break;
            default:
                int itemIdx = result - 1;
                player.SellITem(itemIdx);
                DisplaySeIlItemUI();
                break;
        }
    }
    static void DisplaySeIlDrpoItemUI()
    {
        Console.Clear();
        Console.WriteLine("상점 - 아이템 판매");
        Console.WriteLine("불필요한 아이템을 판매할 수 있는 상점입니다.");
        Console.WriteLine();
        Console.WriteLine("[보유 골드]");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"{player.Gold} G");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine("[소모 아이템 목록]");
        player.DisplayDropInventory(true);
        Console.WriteLine();
        Console.WriteLine("0. 나가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        int result = CheckInput(0, player.DropInventoryCount);
        switch (result)
        {
            case 0:
                DisplayShopUI();
                break;
            default:
                int itemIdx = result - 1;
                player.SellDropItem(itemIdx);
                DisplaySeIlDrpoItemUI();
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
        Console.WriteLine();
        Console.Write("1.엘프의 숲 -난이도 : ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("쉬움");
        Console.ResetColor();
        Console.Write("2.저주받은 땅 -난이도 : ");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("보통");
        Console.ResetColor();
        Console.Write("3.함락한 성 -난이도 : ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("어려움");
        Console.ResetColor();

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
                Dungeon curDungeon = new Dungeon(DungeonDatabase.Dungeons[result - 1]);
                DungeonManager DM = new DungeonManager(curDungeon, player);
                DM.DungeonSystem();
                DisplayMainUI();
                break;
        }

    }
    static void DisplayRestUI()
    {
        Console.Clear();
        Console.WriteLine("휴식하기");
        Console.Write($"500 G 를 내면 체력을 회복할 수 있습니다.(보유 골드 : ");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write($"{player.Gold} G");
        Console.ResetColor();
        Console.WriteLine(")");
        Console.WriteLine();
        Console.WriteLine("1. 휴식하기");
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
                if (player.Gold >= 500)
                {
                    player.Rest();
                    Console.WriteLine();
                    Console.WriteLine("휴식을 완료 했습니다");
                    Console.WriteLine("Enter 를 눌러주세요.");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("골드가 부족합니다.");
                    Console.WriteLine("Enter 를 눌러주세요.");
                    Console.ReadLine();
                }
                DisplayMainUI();
                DisplayMainUI();
                break;
        }
    }
    static void DisplayQuestUI()
    {
        Console.Clear();
        Console.WriteLine("<<퀘스트 게시판>>");
        Console.WriteLine("수행할 퀘스트를 선택해주세요!\n");
        List<Quest> allQuests = QuestDatabase.Quests;
        Console.WriteLine("[퀘스트 목록]\n");
        for (int i = 0; i < allQuests.Count; i++)
        {
            // 퀘스트의 진행 상태에 따라 표시 형식을 변경
            string rewardItemName = allQuests[i].RewardItem != null ? allQuests[i].RewardItem.Name : "???"; // RewardItem이 null일 경우 공백 처리
            string status = player.PlayerQuestList.Any(q => q.QuestName == allQuests[i].QuestName) ? "[진행중]" : "";
            Console.WriteLine($"{status}[요구레벨 : {allQuests[i].RequiredLevel}] | {allQuests[i].QuestName} | {allQuests[i].QuestDescription} | 보상 : {rewardItemName}");
        }
        Console.WriteLine("");
        Console.WriteLine("0.나가기");

        int result = CheckInput(0, allQuests.Count);

        switch (result)
        {
            case 0:
                DisplayMainUI();
                break;
            default:
                int questIndex = result - 1;
                Quest selectedQuest = allQuests[questIndex];
                player.AddQuest(selectedQuest); // 퀘스트 추가
                Console.WriteLine("계속하려면 Enter 키를 누르세요...");
                Console.ReadLine();
                DisplayQuestUI(); // 퀘스트 UI 다시 표시
                break;
        }
    }
    static void DisplaySaveUI() {
        Console.Clear();
        Console.WriteLine("<<지금까지의 모험을...>>\n");
        Console.WriteLine("1. 저장합니다.");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("2. 삭제합니다.\n");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("0. 돌아가기.");

        int input = CheckInput(0, 2);

        switch (input)
        {
            case 1:
                SaveLoadManager.SaveGame(player);
                DisplayMainUI();
                break;
            case 2:
                DisplayDelUI();
                break;
            case 0:
                DisplayMainUI();
                break;
        }
    }
    static void DisplayDelUI() {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("<<!!경고!!>>\n");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"{player.Name}의 데이터를 삭제 하시겠습니까?\n");
        Console.WriteLine("1. 삭제하기.");
        Console.WriteLine("0. 취소(돌아가기)\n");
        Console.WriteLine("원하시는 행동을 입력해주세요.");

        int input = CheckInput(0, 2);

        switch (input)
        {
            case 1:
                SaveLoadManager.DeleteSaveFile();
                intro(300);
                break;
            case 0:
                DisplayMainUI();
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
    static void intro(int time)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        string[] asciiArtLines = {
           @" __  __     ____        ____       _____       ____       ____      ",
           @"/\ \/\ \   /\  _`\     /\  _`\    /\  __`\    /\  _`\    /\  _`\    ",
           @"\ \ \ \ \  \ \,\L\_\   \ \ \/\_\  \ \ \/\ \   \ \ \/\ \  \ \ \L\_\  ",
           @" \ \ \ \ \  \/_\__ \    \ \ \/_/_  \ \ \ \ \   \ \ \ \ \  \ \  _\L  ",
           @"  \ \ \_/ \   /\ \L\ \   \ \ \L\ \  \ \ \_\ \   \ \ \_\ \  \ \ \L\ \",
           @"   \ `\___/   \ `\____\   \ \____/   \ \_____\   \ \____/   \ \____/",
           @"    `\/__/     \/_____/    \/___/     \/_____/    \/___/     \/___/ "
        };

        foreach (string line in asciiArtLines)
        {
            Console.WriteLine(line);
            Thread.Sleep(50);
        }
        Thread.Sleep(time * 3);
        Console.ForegroundColor = ConsoleColor.White;
        SetData();
    }


}