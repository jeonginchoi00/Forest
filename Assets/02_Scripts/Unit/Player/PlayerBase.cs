using UnityEngine;
using Globals;

public class PlayerBase : MonoBehaviour
{
    private Animator m_animator;
    private Rigidbody2D m_rigidbody;

    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }
    
    // 애니메이션 테스트용 코드
    public virtual void Move()
    {
        float moveX = Input.GetAxisRaw(InputType.HORIZONTAL);
        float moveY = Input.GetAxisRaw(InputType.VERTICAL);

        m_animator.SetFloat(AnimKey.AXISX, moveX);
        m_animator.SetFloat (AnimKey.AXISY, moveY);

        Vector2 moveDir = new Vector2(moveX, moveY).normalized;
        m_rigidbody.linearVelocity = moveDir * 2f;

        if (moveDir.sqrMagnitude > 0.01f)
        {
            m_animator.SetBool(AnimKey.ISMOVE, true);
        }
        else
        {
            m_animator.SetBool(AnimKey.ISMOVE, false);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            m_animator.SetTrigger(AnimKey.ATTACK);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            m_animator.SetTrigger(AnimKey.ATTACK_BOW);
        }
    }
}
