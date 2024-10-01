using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace TeamTextRPG.Jobs
{
    class Archer : Character
    {
        // Job "궁수", Atk 40, Def 60, Hp 150, Mp 100, Gold 1500
        public Archer(int level, string name, int gold)
        {
            Level = level;
            Name = name;
            Job = "궁수";
            Atk = 40;
            Def = 60;
            MaxHp = 150;
            Hp = 150;
            MaxMp = 100;
            Mp = 100;
            Gold = gold;
            FastMovement();
        }

        public override void ActiveSkill(Monster monster)
        {
            DoubleShot(monster);
        }

        public override void UtilitySkill(Monster monster)
        {
            Hawkeye(monster);
        }

        public void DoubleShot(Monster monster)
        {
            // 공격력의 200% 피해 마나소모 10
            if (CheckMana(10) == false) return;

            Console.WriteLine("더블샷을 사용했습니다.");
            int double_shot_damage = RandomDamage() * 2;

            Console.Write("MP ");
            DisplayPlayerColorString(Mp.ToString(), ConsoleColor.Blue);
            Mp -= 10;
            monster.MonsterDefense(double_shot_damage);
            Console.Write($" -> ");
            DisplayPlayerColorString(Mp.ToString(), ConsoleColor.Blue, true);
        }

        public void Hawkeye(Monster monster)
        {
            // 몬스터의 미스 확률 무시 마나소모 5
            Console.WriteLine("호크아이를 발동했습니다.");
            IsHawkeye = true;

            Console.Write("MP ");
            DisplayPlayerColorString(Mp.ToString(), ConsoleColor.Blue);
            Mp -= 10;
            monster.MonsterDefense(Atk);
            Console.Write($" -> ");
            DisplayPlayerColorString(Mp.ToString(), ConsoleColor.Blue, true);
        }

        public void FastMovement()
        {
            // 몬스터 공격 회피 확률 증가 [패시브]
            Console.WriteLine();
            Console.WriteLine("신속한 이동이 발동됩니다.");
            Avoid += 20;
            Console.WriteLine($"몬스터의 공격을 회피할 확률이 {Avoid}이 되었습니다.");
            Console.WriteLine("Enter 를 눌러주세요.");
            Console.ReadLine();
        }

        public override void CalcPlayerLevelUp()
        {
            base.CalcPlayerLevelUp();
            MaxHp += 5;
            Hp = MaxHp;
            MaxMp += 5;
            Mp = MaxMp;
            Atk += 15;
            Def += 5;
        }
    }
}
