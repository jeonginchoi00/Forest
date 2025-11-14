using Globals;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Page_HUD : PageTemplate
{
    [Header("상호작용 버튼")]
    [SerializeField] private Button m_interactionBtn;
    [SerializeField] private TMP_Text m_interactionTxt;

    [Header("무기")]
    [SerializeField] private Button m_handBtn;
    [SerializeField] private Button m_bowBtn;

    public override void Initialize()
    {
        base.Initialize();

        m_interactionBtn.onClick.AddListener(OnClickInteractionBtn);
        m_handBtn.onClick.AddListener(OnClickHandBtn);
        m_bowBtn.onClick.AddListener(OnClickBowBtn);

        GameManager.GetInstance().InteractionTypeChange += SetInteractionUI;
        SetInteractionUI(InteractionType.ATTACK);
    }

    public override void ActivePage()
    {
        base.ActivePage();
    }

    public override void InActivePage()
    {
        base.InActivePage();
    }

    private void OnClickInteractionBtn()
    {
        PlayerBase.GetInstance().Interaction();
    }

    private void OnClickHandBtn()
    {
        PlayerBase.GetInstance().IsHand = true;
        PlayerBase.GetInstance().IsBow = false;

        GameManager.GetInstance().SetInteractionType(InteractionType.ATTACK);
    }

    private void OnClickBowBtn()
    {
        PlayerBase.GetInstance().IsHand = false;
        PlayerBase.GetInstance().IsBow = true;

        GameManager.GetInstance().SetInteractionType(InteractionType.ATTACK_BOW);
    }

    private void SetInteractionUI(InteractionType _type)
    {
        switch (_type)
        {
            case InteractionType.ATTACK:
                m_interactionTxt.text = "주먹";
                break;
            case InteractionType.ATTACK_BOW:
                m_interactionTxt.text = "활";
                break;
            case InteractionType.ENTER_NEXT:
                m_interactionTxt.text = "들어가기";
                break;
            case InteractionType.ENTER_PRE:
                m_interactionTxt.text = "나가기";
                break;
        }
    }
}
