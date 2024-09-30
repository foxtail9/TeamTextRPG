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
            new Dungeon
            (
                DungeonDiffculty.Easy,

                // 현재 임시로 작성한 상황입니다. 클래스 멤버 접근 및 추가 함수가 생긴다면 수정하면 좋을 것 같습니다.
                new List<Monster>
                {
                    new Monster
                    (
                        3, "늑대", 20, 15, 40, 1, 
                        new Item[] 
                        {
                            new Item("희귀", "???", 1, 1, "1", 1)
                        }
                    ),
                    
                    new Monster
                    (
                        3, "여우", 5, 10, 20, 1,
                        new Item[]
                        {
                            new Item("희귀", "???", 1, 1, "1", 1)
                        }
                    ),

                    new Monster
                    (
                        3, "멧돼지", 15, 20, 50, 1,
                        new Item[]
                        {
                            new Item("희귀", "???", 1, 1, "1", 1)
                        }
                    ),

                    new Monster
                    (
                        3, "고블린", 10, 10, 30, 1,
                        new Item[]
                        {
                            new Item("희귀", "???", 1, 1, "1", 1)
                        }
                    )
                },

                "엘프의 숲입니다. 다양한 야생동물과 고블린이 출현합니다."
            ),

            new Dungeon
            (
                DungeonDiffculty.Normal,

                // 현재 임시로 작성한 상황입니다. 클래스 멤버 접근 및 추가 함수가 생긴다면 수정하면 좋을 것 같습니다.
                new List<Monster>
                {
                    new Monster
                    (
                        2, "골렘", 30, 30, 40, 2,
                        new Item[]
                        {
                            new Item("희귀", "???", 1, 1, "1", 1)
                        }
                    ),

                    new Monster
                    (
                        2, "오크", 30, 25, 50, 2,
                        new Item[]
                        {
                            new Item("희귀", "???", 1, 1, "1", 1)
                        }
                    ),

                    new Monster
                    (
                        2, "고스트", 50, 20, 20, 2,
                        new Item[]
                        {
                            new Item("희귀", "???", 1, 1, "1", 1)
                        }
                    ),

                    new Monster
                    (
                        2, "좀비", 40, 20, 30, 2,
                        new Item[]
                        {
                            new Item("희귀", "???", 1, 1, "1", 1)
                        }
                    )
                },

                "저주받은 땅입니다. 골렘과 언데드들이 출현합니다."
            ),

            new Dungeon
            (
                DungeonDiffculty.Hard,

                // 현재 임시로 작성한 상황입니다. 클래스 멤버 접근 및 추가 함수가 생긴다면 수정하면 좋을 것 같습니다.
                new List<Monster>
                {
                    new Monster
                    (
                        1, "드래곤", 70, 70, 100, 3,
                        new Item[]
                        {
                            new Item("희귀", "???", 1, 1, "1", 1)
                        }
                    )
                },

                "함락된 성입니다. 무너진 성에는 용이 살고 있습니다."
            ),
        };
    }

    // ~ 추가 기능 / 난이도 별 던전 가져오기
    // ~ 추가 기능 / 해당 몬스터 출현 던전 검색하기
}