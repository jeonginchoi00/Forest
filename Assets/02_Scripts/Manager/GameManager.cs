using System;
using Globals;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance;
    public static GameManager GetInstance() => m_instance;

    private InteractionType m_currentInteractionType = InteractionType.ATTACK;

    private PlayerBase m_player;
    private NPCBase m_npc;

    private int m_hpPrice = 2000;
    private int m_bowPrice = 5000;

    public event Action<InteractionType> InteractionTypeChange;
    public InteractionType CurrentInteractionType => m_currentInteractionType;
    public PlayerBase Player => m_player;
    public NPCBase NPC => m_npc;
    public int HpPrice => m_hpPrice;
    public int BowPrice => m_bowPrice;

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
