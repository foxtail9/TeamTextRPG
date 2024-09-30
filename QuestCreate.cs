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

    public Quest(QuestType type, string name, string description)
    {
        questype = type;
        questname = name;
        questDescription = description;
    }

    public void DisplayQuest()
    {
        Console.WriteLine($"[{questype}] | {questname} | {questDescription} | 보상 : ");
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
                QuestType.일반,
                "늑대 소굴",
                "엘프의 숲에서 늑대 5마리를 처치해주세요."
            ),
            new Quest(
                QuestType.일반,
                "숲속의 소문",
                "마을 소문에 의하면 숲속에 고블린이 나온다던데..."
            ),
            new Quest(
                QuestType.반복,
                "골렘 소탕",
                "저주받은 마력으로 인해 골렘들이 증식한다. 처리하자"
            ),
            new Quest(
                QuestType.스토리,
                "악의 원천",
                "함락한 성 탐험하기"
            )
        };
    }

    // 퀘스트 이름으로 퀘스트 가져오기
    public static Quest GetQuestByName(string questName)
    {
        return Quests.Find(q => q.questname == questName);
    }
}