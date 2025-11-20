using System;
using Globals;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance;
    public static GameManager GetInstance() => m_instance;

    [SerializeField] private QuestSystem m_questSystem;

    private InteractionType m_currentInteractionType = InteractionType.ATTACK;
    private PlayerState m_playerState;
    private PlayerBase m_player;
    private NPCBase m_npc;
    private JoyStick m_joyStick;
    private int m_hpPrice = 2000;
    private int m_bowPrice = 5000;
    private float m_currentTimeScale = 1f;

    public event Action<InteractionType> InteractionTypeChange;

    public InteractionType CurrentInteractionType => m_currentInteractionType;
    public PlayerState PlayerStage => m_playerState;
    public PlayerBase Player => m_player;
    public NPCBase NPC => m_npc;
    public JoyStick JoyStick => m_joyStick;
    public QuestSystem QuestSystem => m_questSystem;
    public int HpPrice => m_hpPrice;
    public int BowPrice => m_bowPrice;
    public float CurrentTimeScale
    {
        get => m_currentTimeScale;
        set
        {
            m_currentTimeScale = value;
            Time.timeScale = m_currentTimeScale;
        }
    }

    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterPlayer(PlayerBase _player)
    {
        m_player = _player;
    }

    public void RegisterJoyStick(JoyStick _joyStick)
    {
        m_joyStick = _joyStick;
    }

    public void SetPlayerState(PlayerState _state)
    {
        m_playerState = _state;

        if (GameManager.GetInstance().JoyStick != null)
        {
            switch (_state)
            {
                case PlayerState.DIE:
                    GameManager.GetInstance().JoyStick.SetAvailable(false);
                    break;
                case PlayerState.LIVE:
                    GameManager.GetInstance().JoyStick.SetAvailable(true);
                    break;
            }
        }
    }

    public void SetInteractionType(InteractionType _type)
    {
        if (m_currentInteractionType == _type)
        {
            return;
        }

        m_currentInteractionType = _type;
        InteractionTypeChange?.Invoke(_type);
    }

    public void Interaction()
    {
        InteractionType type = GameManager.GetInstance().CurrentInteractionType;

        string currentScene = SceneManager.GetActiveScene().name;

        switch (type)
        {
            case InteractionType.ATTACK:
                m_player.Attack();
                break;
            case InteractionType.ATTACK_BOW:
                m_player.Attack_Bow();
                break;
            case InteractionType.ENTER_NEXT:
                LoadSceneManager.GetInstance().LoadNextScene(currentScene);
                break;
            case InteractionType.ENTER_PRE:
                LoadSceneManager.GetInstance().LoadPreScene(currentScene);
                break;
            case InteractionType.NPC_HP:
                GameUIManager.GetInstance().SetNPCMessage(PopupString.NPC_HP, m_hpPrice);
                GameUIManager.GetInstance().ShowPopup(PopupType.NPC);
                break;
            case InteractionType.NPC_WEAPON:

                if (UserInfoManager.GetInstance().HasBow)
                {
                    GameUIManager.GetInstance().SetNPCMessage(PopupString.NPC_HASWEAPON, m_bowPrice);
                }
                else
                {
                    GameUIManager.GetInstance().SetNPCMessage(PopupString.NPC_WEAPON, m_bowPrice);
                }

                GameUIManager.GetInstance().ShowPopup(PopupType.NPC);
                break;
        }
    }

    public void TryBuy()
    {
        switch (m_currentInteractionType)
        {
            case InteractionType.NPC_HP:
                TryBuyHp();
                break;
            case InteractionType.NPC_WEAPON:
                TryBuyWeapon();
                break;
        }
    }

    public void TryBuyHp()
    {
        if (UserInfoManager.GetInstance().Coin >= m_hpPrice)
        {
            UserInfoManager.GetInstance().SetCoin(-m_hpPrice);
            UserInfoManager.GetInstance().SetHpFull();
            GameUIManager.GetInstance().SetToast(ToastString.NPC_HP_O);
            GameUIManager.GetInstance().ShowPopup(PopupType.TOAST);
            m_questSystem.CheckQuest(QuestType.HP);
        }
        else
        {
            GameUIManager.GetInstance().SetToast(ToastString.NPC_HP_X);
            GameUIManager.GetInstance().ShowPopup(PopupType.TOAST);
        }
    }

    public void TryBuyWeapon()
    {
        int level = 5;

        if (UserInfoManager.GetInstance().Coin >= m_bowPrice
            && UserInfoManager.GetInstance().Level >= level)
        {
            UserInfoManager.GetInstance().SetCoin(-m_bowPrice);
            UserInfoManager.GetInstance().SetBow(true);
            GameUIManager.GetInstance().SetToast(ToastString.NPC_WEAPON_O);
            GameUIManager.GetInstance().ShowPopup(PopupType.TOAST);
            m_questSystem.CheckQuest(QuestType.BOW);
        }
        else if (UserInfoManager.GetInstance().Level < level)
        {
            GameUIManager.GetInstance().SetToast(ToastString.NPC_WEAPON_X_LEVEL);
            GameUIManager.GetInstance().ShowPopup(PopupType.TOAST);
        }
        else if (UserInfoManager.GetInstance().Level >= level
                 && UserInfoManager.GetInstance().Coin < m_bowPrice)
        {
            GameUIManager.GetInstance().SetToast(ToastString.NPC_WEAPON_X_COIN);
            GameUIManager.GetInstance().ShowPopup(PopupType.TOAST);
        }
    }
}
