﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
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
        }

        public override void ActiveSkill(List<int> selectIdxs, List<Monster> monsters)
        {
            // selectIdxs 1번 -> monsters 0번 인덱스
            if (selectIdxs.Count == 1)
            {
                FireBall(monsters[selectIdxs[0] - 1]); 
            }
            else if (selectIdxs.Count == 2)
            {
                WaterBomb(new List<Monster> { monsters[selectIdxs[0] - 1], monsters[selectIdxs[1] - 1] });
            }
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
            Console.WriteLine();

            int fireball_damage = RandomDamage() * 2;

            Console.Write($"Lv.{Level} ");
            DisplayPlayerColorString(Name, ConsoleColor.Cyan, true);
            Console.Write("MP ");
            DisplayPlayerColorString(Mp.ToString(), ConsoleColor.Blue);
            Mp -= 5;
            Console.Write($" -> ");
            DisplayPlayerColorString(Mp.ToString(), ConsoleColor.Blue, true);

            DiaplyerSkillDamage(monster, fireball_damage);
            monster.MonsterDefense(fireball_damage);
        }

        public void WaterBomb(List <Monster> monsters)
        {
            // 몬스터 2마리 공격
            // 공격력 100% 피해, 마나 소모 5
            if (CheckMana(5) == false) return;

            Console.WriteLine("워터밤을 사용했습니다.");
            Console.WriteLine();

            Console.Write($"Lv.{Level} ");
            DisplayPlayerColorString(Name, ConsoleColor.Cyan, true);

            Console.Write("MP ");
            DisplayPlayerColorString(Mp.ToString(), ConsoleColor.Blue);
            Mp -= 5;
            Console.Write($" -> ");
            DisplayPlayerColorString(Mp.ToString(), ConsoleColor.Blue, true);

            foreach(Monster monster in monsters)
            {
                int water_bomb_damage = RandomDamage();
                DiaplyerSkillDamage(monster, water_bomb_damage);
                monster.MonsterDefense(water_bomb_damage);
            }
        }

        public void RegenerateMp()
        {
            // 마나 재생 (전투 당 1회)

            if (IsRegenerateMp) Console.WriteLine("이번 전투에 마나재생을 사용했습니다.");
            else
            {
                Console.WriteLine("마나재생을 사용했습니다.");
                Console.Write($"Lv.{Level} ");
                DisplayPlayerColorString(Name, ConsoleColor.Cyan, true);
                IsRegenerateMp = true;
                Console.Write("MP ");
                DisplayPlayerColorString(Mp.ToString(), ConsoleColor.Blue);
                Mp = MAGE_MAX_MP;
                Console.Write($" -> ");
                DisplayPlayerColorString(Mp.ToString(), ConsoleColor.Blue, true);
            }
            Console.WriteLine();
            Thread.Sleep(1000);
        }

        public override void CalcPlayerLevelUp()
        {
            if ((Level * 10) <= Exp)
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
}
