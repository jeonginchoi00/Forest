using UnityEngine;
using Globals;

public class EnemyBase : MonoBehaviour
{
    private Animator m_animator;

    private void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    public virtual void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            m_animator.SetTrigger(AnimKey.JUMP_ATTACK);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            m_animator.SetTrigger(AnimKey.HURT);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            m_animator.SetTrigger(AnimKey.DEATH);
        }
    }
}
