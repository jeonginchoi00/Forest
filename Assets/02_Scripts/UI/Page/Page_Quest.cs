using UnityEngine;
using UnityEngine.UI;

public class Page_Quest : PageTemplate
{
    [SerializeField] private Button m_btnQuest;
    [SerializeField] private GameObject m_quest;

    public override void Initialize()
    {
        base.Initialize();

        m_btnQuest.onClick.AddListener(OnClickQuestBtn);
    }

    public override void ActivePage()
    {
        base.ActivePage();
    }

    public override void InActivePage()
    {
        base.InActivePage();
    }

    private void OnClickQuestBtn()
    {
        m_quest.SetActive(!m_quest.activeSelf);
    }
}
