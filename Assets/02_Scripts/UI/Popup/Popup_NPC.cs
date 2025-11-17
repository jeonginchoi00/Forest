using Globals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Popup_NPC : PopupTemplate
{
    [SerializeField] private Button m_btnOk;
    [SerializeField] private Button m_btnOk_Has;
    [SerializeField] private TMP_Text m_questionTxt;
    [SerializeField] private TMP_Text m_priceTxt;

    public override void Initialize()
    {
        base.Initialize();
        m_btnOk.onClick.AddListener(OnClickOkBtn);
        m_btnOk_Has.onClick.AddListener(OnClickOkHasBtn);
    }

    public override void ActivePopup()
    {
        base.ActivePopup();

        if (GameManager.GetInstance().CurrentInteractionType == InteractionType.NPC_WEAPON
            && UserInfoManager.GetInstance().HasBow)
        {
            SetUIHasBow(false);
        }
        else
        {
            SetUIHasBow(true);
        }
    }

    public override void InActivePopup()
    {
        base.InActivePopup();
    }

    public void SetQuestionTxt(string _message, int _price)
    {
        m_questionTxt.text = _message;
        m_priceTxt.text = _price.ToString("N0");
    }

    private void OnClickOkBtn()
    {
        GameManager.GetInstance().TryBuy();
    }

    private void OnClickOkHasBtn()
    {
        InActivePopup();
    }

    public void SetUIHasBow(bool _active)
    {
        m_closeBtn.gameObject.SetActive(_active);
        m_btnOk.gameObject.SetActive(_active);
        m_btnOk_Has.gameObject.SetActive(!_active);
        m_priceTxt.transform.parent.gameObject.SetActive(_active);
    }
}
