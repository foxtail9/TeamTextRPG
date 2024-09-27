﻿namespace TeamTextRPG;
class Character
{
    public int Level { get; }
    public string Name { get; }
    public string Job { get; }
    public int Atk { get; }
    public int Def { get; }
    public int Hp { get; private set; }
    public int Gold { get; private set; }

    public int ExtraAtk { get; private set; }
    public int ExtraDef { get; private set; }

    private List<Item> Inventory = new List<Item>();
    private List<Item> EquipList = new List<Item>();

    public int InventoryCount
    {
        get
        {
            return Inventory.Count;
        }
    }

    public Character(int level, string name, string job, int atk, int def, int hp, int gold)
    {
        Level = level;
        Name = name;
        Job = job;
        Atk = atk;
        Def = def;
        Hp = hp;
        Gold = gold;
    }

    public void DisplayCharacterInfo()
    {
        Console.WriteLine($"Lv. {Level:D2}");
        Console.WriteLine($"{Name} {{ {Job} }}");
        Console.WriteLine(ExtraAtk == 0 ? $"공격력 : {Atk}" : $"공격력 : {Atk + ExtraAtk} (+{ExtraAtk})");
        Console.WriteLine(ExtraDef == 0 ? $"방어력 : {Def}" : $"방어력 : {Def + ExtraDef} (+{ExtraDef})");
        Console.WriteLine($"체력 : {Hp}");
        Console.WriteLine($"Gold : {Gold} G");
    }

    public void DisplayInventory(bool showIdx)
    {
        for (int i = 0; i < Inventory.Count; i++)
        {
            Item targetItem = Inventory[i];

            string displayIdx = showIdx ? $"{i + 1} " : "";
            string displayEquipped = IsEquipped(targetItem) ? "[E]" : "";
            Console.WriteLine($"- {displayIdx}{displayEquipped} {targetItem.ItemInfoText()}");
        }
    }

    public void EquipItem(Item item)
    {

        if (IsEquipped(item))
        {
            EquipList.Remove(item);
            if (item.Type == 0)
                ExtraAtk -= item.Akp;
            else
                ExtraDef -= item.Akp;
        }
        else
        {
            EquipList.Add(item);
            if (item.Type == 0)
                ExtraAtk += item.Akp;
            else
                ExtraDef += item.Akp;
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


    public void SellITem(Item item, int i)
    {
        int targetItem = Inventory[i].Value;
        Inventory.RemoveAt(i);
        Gold += targetItem;
    }

    public void DisplaySellInventory(bool showIdx)
    {
        for (int i = 0; i < Inventory.Count; i++)
        {
            Item targetItem = Inventory[i];

            string displayIdx = showIdx ? $"{i + 1} " : "";
            string displayEquipped = IsEquipped(targetItem) ? "[E]" : "";
            Console.WriteLine($"- {displayIdx}{displayEquipped} {targetItem.ItemInfoText()}  |  {targetItem.Value}");
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
}