using TeamTextRPG.Byungchul;

namespace TeamTextRPG;
public class Character
{
    public int Level { get; }
    public string Name { get; }
    public string Job { get; }
    public int Atk { get; }
    public int Def { get; }
    public int MaxHp { get; private set; }
    public int Hp { get; private set; }
    public int MaxMp { get; private set; }
    public int Mp { get; private set; }
    public int Gold { get; private set; }

    public int ExtraAtk { get; private set; }
    public int ExtraDef { get; private set; }

    private List<Item> Inventory = new List<Item>();
    private List<Drop> DropInventory = new List<Drop>();
    private List<Item> EquipList = new List<Item>();
    public Item EquipWeapon { get; set; }
    public Item EquipArmor { get; set; }
    public List<Quest> PlayerQuestList = new List<Quest>();

    public int InventoryCount
    {
        get
        {
            return Inventory.Count;
        }
    }

    public int DropInventoryCount
    {
        get
        {
            return DropInventory.Count;
        }
    }

    public Character(int level, string name, string job, int atk, int def, int maxhp, int hp, int maxmp, int mp, int gold)
    {
        Level = level;
        Name = name;
        Job = job;
        Atk = atk;
        Def = def;
        MaxHp = maxhp;
        Hp = hp;
        MaxMp = maxmp;
        Mp = mp;
        Gold = gold;
        PlayerQuestList = new List<Quest>();
    }
   
    public void DisplayCharacterInfo()
    {
        Console.WriteLine($"Lv. {Level:D2}");
        Console.WriteLine($"{Name} {{ {Job} }}");
        Console.WriteLine(ExtraAtk == 0 ? $"공격력 : {Atk}" : $"공격력 : {Atk + ExtraAtk} (+{ExtraAtk})");
        Console.WriteLine(ExtraDef == 0 ? $"방어력 : {Def}" : $"방어력 : {Def + ExtraDef} (+{ExtraDef})");
        Console.WriteLine($"체력 : {Hp}({MaxHp})");
        Console.WriteLine($"마나 : {Mp}({MaxMp})");
        Console.WriteLine($"Gold : {Gold} G");
    }

    public void DisplayInventory(bool showIdx)
    {
        for (int i = 0; i < Inventory.Count; i++)
        {
            Item targetItem = Inventory[i];

            string displayIdx = showIdx ? $"{i + 1} " : "";
            string displayEquipped =  "";
            if (EquipArmor == Inventory[i] || EquipWeapon == Inventory[i])
            {
                Console.ForegroundColor = ConsoleColor.Green;
                displayEquipped = "[E]";
            }
            Console.WriteLine($"- {displayIdx}{displayEquipped} {targetItem.ItemInfoText()}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    public void DisplaySellInventory(bool showIdx)
    {
        for (int i = 0; i < Inventory.Count; i++)
        {
            Item targetItem = Inventory[i];

            string displayIdx = showIdx ? $"{i + 1} " : "";
            string displayEquipped = "";
            if (EquipArmor == Inventory[i] || EquipWeapon == Inventory[i])
            {
                Console.ForegroundColor = ConsoleColor.Green;
                displayEquipped = "[E]";
            }
            Console.WriteLine($"- {displayIdx}{displayEquipped} {targetItem.ItemInfoText()}  |  {targetItem.Value}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }


    public void DisplayDropInventory(bool showIdx)
    {
        for (int i = 0; i < DropInventory.Count; i++)
        {
            Drop targetItem = DropInventory[i];

            string displayIdx = showIdx ? $"{i + 1} " : "";
            string displatValue = showIdx ? $"  |  {targetItem.Value}G" : "";

            Console.WriteLine($"- {displayIdx} {targetItem.DropInfoText()}{displatValue}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }


    public void EquipItem(Item item, int i)
    {
        int targetItemType = Inventory[i].Type;
        Item targetItem = Inventory[i];

        if (EquipArmor == targetItem || EquipWeapon == targetItem)
        {
            if (targetItemType == 0)
            {
                ExtraAtk -= EquipWeapon.Akp;
                EquipWeapon = null;
            }
               
            else
            {
                ExtraDef -= EquipArmor.Akp;
                EquipArmor = null;
            }
               
                
        }
        else
        {
            if (targetItemType == 0)
            {
                EquipList.Remove(EquipWeapon);
                EquipWeapon = Inventory[i];
                ExtraAtk = EquipWeapon.Akp;
            }
               
            else
            {
                EquipList.Remove(EquipArmor);
                EquipArmor = Inventory[i];
                ExtraDef = EquipArmor.Akp;
            }
        }
    }

    public bool IsEquipped(Item item)
    {
        return EquipList.Contains(item);
    }

    public void BuyItem(Item item)
    {
        Gold -= item.Value;
        Inventory.Add(item);
    }
    public void AddDropItem(Drop drop)
    {
        DropInventory.Add(drop);
    }

    public void SellITem(int i)
    {
        int targetItemValue = Inventory[i].Value;
        if (EquipArmor == Inventory[i] || EquipWeapon == Inventory[i])
        {
            Console.WriteLine("착용중인 아이템입니다");
            Console.WriteLine("Enter 를 눌러주세요.");
            Console.ReadLine();
        }
        else
        {
            Inventory.RemoveAt(i);
            Gold += targetItemValue;
        } 
    }

    public void SellDropItem(int i)
    {
        int targetItemValue = DropInventory[i].Value;
        DropInventory.RemoveAt(i);
        Gold += targetItemValue;
    }

    public void UsePotion(int i)
    {
        int targetItemType = DropInventory[i].Type;
        if (targetItemType == 0)
        {
            Hp = Math.Min(Hp + 30, MaxHp);
            DropInventory.RemoveAt(i);
        }
        else if (targetItemType == 1)
        {
            Mp = Math.Min(Mp + 30, MaxMp);
            DropInventory.RemoveAt(i);
        }
        else if(targetItemType == 2)
        {
            MaxHp += 10;
            DropInventory.RemoveAt(i);
        }
        else
        {
            MaxMp += 10;
            DropInventory.RemoveAt(i);
        }

    }

    public void Rest()
    {
        Gold -= 500;
        Hp = 100;
    }

    public bool HasItem(Item item)
    {
        return Inventory.Contains(item);
    }

    public bool HasDropItem(Drop drop)
    {
        return DropInventory.Contains(drop);
    }


    public void AddQuest(Quest quest)
    {
        if (!PlayerQuestList.Contains(quest)) // 중복 퀘스트 방지
        {
            PlayerQuestList.Add(quest);
            Console.WriteLine($"{quest.questname} 퀘스트가 추가되었습니다.");
        }
        else
        {
            Console.WriteLine("이미 해당 퀘스트를 진행하고 있습니다.");
        }
    }
}