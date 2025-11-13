using Globals;
using UnityEngine;
using UnityEngine.UI;

public class Page_HUD : PageTemplate
{
    [SerializeField] private Button m_interactionBtn;

    private void Start()
    {
        m_interactionBtn.onClick.AddListener(OnClickInteractionBtn);
    }

    private void OnClickInteractionBtn()
    {

    }
}
