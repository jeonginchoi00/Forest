using UnityEngine;
using UnityEngine.UI;
using Globals;

public class Popup_Die : PopupTemplate
{
    [SerializeField] private Button m_btnOk;

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

    private void OnClickOkBtn()
    {
        SoundManager.GetInstance().PlaySFX(SoundType.SFX_CLICK);
        UserInfoManager.GetInstance().UserRebirth();
        GameManager.GetInstance().Player.SpawnPosition = new Vector2(-13, 7);
        LoadSceneManager.GetInstance().LoadScene(SceneName.MAIN);
    }
}
