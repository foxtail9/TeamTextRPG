namespace TeamTextRPG;
public static class DungeonDatabase
{
    // 스태틱 생성자를 통해 시작 시 초기화를 진행함.
    public static List<Dungeon> Dungeons { get; private set; }


    // 던전
    static DungeonDatabase()
    {
        Dungeons = new List<Dungeon>
        {
            // ~ 난이도 초기화
            new Dungeon
            (
                DungeonDiffculty.Easy,

                // 현재 임시로 작성한 상황입니다. 클래스 멤버 접근 추가 함수가 생긴다면 수정하면 좋을 것 같습니다.
                new List<Monster>
                {
                    new Monster
                    (
                        3, "늑대", 20, 15, 40, 1, 
                        new Item[] 
                        {
                            new Item("희귀", "???", 1, 1, "1", 1)
                        }
                    )   
                },

                "어두운 숲입니다. 다양한 야생동물과 고블린이 출현합니다."
            ),
        };
    }

    // ~ 추가 기능 / 난이도 별 던전 가져오기
    // ~ 추가 기능 / 해당 몬스터 출현 던전 검색하기
}