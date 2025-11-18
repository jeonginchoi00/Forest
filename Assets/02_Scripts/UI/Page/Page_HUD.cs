using Globals;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Page_HUD : PageTemplate
{
    [Header("상호작용 버튼")]
    [SerializeField] private Button m_interactionBtn;
    [SerializeField] private TMP_Text m_interactionTxt;

    [Header("무기")]
    [SerializeField] private Button m_handBtn;
    [SerializeField] private Button m_bowBtn;

    [Header("유저 정보")]
    [SerializeField] private TMP_Text m_coin;
    [SerializeField] private TMP_Text m_level;
    [SerializeField] private TMP_Text m_hpTxt;
    [SerializeField] private TMP_Text m_expTxt;
    [SerializeField] private Image m_hp;
    [SerializeField] private Image m_exp;

    [Header("씬 이름")]
    [SerializeField] private TMP_Text m_sceneTxt;

    [Header("환경설정")]
    [SerializeField] private Button m_settingBtn;

    public override void Initialize()
    {
        base.Initialize();

        m_interactionBtn.onClick.AddListener(OnClickInteractionBtn);
        m_handBtn.onClick.AddListener(OnClickHandBtn);
        m_bowBtn.onClick.AddListener(OnClickBowBtn);
        m_settingBtn.onClick.AddListener(OnClickSettingBtn);

        GameManager.GetInstance().InteractionTypeChange += SetInteractionUI;
        SetInteractionUI(InteractionType.ATTACK);

        SetBuyBow();
    }

    public override void ActivePage()
    {
        base.ActivePage();
    }

    public override void InActivePage()
    {
        base.InActivePage();
    }

    private void Update()
    {
        PrintUserInfo();
        PrintSceneInfo();
    }

    private void OnClickInteractionBtn()
    {
        GameManager.GetInstance().Interaction();
    }

    private void OnClickHandBtn()
    {
        GameManager.GetInstance().Player.IsHand = true;
        GameManager.GetInstance().Player.IsBow = false;

        GameManager.GetInstance().SetInteractionType(InteractionType.ATTACK);
    }

    private void OnClickBowBtn()
    {
        GameManager.GetInstance().Player.IsHand = false;
        GameManager.GetInstance().Player.IsBow = true;

        GameManager.GetInstance().SetInteractionType(InteractionType.ATTACK_BOW);
    }

    private void OnClickSettingBtn()
    {
        GameUIManager.GetInstance().ShowPopup(PopupType.SETTING);
    }

    private void SetInteractionUI(InteractionType _type)
    {
        if (m_handBtn != null)
        {
            m_handBtn.transform.DOKill();
        }
        
        if (m_bowBtn != null)
        {
            m_bowBtn.transform.DOKill();
        }

        Vector2 selected = Vector2.one * 1.2f;
        Vector2 notSelected = Vector2.one * 0.8f;

        switch (_type)
        {
            case InteractionType.ATTACK:
                m_interactionTxt.text = "공격";

                if (m_handBtn != null)
                {
                    m_handBtn.transform.DOScale(selected, 0.2f);
                }
                if (m_bowBtn != null)
                {
                    m_bowBtn.transform.DOScale(notSelected, 0.2f);
                }

                break;
            case InteractionType.ATTACK_BOW:
                m_interactionTxt.text = "공격";
                if (m_handBtn != null)
                {
                    m_handBtn.transform.DOScale(notSelected, 0.2f);
                }
                if (m_bowBtn != null)
                {
                    m_bowBtn.transform.DOScale(selected, 0.2f);
                }
                break;
            case InteractionType.ENTER_NEXT:
                m_interactionTxt.text = "들어가기";
                break;
            case InteractionType.ENTER_PRE:
                m_interactionTxt.text = "나가기";
                break;
            case InteractionType.NPC_HP:
                m_interactionTxt.text = "말걸기";
                break;
            case InteractionType.NPC_WEAPON:
                m_interactionTxt.text = "말걸기";
                break;
        }
    }

    private void PrintUserInfo()
    {
        m_hp.DOKill();
        m_exp.DOKill();

        int userCoin = UserInfoManager.GetInstance().Coin;
        int userLevel = UserInfoManager.GetInstance().Level;
        int userCurrentHp = UserInfoManager.GetInstance().CurrentHp;
        int userMaxHp = UserInfoManager.GetInstance().MaxHp;
        int userCurrentExp = UserInfoManager.GetInstance().CurrentExp;
        int userMaxExp = UserInfoManager.GetInstance().MaxExp;

        m_coin.text = userCoin.ToString("N0");
        m_level.text = userLevel.ToString();
        m_hpTxt.text = $"{userCurrentHp}/{userMaxHp}";
        m_expTxt.text = $"{userCurrentExp}/{userMaxExp}";

        float hpRatio = (float)userCurrentHp / userMaxHp;
        float expRatio = (float)userCurrentExp / userMaxExp;

        m_hp.DOFillAmount(hpRatio, 0.5f).SetEase(Ease.OutQuad);
        m_exp.DOFillAmount(expRatio, 0.5f).SetEase(Ease.OutQuad);
    }

    private void PrintSceneInfo()
    {
        m_sceneTxt.outlineWidth = 0.2f;
        m_sceneTxt.outlineColor = Color.black;

        string currentScene = SceneManager.GetActiveScene().name;

        switch (currentScene)
        {
            case SceneName.MAIN:
                m_sceneTxt.text = SceneInfo.MAIN_NAME;
                break;
            case SceneName.GAME:
                m_sceneTxt.text = SceneInfo.GAME_NAME;
                break;
            case SceneName.BOSSGAME:
                m_sceneTxt.text = SceneInfo.BOSSGAME_NAME;
                break;
        }
    }

    public void SetBuyBow()
    {
        m_bowBtn.gameObject.SetActive(UserInfoManager.GetInstance().HasBow);
    }
}
