﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTextRPG
{
    public class Monster
    {
        public int Tier { get; protected set; }
        public string Name { get; protected set; }
        public int Atk { get; protected set; }
        public int Def { get; protected set; }
        public int Hp { get; protected set; }
        //public int Mp { get; protected set; }
        public int Critical { get; private set; } = 15;
        public int Avoid { get; protected set; } = 10;
        public int Exp { get; protected set; }
        public Item[] DropItem { get; protected set; }

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
            Def = def;
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

            // 크리티컬 성공
            if (critical_prob <= Critical)
            {
                monster_damage = (int)Math.Round(RandomDamage() * 1.6f);
                player.Hp -= monster_damage;
                Console.WriteLine("치명타가 발동되었습니다.");
            }
            else
            {
                monster_damage = RandomDamage();
                player.Hp -= monster_damage;
                Console.WriteLine("치명타가 발동되지 않았습니다.");
            }

            Console.WriteLine($"{player.Name}에게 {monster_damage}의 피해를 입혀 Hp가 {player.Hp}가 되었습니다.");
        }

        public void MonsterDefense(int player_damage)
        {
            Random random = new Random();
            int avoid_prob = random.Next(1, 101);

            // 회피 성공
            if (avoid_prob <= Avoid)
            {
                Console.WriteLine("회피했습니다.");
            }
            else
            {
                Hp -= player_damage;
                Console.WriteLine("회피에 실패하였습니다.");
                Console.WriteLine($"{Name}이 {player_damage}만큼의 피해를 입어 HP가 {Hp}가 되었습니다.");
            }
        }
    }
}
