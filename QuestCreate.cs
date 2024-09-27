using System;
using System.Collections.Generic;

public enum QuestType
{
    Fetch,  // 아이템 가져오기
    Kill,   // 몬스터 처치
    Explore // 지역 탐험
}

public class Quest
{
    public QuestType Type { get; set; }  // 퀘스트 타입
    public string Name { get; set; }     // 퀘스트 이름
    public string Description { get; set; }  // 퀘스트 내용

    public Quest(QuestType type, string name, string description)
    {
        Type = type;
        Name = name;
        Description = description;
    }

    public void DisplayQuest()
    {
        Console.WriteLine($"[퀘스트 이름] {Name}");
        Console.WriteLine($"[퀘스트 타입]");
    }
}

public static class QuestDatabase
{
    public static List<Quest> Quests { get; private set; }

    // 스태틱 생성자: 프로그램 시작 시 자동으로 실행되어 퀘스트들을 한꺼번에 초기화
    static QuestDatabase()
    {
        // 미리 정의된 아이템들 (이미 정의된 Item 클래스를 사용)

        // 미리 정의된 퀘스트들
        Quests = new List<Quest>
        {
            new Quest(
                QuestType.Fetch,
                "Retrieve the Sword",
                "Find and retrieve the legendary sword from the dungeon."
            ),
            new Quest(
                QuestType.Kill,
                "Defeat the Goblin King",
                "Defeat the Goblin King and bring peace to the village."
            )
        };
    }

    // 퀘스트 이름으로 퀘스트 가져오기
    public static Quest GetQuestByName(string questName)
    {
        return Quests.Find(q => q.Name == questName);
    }
}