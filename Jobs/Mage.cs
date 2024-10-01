using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TeamTextRPG.Jobs
{
    class Mage : Character
    {
        const int MAGE_MAX_MP = 150;

        // Job "마법사", Atk 20, Def 20, Hp 100, Mp 150, Gold 1500
        public Mage(int level, string name, int gold)
        {
            Level = level;
            Name = name;
            Job = "마법사";
            Atk = 20;
            Def = 20;
            MaxHp = 100;
            Hp = 100;
            MaxMp = 150;
            Mp = 150;
            Gold = gold;
            PlayerQuestList = new List<Quest>();
        }

        public override void ActiveSkill(Monster monster)
        {
            FireBall(monster);
        }

        public override void ActiveSkill(Monster monster1, Monster monster2)
        {
            WaterBomb(monster1, monster2);
        }

        public override void UtilitySkill()
        {
            RegenerateMp();
        }

        public void FireBall(Monster monster)
        {
            // 공격력 200% 피해, 마나소모 5
            if (CheckMana(5) == false) return;
            
            Console.WriteLine("파이어볼을 사용했습니다.");
            int fire_ball_damage = RandomDamage() * 2;
            Console.Write("MP ");
            DisplayPlayerColorString(Mp.ToString(), ConsoleColor.Blue);
            Mp -= 5;
            monster.MonsterDefense(fire_ball_damage);
            Console.Write($" -> ");
            DisplayPlayerColorString(Mp.ToString(), ConsoleColor.Blue, true);
        }

        public void WaterBomb(Monster monster1, Monster monster2)
        {
            // 몬스터 2마리 공격
            // 공격력 100% 피해, 마나 소모 5
            if (CheckMana(5) == false) return;

            Console.WriteLine("워터밤을 사용했습니다.");
            Console.Write("MP ");
            DisplayPlayerColorString(Mp.ToString(), ConsoleColor.Blue);
            monster1.MonsterDefense(Atk);
            monster2.MonsterDefense(Atk);
            Console.Write($" -> ");
            DisplayPlayerColorString(Mp.ToString(), ConsoleColor.Blue, true);
        }

        public void RegenerateMp()
        {
            // 마나 재생 (전투 당 1회)
            Console.WriteLine("마나재생을 사용했습니다.");

            if (IsRegenerateMp) Console.WriteLine("이번 전투에 마나재생을 사용했습니다.");
            else
            {
                IsRegenerateMp = true;
                Console.Write("MP ");
                DisplayPlayerColorString(Mp.ToString(), ConsoleColor.Blue);
                Mp = MAGE_MAX_MP;
                Console.Write($" -> ");
                DisplayPlayerColorString(Mp.ToString(), ConsoleColor.Blue, true);
            }
        }

        public override void CalcPlayerLevelUp()
        {
            base.CalcPlayerLevelUp();
            MaxHp += 5;
            Hp = MaxHp;
            MaxMp += 15;
            Mp = MaxMp;
            Atk += 5;
            Def += 5;
        }
    }
}
