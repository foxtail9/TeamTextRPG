using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TeamTextRPG;

public enum DungeonDiffculty
{
    Easy,
    Normal,
    Hard
}
public class Dungeon
{
    // 던전 난이도
    public DungeonDiffculty DungeonDiffculty { get; set; } 

    // 출현 몬스터 정보
    // public List<Monster> Monsters_can_appear { get; set; }

    // 던전 설명
    public string Description { get; set; }


    // public Dungeon(DungeonDiffculty diffculty, List<Monster> monsters, string description) 로 바꿔줄 것임.
    public Dungeon(DungeonDiffculty diffculty)
    {
        // Monsters_can_appear =  monsters;
        // Description = description; 
    }
}

