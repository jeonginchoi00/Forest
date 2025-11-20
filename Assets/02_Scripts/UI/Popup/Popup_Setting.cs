using UnityEngine;
using UnityEngine.UI;
using Globals;

public class Popup_Setting : PopupTemplate
{
    [SerializeField] private Button m_mainBtn;
    [SerializeField] private Button m_quitBtn;
    [SerializeField] private Slider m_bgmCtrl;
    [SerializeField] private Slider m_sfxCtrl;

    public override void Initialize()
    {
        base.Initialize();

        m_mainBtn.onClick.AddListener(OnClickMainBtn);
        m_quitBtn.onClick.AddListener(OnClickQuitBtn);

        m_bgmCtrl.value = SoundManager.GetInstance().GetBGMVolume();
        m_sfxCtrl.value = SoundManager.GetInstance().GetSFXVolume();

        m_bgmCtrl.onValueChanged.AddListener((v) => SoundManager.GetInstance().SetBGMVolume(v));
        m_sfxCtrl.onValueChanged.AddListener((v) => SoundManager.GetInstance().SetSFXVolume(v));
    }

    public override void ActivePopup()
    {
        base.ActivePopup();

        GameManager.GetInstance().CurrentTimeScale = 0f;
    }

    public override void InActivePopup()
    {
        base.InActivePopup();

        GameManager.GetInstance().CurrentTimeScale = 1f;
    }

    private void OnClickMainBtn()
    {
        SoundManager.GetInstance().PlaySFX(SoundType.SFX_CLICK);
        LoadSceneManager.GetInstance().LoadScene(SceneName.TITLE);
        InActivePopup();
    }

    private void OnClickQuitBtn()
    {
        SoundManager.GetInstance().PlaySFX(SoundType.SFX_CLICK);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
