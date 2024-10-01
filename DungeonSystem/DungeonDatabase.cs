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

                new List<Monster>
                {
                    new Monster
                    (
                        3, "늑대", 20, 15, 40, 1, 100,
                        new Item[] 
                        {
                            new Item("커먼","목검", 0, 5, "단단한 목검이다.", 500)
                        },

                        new Drop[]
                        {
                            new Drop("HP 포션",0,"체력을 30회복 시킨다", 500),
                        }
                    ),
                    

                    new Monster
                    (
                        3, "여우", 5, 10, 20, 1, 50,
                        new Item[]
                        {
                            new Item("커먼","면 셔츠", 1, 3, "면으로 만든 셔츠다. 최소한의 방어력을 제공한다.", 500)
                        },

                        new Drop[]
                        {
                            new Drop("HP 포션",0,"체력을 30회복 시킨다", 500),
                        }
                    ),

                    new Monster
                    (
                        3, "멧돼지", 15, 20, 50, 1, 100,
                        new Item[]
                        {
                            new Item("희귀", "???", 1, 1, "1", 1)
                        },

                        new Drop[]
                        {
                            new Drop("HP 포션",0,"체력을 30회복 시킨다", 500),
                        }
                    ),

                    new Monster
                    (
                        3, "고블린", 10, 10, 30, 1, 80,
                        new Item[]
                        {
                            new Item("커먼","철검", 0, 6, "평범한 철검이다.", 800),
                            new Item("커먼","면 셔츠", 1, 3, "면으로 만든 셔츠다. 최소한의 방어력을 제공한다.", 500)
                        },

                        new Drop[]
                        {
                            new Drop("HP 포션",0,"체력을 30회복 시킨다", 500),
                        }
                    )
                },

                "엘프의 숲입니다. 다양한 야생동물과 고블린이 출현합니다."
            ),

            new Dungeon
            (
                DungeonDiffculty.Normal,

                new List<Monster>
                {
                    new Monster
                    (
                        2, "골렘", 30, 30, 40, 2, 200,
                        new Item[]
                        {
                            new Item("언커먼","재련된 강철검", 0, 12, "강철 검이라고 해서 다 같은 강철 검은 아니다", 1500),
                        },

                        new Drop[]
                        {
                            new Drop("HP 포션",0,"체력을 30회복 시킨다", 500),
                            new Drop("루비", 2,"붉은색의 보석이다. 체력를 10 올려준다",1000),
                        }
                    ),

                    new Monster
                    (
                        2, "오크", 30, 25, 50, 2, 180,
                        new Item[]
                        {
                            new Item("언커먼","재련된 강철검", 0, 12, "강철 검이라고 해서 다 같은 강철 검은 아니다", 1500),
                            new Item("언커먼","카타나", 0, 8, "날카로운 카타나다.", 1200),
                            new Item("언커먼","가죽 조끼", 1, 5, "가죽으로 만든 셔츠다. 낮은 방어력을 제공한다.", 800),
                            new Item("언커먼","가죽 스커트", 1, 5, "가죽으로 만든 치마다. 남자도 입을 수 있다.", 800),
                        },

                        new Drop[]
                        {
                            new Drop("HP 포션",0,"체력을 30회복 시킨다", 500),
                            new Drop("루비", 2,"붉은색의 보석이다. 체력를 10 올려준다",1000),
                        }
                    ),

                    new Monster
                    (
                        2, "고스트", 50, 20, 20, 2, 150,
                        new Item[]
                        {
                            new Item("언커먼","재련된 강철검", 0, 12, "강철 검이라고 해서 다 같은 강철 검은 아니다", 1500),
                            new Item("언커먼","가죽 조끼", 1, 5, "가죽으로 만든 셔츠다. 낮은 방어력을 제공한다.", 800),
                        },

                        new Drop[]
                        {
                            new Drop("MP 포션",1,"마나를 30회복 시킨다", 500),
                            new Drop("사파이어", 3,"푸른색의 보석이다. 마나를 10 올려준다",1000)
                        }
                    ),

                    new Monster
                    (
                        2, "좀비", 40, 20, 30, 2, 130,
                        new Item[]
                        {
                            new Item("언커먼","재련된 강철검", 0, 12, "강철 검이라고 해서 다 같은 강철 검은 아니다", 1500),
                            new Item("언커먼","가죽 조끼", 1, 5, "가죽으로 만든 셔츠다. 낮은 방어력을 제공한다.", 800),
                            new Item("언커먼","가죽 스커트", 1, 5, "가죽으로 만든 치마다. 남자도 입을 수 있다.", 800),
                        },

                        new Drop[]
                        {
                            new Drop("MP 포션",1,"마나를 30회복 시킨다", 500),
                        }
                    )
                },

                "저주받은 땅입니다. 골렘과 언데드들이 출현합니다."
            ),

            new Dungeon
            (
                DungeonDiffculty.Hard,

                new List<Monster>
                {
                    new Monster
                    (
                        1, "드래곤", 70, 70, 100, 3, 300,
                        new Item[]
                        {
                            new Item("레어","플레이트 아머", 1, 12, "고딕 양식 판금 갑옷이다.", 2000),
                            new Item("레어","체인메일", 1, 9, "철사 따위로 만든 고리를 엮은 사슬 형태로 된 갑옷이다.", 1500),
                            new Item("레어","초진동검", 0, 15, "첨단 기술이 적용된 검이다.", 2000),
                            new Item("레어","카타나", 0, 17, "작열하는 불의 기운을 담고 있는 도.", 2500),
                        },

                        new Drop[]
                        {
                            new Drop("HP 포션",0,"체력을 30회복 시킨다", 500),
                            new Drop("MP 포션",1,"마나를 30회복 시킨다", 500),
                            new Drop("루비", 2,"붉은색의 보석이다. 체력를 10 올려준다",1000),
                            new Drop("사파이어", 3,"푸른색의 보석이다. 마나를 10 올려준다",1000)
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