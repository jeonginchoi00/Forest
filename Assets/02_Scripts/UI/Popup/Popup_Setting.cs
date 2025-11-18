using UnityEngine;
using UnityEngine.UI;
using Globals;

public class Popup_Setting : PopupTemplate
{
    [SerializeField] private Button m_goMainBtn;
    [SerializeField] private Button m_resetBtn;
    [SerializeField] private Button m_quitBtn;
    [SerializeField] private Slider m_bgmCtrlBar;
    [SerializeField] private Slider m_sfxCtrlBar;

    public override void Initialize()
    {
        base.Initialize();

        m_goMainBtn.onClick.AddListener(OnClickGoMainBtn);
        m_resetBtn.onClick.AddListener(OnClickResetBtn);
        m_quitBtn.onClick.AddListener(OnClickQuitBtn);
    }

    public override void ActivePopup()
    {
        base.ActivePopup();
    }

    public override void InActivePopup()
    {
        base.InActivePopup();
    }

    private void OnClickGoMainBtn()
    {
        LoadSceneManager.GetInstance().LoadScene(SceneName.TITLE);
        InActivePopup();
    }

    private void OnClickResetBtn()
    {
        UserInfoManager.GetInstance().UserInitialize();
        GameUIManager.GetInstance().Initialize();
    }

    private void OnClickQuitBtn()
    {

    }
}
