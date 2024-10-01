using System.ComponentModel;
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
    public int Exp { get; protected set; } = 0;

    public int MaxHp { get; protected set; }
    public int MaxMp { get; protected set; }

    public bool IsInvincible { get; protected set; } = false;
    public bool IsHawkeye { get; protected set; } = false;
    public bool IsRegenerateMp { get; protected set; } = false;
    public bool OnPassive { get; protected set; } = false;
    public bool IsDie { get; protected set; } = false;

    public int ExtraAtk { get; private set; }
    public int ExtraDef { get; private set; }

    private List<Item> Inventory = new List<Item>();
    private List<Drop> DropInventory = new List<Drop>();
    private List<Item> EquipList = new List<Item>();
    public Item EquipWeapon { get; set; }
    public Item EquipArmor { get; set; }
    public Dictionary<string, int> RequiredMonsterNames { get; set; } = new Dictionary<string, int>();
    public List<Quest> PlayerQuestList { get; set; } = new List<Quest>();
    public List<Quest> PlayerCompletedQuests = new List<Quest>();
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
            float targetItemValue = Inventory[i].Value * 0.8f;
            int sellValue = int.Parse(targetItemValue.ToString("0"));

            string displayIdx = showIdx ? $"{i + 1} " : "";
            string displayEquipped = "";
            if (EquipArmor == Inventory[i] || EquipWeapon == Inventory[i])
            {
                Console.ForegroundColor = ConsoleColor.Green;
                displayEquipped = "[E]";
            }
            Console.WriteLine($"- {displayIdx}{displayEquipped} {targetItem.ItemInfoText()}  |  {sellValue}");
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

        float targetItemValue = Inventory[i].Value * 0.8f;
        int sellValue = int.Parse(targetItemValue.ToString("0"));
        if (EquipArmor == Inventory[i] || EquipWeapon == Inventory[i])
        {
            Console.WriteLine("착용중인 아이템입니다");
            Console.WriteLine("Enter 를 눌러주세요.");
            Console.ReadLine();
        }
        else
        {
            Inventory.RemoveAt(i);
            Gold += sellValue;
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
        Hp = MaxHp;
        Mp = MaxMp;
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

        DisplayPlayerColorString(Name, ConsoleColor.Cyan);
        Console.WriteLine("의 공격!");

        if (monster.CheckMonsterAvoid(IsHawkeye))
        {
            Console.Write($"Tier.{monster.Tier}");
            DisplayPlayerColorString(monster.Name, ConsoleColor.Green);
            Console.WriteLine("을(를) 공격했지만 아무일도 일어나지 않았습니다.");
            return;
        }

        Console.Write($"Tier.{monster.Tier} ");
        DisplayPlayerColorString(monster.Name, ConsoleColor.Green);
        Console.Write("을(를) 맞췄습니다. ");

        if (critical_prob <= Critical)
        {
            // 치명타
            player_damage = (int)Math.Round(RandomDamage() * 1.6f);
            Console.Write($"[데미지 : ");
            DisplayPlayerColorString(player_damage.ToString(), ConsoleColor.Red);
            Console.WriteLine("] - 치명타 공격!!");
        }
        else
        {
            // 평타
            player_damage = RandomDamage();
            Console.Write($"[데미지 : ");
            DisplayPlayerColorString(player_damage.ToString(), ConsoleColor.Red);
            Console.WriteLine("]"); 
        }
        Console.WriteLine($"{monster.Name}이 {player_damage} 만큼의 피해를 입어 Hp가 {monster.Hp}이 되었습니다. \n");

        
        monster.MonsterDefense(player_damage);
    }

    public void PlayerDefense(int monster_damage)
    {
        int new_monster_damage = monster_damage - Def;
        new_monster_damage = monster_damage > 0 ? monster_damage : 0;

        Console.Write($"Lv.{Level} ");
        DisplayPlayerColorString(Name, ConsoleColor.Cyan, true);

        Console.Write("HP ");
        DisplayPlayerColorString(Hp.ToString(), ConsoleColor.Red);
        Hp -= new_monster_damage;
        Console.Write($" -> ");

        if (Hp <= 0)
        {
            Hp = 0;
            IsDie = true;
            DisplayPlayerColorString("Dead", ConsoleColor.DarkGray, true);
        }
        else DisplayPlayerColorString(Hp.ToString(), ConsoleColor.Red, true);
    }

    public bool CheckPlayerAvoid()
    {
        Random random = new Random();
        int avoid_prob = random.Next(1, 101);

        if (avoid_prob <= Avoid) return true;
        else return false;
    }

    public bool CheckMana(int player_mana)
    {
        if (Mp < player_mana)
        {
            DisplayPlayerColorString(Mp.ToString(), ConsoleColor.Blue);
            Console.WriteLine("가 부족합니다.");
            return false;
        }
        return true;
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
            if(this.Level >= quest.RequiredLevel)
            {
                PlayerQuestList.Add(quest);
                Console.WriteLine($"{quest.questname} 퀘스트가 추가되었습니다.");
                // 잡아야 할 몬스터 리스트에 추가
                if (!string.IsNullOrEmpty(quest.RequiredMonsterType))
                {
                    if (!RequiredMonsterNames.ContainsKey(quest.RequiredMonsterType))
                    {
                        RequiredMonsterNames.Add(quest.RequiredMonsterType, quest.RequiredMonsterCount);
                    }
                }
            }
            else
            {
                Console.WriteLine($"요구 레벨이 부족합니다!");
            }
        }
        else
        {
            Console.WriteLine("이미 해당 퀘스트를 진행하고 있습니다.");
        }
    }
    public void UpdateQuestProgress(string monsterName)
    {
        Console.WriteLine("퀘스트 확인 시퀀스 시작");
        //// 완료된 퀘스트를 저장할 리스트
        List<Quest> completedQuests = new List<Quest>();

        foreach (var quest in PlayerQuestList)
        {
            // 해당 퀘스트의 타겟 몬스터가 맞는지 확인
            if (quest.RequiredMonsterType == monsterName)
            {
                quest.RequiredMonsterCount--; // 현재 잡은 몬스터 수 감소
                Console.WriteLine($"{quest.questname} 퀘스트: {monsterName}을(를) 잡았습니다. 남은 몬스터 수: {quest.RequiredMonsterCount}");

                // 퀘스트 목표 달성 여부 확인
                if (quest.RequiredMonsterCount <= 0)
                {
                    Console.WriteLine($"{quest.questname} 퀘스트 완료!");

                    this.Gold += quest.GoldReward; //골드보상
                    //this.Exp += quest.GoldReward; 경험치 보상
                    Inventory.Add(quest.RewardItem);

                    // 완료된 퀘스트 리스트에 추가
                    completedQuests.Add(quest);
                }
            }
        }

        // 완료된 퀘스트 삭제
        foreach (var completedQuest in completedQuests)
        {
            // PlayerQuestList에서 완료된 퀘스트 삭제
            PlayerQuestList.Remove(completedQuest);
            PlayerCompletedQuests.Add(completedQuest); // 완료된 퀘스트는 따로 보관 가능

            // RequiredMonsterNames에서 해당 몬스터 삭제
            if (RequiredMonsterNames.ContainsKey(completedQuest.RequiredMonsterType))
            {
                RequiredMonsterNames.Remove(completedQuest.RequiredMonsterType);
            }
        }
    }
    public void DisplayPlayerColorString(string str, ConsoleColor color, bool new_line = false)
    {
        Console.ForegroundColor = color;
        if(new_line) Console.WriteLine(str);
        else Console.Write(str);
        Console.ResetColor();
    }
    public void UpdatePlayerExp(Monster monster)
    {
        if (monster.IsDie) Exp += monster.Exp;
        CalcPlayerLevelUp();
    }
    public virtual void CalcPlayerLevelUp()
    {
        if ((Level * 10) < Exp)
        {
            Level++;
            Console.Write($"레벨업하여 ");
            DisplayPlayerColorString(Name, ConsoleColor.Cyan);
            Console.WriteLine($"(이/가) {Level} 레벨이 되었습니다.");
            Exp -= Level * 10;
        }
    }
}