using System.Numerics;
using TeamTextRPG.Byungchul;

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

    private static Character player;
    private static Item[] itemDb;
    private static Drop[] dropDB;
    private static Character[] invertory;

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
            new Drop("HP 포션",0,"HP를 30회복 시킨다", 500),
            new Drop("MP 포션",1,"MP를 30회복 시킨다", 500),
            new Drop("사파이어", 2,"푸른색의 보석이다.",500)
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
        Console.WriteLine("4. 휴식");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");


        int result = CheckInput(1, 4);

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
        Console.WriteLine($"{player.Gold} G");
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

    static void DisplaySellUI()
    {
        Console.Clear();
        Console.WriteLine("상점 - 아이템 판매");
        Console.WriteLine("불필요한 아이템을 판매할 수 있는 상점입니다.");
        Console.WriteLine();
        Console.WriteLine("[보유 골드]");
        Console.WriteLine($"{player.Gold} G");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");
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
                Item targetItem = itemDb[itemIdx];
                player.SellITem(targetItem, itemIdx);
                DisplaySellUI();
                break;
        }


    }

    static void DisplayRestUI()
    {
        Console.Clear();
        Console.WriteLine("휴식하기");
        Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다.(보유 골드 : {player.Gold} G");
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
}