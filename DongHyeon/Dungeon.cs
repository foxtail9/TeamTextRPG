using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TeamTextRPG;

class Dungeon
{
    /// <summary>
    /// 
    /// // 난이도별 몬스터 배열(리스트)를 분리해둡니다.
    /// List<Monster> monsters_easy;
    /// List<Monster> monsters_normal;
    /// List<Monster> monsters_hard;
    /// 
    /// // 던전에서 마주치는 몬스터 배열입니다.
    /// List<Monster> monsters_in_battle;
    ///
    /// 
    /// </summary>

    public bool ready_attack { get; set; } = false;

    public Dungeon(int difficulty)
    {
        SettingBattleMonsters(difficulty);
    }

    public void DungeonSystem(Character Player)
    {
        while(true)
        {
            DisplayInDungeonBattle();

            int result = CheckInput(0, 0);

            switch (result)
            {
                case 0:

                    break;

                case 1:

                    break;

                case 2:

                    break;

                case 3:
                    break;
            }
        }
    }

    public void DisplayInDungeonBattle()
    {
        Console.Clear();
        Console.WriteLine("Battle!!");
        Console.WriteLine();

        // 현재 던전에 존재하는 몬스터 정보 출력
        DisplayMonstersInfo(ready_attack);
        Console.WriteLine();
        
        // 플레이어 현재 정보 출력
        Console.WriteLine("내정보");
        // ~~~~

        // 행동 선택
        Console.WriteLine();
        Console.WriteLine("1. 공격");
        Console.WriteLine("2. 스킬 사용");
        Console.WriteLine("3. 인벤토리");
        Console.WriteLine("4. 도망가기");
        Console.WriteLine();
        Console.WriteLine("원하시는 행동을 입력해주세요.");
    }

    void DisplayMonstersInfo(bool showIdx)
    {
        ///
        /// for(int i = 0; i < monsters_in_battle[i].Count; i++)
        /// {
        ///     string monster_info = "";
        ///     if(ready_attack)  monster_info += "(i+1).ToString() ";
        ///     
        ///     ~ 몬스터 정보 출력
        ///     ~ 레벨 / 이름 / 체력
        ///     
        /// }
        /// 
    }

    void SettingBattleMonsters(int difficulty)
    {
        Random rand = new Random();
        int spawn_num = rand.Next(1, 4);

        for (int i = 0; i < spawn_num; i++)
        {
            // 난이도 별 분리된 몬스터 배열에서 몬스터를 배치합니다. 
            // int random_monster = rand.Next();
            // monsters_in_battle[i].Add();
        }

        ready_attack = true;
    }

    static int CheckInput(int min, int max)
    {
        int result;
        while (true)
        {
            string input = Console.ReadLine();
            bool isNumber = int.TryParse(input, out result);
            if (isNumber)
            {
                if (result >= min && result <= max)
                    return result;
            }
            Console.WriteLine("잘못된 입력입니다!!!!");
        }
    }
}

