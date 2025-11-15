using System;
using Globals;
using UnityEngine;

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
}
