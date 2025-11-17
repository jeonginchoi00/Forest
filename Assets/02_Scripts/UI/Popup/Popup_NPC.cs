using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Popup_NPC : PopupTemplate
{
    [SerializeField] private Button m_btnOk;
    [SerializeField] private TMP_Text m_questionTxt;

    public override void Initialize()
    {
        base.Initialize();
        m_btnOk.onClick.AddListener(OnClickOkBtn);
    }

    public override void ActivePopup()
    {
        base.ActivePopup();
    }

    public override void InActivePopup()
    {
        base.InActivePopup();
    }

    public void SetQuestionTxt(string _message)
    {
        m_questionTxt.text = _message;
    }

    private void OnClickOkBtn()
    {
        GameManager.GetInstance().TryBuy();
    }
}
