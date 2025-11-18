using TMPro;
using UnityEngine;
using Globals;

public class Member_Quest : MonoBehaviour
{
    [SerializeField] private TMP_Text m_questTxt;
    [SerializeField] private TMP_Text m_rewardTxt;

    private QuestType m_type;
    private bool m_isCompleted;
    private int m_reward;

    public bool IsCompleted => m_isCompleted;
    public QuestType Type => m_type;
    public int Reward => m_reward;

    public void SetQuest(QuestType _type, string _description, int _reward)
    {
        m_type = _type;
        m_reward = _reward;
        m_questTxt.text = _description;
        m_rewardTxt.text = _reward.ToString("N0");
    }

    public void Complete()
    {
        m_isCompleted = true;
        m_questTxt.color = Color.gray;
        m_rewardTxt.color = Color.gray;
    }
}
