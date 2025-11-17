using System;
using Globals;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance;
    public static GameManager GetInstance() => m_instance;

    private InteractionType m_currentInteractionType = InteractionType.ATTACK;
    public InteractionType CurrentInteractionType => m_currentInteractionType;

    public event Action<InteractionType> InteractionTypeChange;

    private PlayerBase m_player;
    private NPCBase m_npc;
    public PlayerBase Player => m_player;
    public NPCBase NPC => m_npc;

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
                GameUIManager.GetInstance().SetNPCMessage(PopupString.NPC_HP);
                GameUIManager.GetInstance().ShowPopup(PopupType.NPC);
                break;
            case InteractionType.NPC_WEAPON:
                GameUIManager.GetInstance().SetNPCMessage(PopupString.NPC_WEAPON);
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
        int price = 5000;

        if (UserInfoManager.GetInstance().Coin >= price)
        {
            UserInfoManager.GetInstance().SetCoin(-price);
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
        int price = 5000;
        int level = 5;

        if (UserInfoManager.GetInstance().Coin >= price
            && UserInfoManager.GetInstance().Level >= level)
        {
            UserInfoManager.GetInstance().SetCoin(-price);
            // [TODO] 활 구매 처리 함수 추가
            GameUIManager.GetInstance().SetToast(ToastString.NPC_WEAPON_O);
            GameUIManager.GetInstance().ShowPopup(PopupType.TOAST);
        }
        else if (UserInfoManager.GetInstance().Level < level)
        {
            GameUIManager.GetInstance().SetToast(ToastString.NPC_WEAPON_X_LEVEL);
            GameUIManager.GetInstance().ShowPopup(PopupType.TOAST);
        }
        else if (UserInfoManager.GetInstance().Level >= level
                 && UserInfoManager.GetInstance().Coin < price)
        {
            GameUIManager.GetInstance().SetToast(ToastString.NPC_WEAPON_X_COIN);
            GameUIManager.GetInstance().ShowPopup(PopupType.TOAST);
        }
    }
}
