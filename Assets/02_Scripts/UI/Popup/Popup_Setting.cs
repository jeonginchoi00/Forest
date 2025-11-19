using UnityEngine;
using UnityEngine.UI;
using Globals;

public class Popup_Setting : PopupTemplate
{
    [SerializeField] private Button m_mainBtn;
    [SerializeField] private Button m_quitBtn;

    public override void Initialize()
    {
        base.Initialize();

        m_mainBtn.onClick.AddListener(OnClickMainBtn);
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

    private void OnClickMainBtn()
    {
        LoadSceneManager.GetInstance().LoadScene(SceneName.TITLE);
        InActivePopup();
    }

    private void OnClickQuitBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
