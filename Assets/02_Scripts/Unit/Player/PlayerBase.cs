using UnityEngine;
using Globals;
using System.Collections;

public class PlayerBase : MonoBehaviour
{
    private static PlayerBase m_instance;
    public static PlayerBase GetInstance() => m_instance;

    private Animator m_animator;
    private Rigidbody2D m_rigidbody;
    private Vector2 m_moveDir;
    private Vector2 m_lastMoveDir;
    private float m_speed = 4f;

    public Vector2 MoveDir { get => m_moveDir; set => m_moveDir = value; }

    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
        }
    }

    private void OnDestroy()
    {
        if (m_instance == this)
        {
            m_instance = null;
        }
    }

    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    public virtual void FixedUpdate()
    {
        Move();
    }

    public virtual void OnCollisionStay2D(Collision2D _collision)
    {
        if (_collision.transform.CompareTag(Tag.DOOR))
        {
            GameManager.GetInstance().SetInteractionType(InteractionType.ENTER);
        }
    }

    public virtual void OnCollisionExit2D(Collision2D _collision)
    {
        if (_collision.transform.CompareTag(Tag.DOOR))
        {
            GameManager.GetInstance().SetInteractionType(InteractionType.ATTACK);
        }
    }

    public virtual void Move()
    {
        if (m_moveDir != Vector2.zero)
        {
            m_lastMoveDir = m_moveDir;
        }

        Vector2 moveDir = transform.position + (Vector3)m_moveDir * m_speed * Time.deltaTime;

        Vector2 animDir = (m_moveDir != Vector2.zero) ? m_moveDir : m_lastMoveDir;

        m_animator.SetFloat(AnimKey.AXISX, animDir.x);
        m_animator.SetFloat(AnimKey.AXISY, animDir.y);
        m_animator.SetBool(AnimKey.ISMOVE, m_moveDir.magnitude > 0);

        if (m_moveDir.x != 0)
        {
            Vector2 localScale = transform.localScale;
            localScale.x = (m_moveDir.x > 0) ? 1 : -1;
            transform.localScale = localScale;
        }

        m_rigidbody.MovePosition(moveDir);
    }

    public virtual void Attack()
    {
        m_animator.SetTrigger(AnimKey.ATTACK);
    }

    public virtual void Attack_Bow()
    {
        m_animator.SetTrigger(AnimKey.ATTACK_BOW);
    }

    public virtual void Interaction()
    {
        InteractionType type = GameManager.GetInstance().CurrentInteractionType;

        switch (type)
        {
            case InteractionType.ATTACK:
                Attack();
                break;
            case InteractionType.ATTACK_BOW:
                Attack_Bow();
                break;
            case InteractionType.ENTER:
                break;
        }
    }
}
