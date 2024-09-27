using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace TeamTextRPG.Jobs
{
    class Archer : Character
    {
        public bool IsHawkeye { get; set; } = false;

        // Job "궁수", Atk 40, Def 60, Hp 150, Mp 100, Gold 1500
        public Archer(int level, string name, int gold)
        {
            Level = level;
            Name = name;
            Job = "궁수";
            Atk = 40;
            Def = 60;
            Hp = 150;
            Mp = 100;
            Gold = gold;
            FastMovement();
        }

        public override void ActiveSkill(Monster monster)
        {
            DoubleShot(monster);
        }

        public override void UtilitySkill()
        {
            Hawkeye();
        }

        public void DoubleShot(Monster monster)
        {
            // 공격력의 200% 피해 마나소모 10
            int double_shot_damage = RandomDamage() * 2;
            Mp -= 10;
            monster.MonsterDefense(double_shot_damage);
        }

        public void Hawkeye()
        {
            // 몬스터의 미스 확률 무시
            IsHawkeye = true;
        }

        public void FastMovement()
        {
            // 몬스터 공격 회피 확률 증가 [패시브]
            Avoid += 20;
            Console.WriteLine($"몬스터의 공격을 회피할 확률이 {Avoid}가 되었습니다.");
        }
    }
}
