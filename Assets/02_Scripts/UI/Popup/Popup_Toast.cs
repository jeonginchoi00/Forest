using DG.Tweening;
using TMPro;
using UnityEngine;

public class Popup_Toast : PopupTemplate
{
    [SerializeField] private TMP_Text m_toastTxt;

    private Vector2 m_originPos;

    public override void Initialize()
    {
        base.Initialize();

        m_originPos = m_toastTxt.transform.position;
    }

    public override void ActivePopup()
    {
        base.ActivePopup();

        TxtAnimation();
    }

    public override void InActivePopup()
    {
        base.InActivePopup();
    }

    public void SetToastTxt(string _message)
    {
        m_toastTxt.text = _message;
    }

    private void TxtAnimation()
    {
        m_toastTxt.transform.position = m_originPos;

        Color color = m_toastTxt.color;
        color.a = 1f;
        m_toastTxt.color = color;
        m_toastTxt.outlineWidth = 0.2f;
        m_toastTxt.outlineColor = Color.black;

        m_toastTxt.transform
            .DOMoveY(m_originPos.y + 5f, 1.8f)
            .SetEase(Ease.OutQuad);

        m_toastTxt.DOFade(0f, 1.8f)
            .OnComplete(() =>
            {
                InActivePopup();
            });
    }
}
