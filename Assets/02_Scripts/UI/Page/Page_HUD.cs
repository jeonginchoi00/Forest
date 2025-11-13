using Globals;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Page_HUD : PageTemplate
{
    [SerializeField] private Button m_interactionBtn;
    [SerializeField] private TMP_Text m_interactionTxt;

    public override void Initialize()
    {
        base.Initialize();
        m_interactionBtn.onClick.AddListener(OnClickInteractionBtn);
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

    private void SetInteractionUI(InteractionType _type)
    {
        switch (_type)
        {
            case InteractionType.ATTACK:
                m_interactionTxt.text = "¡÷∏‘";
                break;
            case InteractionType.ATTACK_BOW:
                m_interactionTxt.text = "»∞";
                break;
            case InteractionType.ENTER:
                m_interactionTxt.text = "¿‘¿Â";
                break;
        }
    }
}
