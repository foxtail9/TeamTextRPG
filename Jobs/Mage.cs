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
            Hp = 100;
            Mp = 150;
            Gold = gold;
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
            int fire_ball_damage = RandomDamage() * 2;
            Mp -= 5;
            monster.MonsterDefense(fire_ball_damage, IsHawkeye);
        }

        public void WaterBomb(Monster monster1, Monster monster2)
        {
            // 몬스터 2마리 공격
            // 공격력 100% 피해, 마나 소모 5
            monster1.MonsterDefense(Atk, IsHawkeye);
            monster2.MonsterDefense(Atk, IsHawkeye);
        }

        public void RegenerateMp()
        {
            // 마나 재생 (전투 당 1회)
            Mp = MAGE_MAX_MP;
        }
    }
}
