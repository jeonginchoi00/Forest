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
    public PlayerBase Player => m_player;

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
            case InteractionType.NPC:
                Debug.Log("NPC¿Í ¸¸³µ´Ù!");
                break;
        }
    }
}
