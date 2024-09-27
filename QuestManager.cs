public class QuestManager
{
    private List<Quest> quests;

    public QuestManager()
    {
        quests = new List<Quest>();
    }

    // 퀘스트 추가
    public void AddQuest(Quest quest)
    {
        quests.Add(quest);
    }

    // 이름으로 퀘스트 검색
    public Quest GetQuestByName(string questName)
    {
        return quests.Find(q => q.Name == questName);
    }

    // 모든 퀘스트 출력
    public void DisplayAllQuests()
    {
        foreach (var quest in quests)
        {
            quest.DisplayQuest();
            Console.WriteLine();
        }
    }
}
