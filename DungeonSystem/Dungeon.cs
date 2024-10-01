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
    public List<Monster> Monsters_can_appear { get; set; }

    // 던전 설명
    public string Description { get; set; }

    // DungeonDatabase의 생성된 던전을 참고하는 초기화 생성자
    public Dungeon(Dungeon original)
    {
        DungeonDiffculty = original.DungeonDiffculty;
        Monsters_can_appear = new List<Monster>(original.Monsters_can_appear);
        Description = original.Description;
    }

    // DungeonDatabase에서 사용하는 초기 던전 초기화 생성자.
    public Dungeon(DungeonDiffculty diffculty, List<Monster> monsters, string description)
    {
        DungeonDiffculty = diffculty;
        Monsters_can_appear =  monsters;
        Description = description; 
    }
}

