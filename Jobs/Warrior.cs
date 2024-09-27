using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
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
            Hp = 200;
            Mp = 50;
            Gold = gold;
        }

        public override void ActiveSkill(Monster monster)
        {
            PowerfulShot(monster);
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
            int power_shot_damage = RandomDamage() * 3;
            Mp -= 5;
            monster.MonsterDefense(power_shot_damage);
        }

        public void Guard()
        {
            // 다음 턴의 데미지 0
            IsInvincible = true;
        }

        public void Adrenaline()
        {
            if (!OnPassive) {
            // 체력 50% 이하일 때 공격력 50% 증가
                if(Hp <= (WARRIOR_MAX_HP / 2))
                {
                    Atk += Atk / 2;
                    OnPassive = true;
                }
            }
        }
    }
}
