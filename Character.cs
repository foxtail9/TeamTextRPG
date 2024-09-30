using TeamTextRPG.Byungchul;

namespace TeamTextRPG;
public class Character
{
    public int Level { get; protected set; }
    public string Name { get; protected set; }
    public string Job { get; protected set; }
    public int Atk { get; protected set; }
    public int Def { get; protected set; }
    public int Hp { get; protected set; }
    public int Mp { get; protected set; }
    public int Gold { get; protected set; }
    public int Critical { get; private set; } = 15;
    public int Avoid { get; protected set; } = 10;

    public bool IsInvincible { get; protected set; } = false;
    public bool IsHawkeye { get; protected set; } = false;
    public bool OnPassive { get; protected set; } = false;

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
    public Character(int level, string name, string job, int atk, int def, int hp, int gold)
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
            Console.ResetColor();
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

    public int RandomDamage()
    {
        // 오차 범위 계산
        int damage_range = (int)Math.Round(Atk * 0.1f);
        int start_damage = Atk - damage_range;
        int end_damage = Atk + damage_range;

        Random random = new Random();
        int result_damage = random.Next(start_damage, end_damage + 1);
        return result_damage;
    }

    public void BasicAttack(Monster monster)
    {

        // 치명타 확률
        Random random = new Random();
        int critical_prob = random.Next(1, 101);
        int player_damage;

        if (critical_prob <= Critical)
        {
            // 치명타
            Console.WriteLine("치명타가 발동되었습니다.");
            player_damage = (int)Math.Round(RandomDamage() * 0.1f);
            monster.MonsterDefense(player_damage, IsHawkeye);
        }
        else
        {
            // 평타
            Console.WriteLine("치명타가 발동되지 않았습니다.");
            player_damage = RandomDamage();
            monster.MonsterDefense(player_damage, IsHawkeye);
        }
        Console.WriteLine($"{Name}이 {player_damage} 만큼의 피해를 입어 Hp가 {Hp}이 되었습니다. ");
    }

    public void PlayerDefense(int monster_damage)
    {
        // 회피 확률
        Random random = new Random();
        int avoid_prob = random.Next(1, 101);
        int new_monster_damage = monster_damage - Def;
        new_monster_damage = monster_damage > 0 ? monster_damage : 0;

        if (IsInvincible)
        {
            Console.WriteLine($"{Name}이 Guard 스킬을 사용했으므로 데미지를 받지 않습니다.");
            return;
        }

        if (avoid_prob <= Avoid)
        {
            // 회피 성공
            Console.WriteLine("회피했습니다.");
        }
        else
        {
            // 회피 실패
            Hp -= new_monster_damage;
            Console.WriteLine("회피에 실패하였습니다.");
            Console.WriteLine($"{Name}이 {new_monster_damage} 만큼의 피해를 입어 Hp가 {Hp}이 되었습니다. ");
        }
    }

    public virtual void ActiveSkill(Monster monster)
    {

    }

    public virtual void ActiveSkill(Monster monster1, Monster monster2)
    {

    }

    public virtual void UtilitySkill()
    {

    }

    public virtual void PassiveSkill()
    {

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