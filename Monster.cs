using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TeamTextRPG
{
    public class Monster
    {
        public int Tier { get; }
        public string Name { get; }
        public int Atk { get;  }
        public int Hp { get; set; }
        //public int Mp { get;  }
        public int Critical { get; } = 15;
        public int Avoid { get; } = 10;
        public int Exp { get; }
        
        public bool IsDie { get; private set; }
        public Item[] DropItem { get; }

        //      티어 공격력 방어력 hp  exp
        // 늑대	3	20	15	40	1
        // 여우	3	5	10	20	1
        // 멧돼지	3	15	20	50	1
        // 고블린	3	10	10	30	1
        // 
        // 골렘	2	30	30	40	2
        // 오크	2	30	25	50	2
        // 고스트	2	50	20	20	2
        // 좀비	2	40	20	30	2
        // 
        // 드래곤	1	70	70	100	3

        public Monster(int tier, string name, int atk, int def, int hp, int exp, Item[] dropItem)
        {
            Tier = tier;
            Name = name;
            Atk = atk;
            Hp = hp;
            Exp = 1;
            DropItem = dropItem;
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

        public void MonsterAttack(Character player)
        {
            Random random = new Random();
            int critical_prob = random.Next(1, 101);
            int monster_damage;

            Console.Write($"Tier.{Tier} ");
            DisplayMonsterColorString(Name, ConsoleColor.Green);
            Console.WriteLine("의 공격!");

            if (player.CheckPlayerAvoid() == true || player.IsInvincible == true)
            {
                DisplayMonsterColorString(player.Name, ConsoleColor.Cyan);
                if(player.IsInvincible == true) Console.WriteLine("이(가) Guard 스킬을 사용했으므로 데미지를 받지 않습니다.");
                else Console.WriteLine("을(를) 공격했지만 아무일도 일어나지 않았습니다.");
                return;
            }

            DisplayMonsterColorString(player.Name.ToString(), ConsoleColor.Cyan);
            Console.Write("을(를) 맞췄습니다.");

            if (critical_prob <= Critical)
            {
                // 치명타
                monster_damage = (int)Math.Round(RandomDamage() * 1.6f);
                Console.Write($"[데미지 : ");
                DisplayMonsterColorString(monster_damage.ToString(), ConsoleColor.Red);
                Console.WriteLine("] - 치명타 공격!!");
            }
            else
            {
                // 평타
                monster_damage = RandomDamage();
                Console.Write($"[데미지 : ");
                DisplayMonsterColorString(monster_damage.ToString(), ConsoleColor.Red);
                Console.WriteLine("]");
            }
            player.PlayerDefense(monster_damage);
        }

        public void MonsterDefense(int player_damage)
        {
            Console.Write($"Tier.{Tier} ");
            DisplayMonsterColorString(Name, ConsoleColor.Green, true);

            Console.Write("HP ");
            DisplayMonsterColorString(Hp.ToString(), ConsoleColor.Red);
            Hp -= player_damage;
            Console.Write($" -> ");

            if (Hp <= 0)
            {
                Hp = 0;
                IsDie = true;
                DisplayMonsterColorString("Dead", ConsoleColor.DarkGray, true);
            }
            else DisplayMonsterColorString(Hp.ToString(), ConsoleColor.Red, true);
        }

        public bool CheckMonsterAvoid(bool is_hawkeye)
        {
            if (is_hawkeye == true) return false;
            Random random = new Random();
            int avoid_prob = random.Next(1, 101);

            if (avoid_prob <= Avoid) return true;
            else return false;
        }

        public void DisplayMonsterColorString(string str, ConsoleColor color, bool newLine=false)
        {
            Console.ForegroundColor = color;
            if(newLine == true) Console.WriteLine(str);
            else Console.Write(str);
            Console.ResetColor();
        }
    }
}
