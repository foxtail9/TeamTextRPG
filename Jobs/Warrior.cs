using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TeamTextRPG.Jobs
{

    class Warrior : Character
    {
        const int WARRIOR_MAX_HP = 200;

        // Job "전사" Atk 40, Def 60, Hp 200, Mp 50, Gold 1500
        public Warrior(int level, string name, int gold)
        {
            Level = level;
            Name = name;
            Job = "전사";
            Atk = 40;
            Def = 60;
            MaxHp = 200;
            Hp = 200;
            MaxMp = 50;
            Mp = 50;
            Gold = gold;
        }

        public override void ActiveSkill(List<int> selectIdxs, List<Monster> monsters)
        {
            PowerfulShot(monsters[selectIdxs[0] - 1]); // selectIdxs 1번 -> monsters 0번 인덱스
        }

        public override void UtilitySkill()
        {
            Guard();
        }

        public override void PassiveSkill()
        {
            Adrenaline();
        }

        public void PowerfulShot(Monster monster)
        {
            // 공격력 300% 피해, 마나소모 5
            if (CheckMana(5) == false) return;

            Console.WriteLine("강력한 한방을 사용했습니다.");
            Console.WriteLine();
            int power_shot_damage = RandomDamage() * 3;

            Console.Write($"Lv.{Level} ");
            DisplayPlayerColorString(Name, ConsoleColor.Cyan, true);
            Console.Write("MP ");
            DisplayPlayerColorString(Mp.ToString(), ConsoleColor.Blue);
            Mp -= 5;
            Console.Write($" -> ");
            DisplayPlayerColorString(Mp.ToString(), ConsoleColor.Blue, true);
            DiaplyerSkillDamage(power_shot_damage);
            monster.MonsterDefense(power_shot_damage);
        }

        public void Guard()
        {
            // 다음 턴의 데미지 0 - 마나소모 10
            if (CheckMana(10) == false) return;
            Console.WriteLine("가드를 사용했습니다.");
            Console.WriteLine();
            Console.Write("MP ");
            DisplayPlayerColorString(Mp.ToString(), ConsoleColor.Blue);
            Mp -= 10;
            Console.Write($" -> ");
            DisplayPlayerColorString(Mp.ToString(), ConsoleColor.Blue, true);
            IsInvincible = true;
            Console.WriteLine();
        }

        public void Adrenaline()
        {
            // 체력이 50% 미만일 때 공격력이 1.5배 향상됩니다.
            if (!OnPassive && Hp <= (WARRIOR_MAX_HP / 2)) {
                // 체력 50% 이하일 때 공격력 50% 증가  
                Console.WriteLine("아드레날린이 활성화 되었습니다.");
                Console.WriteLine();
                Atk += Atk / 2;
                OnPassive = true;
            }
            else if (OnPassive && Hp > (WARRIOR_MAX_HP / 2)){
                Console.WriteLine("아드레날린이 해제 되었습니다.");
                Console.WriteLine();
                Atk -= Atk / 2;
                OnPassive = false;
            }
        }

        public override void CalcPlayerLevelUp()
        {
            if (Level * 10 < Exp)
            {
                base.CalcPlayerLevelUp();
                MaxHp += 10;
                Hp = MaxHp;
                MaxMp += 5;
                Mp = MaxMp;
                Atk += 5;
                Def += 10;
            }
        }
    }
}
