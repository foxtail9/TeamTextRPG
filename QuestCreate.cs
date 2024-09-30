public enum QuestType
{
    일반,
    반복,
    스토리
}

public class Quest
{
    public QuestType questype { get; set; }  // 퀘스트 타입
    public string questname { get; set; }     // 퀘스트 이름
    public string questDescription { get; set; }  // 퀘스트 내용
    public bool IsInProgress { get; set; } = false; // 퀘스트 진행 상태
    public int RequiredMonsterCount { get; set; } // 필요 몬스터 수
    public string RequiredMonsterType { get; set; } // 필요 몬스터 종류
    public int GoldReward { get; set; } // 골드 보상
    public int ExpReward { get; set; } // 경험치 보상
    public Item RewardItem { get; set; }  // 보상 아이템
    public int CurrentMonsterCount { get; private set; } // 현재 처치한 몬스터 수

    public Quest(QuestType type, string name, string description, int requiredMonsterCount, string requiredMonsterType, int goldReward, int expReward, string rewardItem)
    {
        questype = type;
        questname = name;
        questDescription = description;
        RequiredMonsterCount = requiredMonsterCount;
        RequiredMonsterType = requiredMonsterType;
        GoldReward = goldReward;
        ExpReward = expReward;
        RewardItem = rewardItem;
        IsInProgress = false;
        CurrentMonsterCount = 0;
    }

    public void DisplayQuest()
    {
        Console.WriteLine($"[{questype}] | {questname} | {questDescription} | 보상: {GoldReward} 골드, {ExpReward} 경험치, 아이템: {RewardItem}");
    }

    public void DefeatMonster(string monsterType)
    {
        if (monsterType == RequiredMonsterType)
        {
            CurrentMonsterCount++;
            Console.WriteLine($"{monsterType} 처치! 현재 처치한 몬스터 수: {CurrentMonsterCount}/{RequiredMonsterCount}");

            // 퀘스트 완료 체크
            if (CurrentMonsterCount >= RequiredMonsterCount)
            {
                IsInProgress = false;
                Console.WriteLine($"퀘스트 '{questname}' 완료!");
            }
        }
    }

}

public static class QuestDatabase
{
    public static List<Quest> Quests { get; private set; }
    var sword = new Item("희귀", "철검", 0, 10, "강력한 철로 만든 검입니다.", 150);

    // 스태틱 생성자: 프로그램 시작 시 자동으로 실행되어 퀘스트들을 한꺼번에 초기화
    static QuestDatabase()
    {
        Quests = new List<Quest>
        {
            new Quest(
                QuestType.일반,
                "늑대 소굴",
                "엘프의 숲에서 늑대 5마리를 처치해주세요.",
                5, // 필요 몬스터 수
                "Wolf", // 필요 몬스터 종류
                100, // 보상: 골드
                50, // 보상: 경험치
                sword // 보상: 아이템
            ),
            new Quest(
                QuestType.일반,
                "숲속의 소문",
                "마을 소문에 의하면 숲속에 고블린이 나온다던데...",
                1, // 필요 몬스터 수
                "Goblin", // 예시로 고블린 추가
                50, // 보상: 골드
                225, // 보상: 경험치
                "가죽 조끼" // 보상: 아이템
            ),
            new Quest(
                QuestType.반복,
                "골렘 소탕",
                "저주받은 마력으로 인해 골렘들이 증식한다. 처리하자",
                10, // 필요 몬스터 수
                "Golem", // 예시로 골렘 추가
                200, // 보상: 골드
                100, // 보상: 경험치
                "" // 보상: 아이템
            ),
            new Quest(
                QuestType.스토리,
                "악의 원천",
                "함락한 성 탐험하기",
                0, // 필요 몬스터 수
                "", // 몬스터 없음
                300, // 보상: 골드
                150, // 보상: 경험치
                "HP 포션" // 보상: 아이템
            )
        };
    }

    // 퀘스트 이름으로 퀘스트 가져오기
    public static Quest GetQuestByName(string questName)
    {
        return Quests.Find(q => q.questname == questName);
    }
}