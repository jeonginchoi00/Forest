using System.Collections.Generic;
using Globals;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    [SerializeField] private Transform m_content;
    [SerializeField] private GameObject m_questPrefab;

    private List<Member_Quest> m_questList = new List<Member_Quest>();

    public Member_Quest AddQuest(QuestType _type, string _description, int _reward)
    {
        GameObject questPrefab = Instantiate(m_questPrefab, m_content);
        Member_Quest quest = questPrefab.GetComponent<Member_Quest>();

        quest.SetQuest(_type, _description, _reward);
        m_questList.Add(quest);

        return quest;
    }

    public void RemoveQuest(Member_Quest _quest)
    {
        if (m_questList.Contains(_quest))
        {
            m_questList.Remove(_quest);
            Destroy(_quest.gameObject);
        }
    }

    public void ClearAll()
    {
        for (int i = 0; i < m_questList.Count; i++)
        {
            Destroy(m_questList[i].gameObject);
        }

        m_questList.Clear();
    }

    public void LoadQuestList()
    {
        foreach (Member_Quest quest in m_questList)
        {
            if (UserInfoManager.GetInstance().IsQuestCompleted(quest.Type))
            {
                quest.Complete();
            }
        }

        if (!m_questList.Exists(q => q.Type == QuestType.BOW))
        {
            AddQuest(QuestType.BOW, Quest.QUEST_BOW, 5000);
        }

        if (!m_questList.Exists(q => q.Type == QuestType.HP))
        {
            AddQuest(QuestType.HP, Quest.QUEST_HP, 2500);
        }
    }

    public void CheckQuest(QuestType _type)
    {
        for (int i = 0; i < m_questList.Count; i++)
        {
            Member_Quest quest = m_questList[i];

            if (!quest.IsCompleted && quest.Type == _type)
            {
                quest.Complete();
                RewardQuest(quest);

                // 완료된 퀘스트는 밑으로 가도록
                m_questList.RemoveAt(i);
                m_questList.Add(quest);
                quest.transform.SetSiblingIndex(m_content.childCount - 1);
                i--;
            }
        }
    }

    private void RewardQuest(Member_Quest _quest)
    {
        UserInfoManager.GetInstance().SetCoin(_quest.Reward);
        UserInfoManager.GetInstance().SetQuestCompleted(_quest.Type, true);
    }
}
