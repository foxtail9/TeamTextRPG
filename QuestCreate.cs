﻿public enum QuestType
{
    일반,
    반복,
    스토리
}

public class Quest
{
    public QuestType QuestType { get; set; }  // 퀘스트 타입
    public string QuestName { get; set; }    // 퀘스트 이름
    public string QuestDescription { get; set; }  // 퀘스트 내용
    public bool IsInProgress { get; set; } = false; // 퀘스트 진행 상태
    public int RequiredMonsterCount { get; set; } // 필요 몬스터 수
    public string RequiredMonsterType { get; set; } // 필요 몬스터 종류
    public int GoldReward { get; set; } // 골드 보상
    public int ExpReward { get; set; } // 경험치 보상
    public Item RewardItem { get; set; }  // 보상 아이템
    public int RequiredLevel { get; set; } // 요구 레벨

    public Quest(QuestType questType, string questName, string questDescription, int requiredMonsterCount, string requiredMonsterType, int goldReward, int expReward, Item rewardItem, int requiredLevel)
    {
        QuestType = questType;
        QuestName = questName;
        QuestDescription = questDescription;
        RequiredMonsterCount = requiredMonsterCount;
        RequiredMonsterType = requiredMonsterType;
        GoldReward = goldReward;
        ExpReward = expReward;
        RewardItem = rewardItem;
        IsInProgress = false;
        RequiredLevel = requiredLevel;
    }

    public void DisplayQuest()
    {
        Console.WriteLine($"[{QuestType}] | {QuestName} | {QuestDescription} | 보상: {GoldReward} 골드, {ExpReward} 경험치, 아이템: {RewardItem}");
    }
}

public static class QuestDatabase
{
    public static List<Quest> Quests { get; private set; }

    static QuestDatabase()
    {
        var sword = new Item("커먼","퀘스트로 받은 철검", 0, 10,"강력한 철로 만든 검입니다.", 150);
        var teeshirt = new Item("커먼","퀘스트로 받은 면 셔츠", 1, 3,"면으로 만든 셔츠다. 최소한의 방어력을 제공한다.", 500);

        Quests = new List<Quest>
        {
            new Quest(
                QuestType.반복,
                "늑대 소굴",
                "엘프의 숲에서 늑대 5마리를 처치해주세요.",
                5, // 필요 몬스터 수
                "늑대", // 필요 몬스터 종류
                100, // 보상: 골드
                50, // 보상: 경험치
                sword, // 보상: 아이템
                1 // 요구레벨
            ),
            new Quest(
                QuestType.반복,
                "숲속의 소문",
                "마을 소문에 의하면 숲속에 고블린이 나온다던데...",
                1, // 필요 몬스터 수
                "고블린", // 예시로 고블린 추가
                50, // 보상: 골드
                225, // 보상: 경험치
                teeshirt, // 보상: 아이템
                1 // 요구레벨
            ),
            new Quest(
                QuestType.반복,
                "골렘 소탕",
                "저주받은 마력으로 인해 골렘들이 증식한다. 처리하자",
                10, // 필요 몬스터 수
                "골렘", // 예시로 골렘 추가
                200, // 보상: 골드
                100, // 보상: 경험치
                null, // 보상: 아이템
                8 // 요구레벨
            ),
            new Quest(
                QuestType.스토리,
                "악의 원천",
                "함락한 성 탐험하기",
                0, // 필요 몬스터 수
                "", // 몬스터 없음
                300, // 보상: 골드
                150, // 보상: 경험치
                null, // 보상: 아이템
                30 // 요구레벨
            )
        };
    }

    // 퀘스트 이름으로 퀘스트 가져오기
    public static Quest GetQuestByName(string QuestName)
    {
        return Quests.Find(q => q.QuestName == QuestName);
    }
}