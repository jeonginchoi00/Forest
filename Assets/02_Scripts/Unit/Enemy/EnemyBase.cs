using UnityEngine;
using Globals;

public class EnemyBase : MonoBehaviour
{
    [Header("Move")]
    private Animator m_animator;
    private Vector3 m_originPos;
    private Transform m_player;
    private float m_enemyRange = 0.5f; // Enemy 간격

    [Header("Attack")]
    private float m_attackCool = 5f; // 공격 쿨타임
    private float m_lastAttack;

    protected float m_speed;
    protected float m_playerRange; // Player 공격 범위
    protected float m_playerDistance; // Player, Enemy 간격
    protected int m_damage; // Enemy 데미지

    private void Start()
    {
        Initialize();
    }

    public virtual void Initialize()
    {
        m_animator = GetComponent<Animator>();
        m_originPos = transform.position;

        if (m_player == null)
        {
            m_player = GameObject.FindGameObjectWithTag(Tag.PLAYER).transform;
        }
    }

    public virtual void Update()
    {
        Move();
        Separation();
    }

    #region Move
    public virtual void Move()
    {
        float distance = Vector2.Distance(transform.position, m_player.position);

        if (distance <= m_playerRange && distance > m_playerDistance) // 플레이어
        {
            Vector2 dir = (m_player.position - transform.position).normalized;
            transform.position += (Vector3)(dir * m_speed * Time.deltaTime);

            // 공격
            if (Time.time - m_lastAttack >= m_attackCool)
            {
                Attack();
                m_lastAttack = Time.time;
            }
        }
        else // 원래 자리
        {
            float returnDistance = Vector2.Distance(transform.position, m_originPos);

            Vector2 dir = (m_originPos - transform.position).normalized;
            transform.position += (Vector3)(dir * m_speed * Time.deltaTime);
        }
    }

    public virtual void Separation()
    {
        int enemyLayer = LayerMask.NameToLayer(Layer.ENEMY);

        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, m_enemyRange);

        foreach (Collider2D enemy in enemies)
        {
            if (enemy.gameObject == gameObject)
            {
                continue;
            }
            
            if (enemy.gameObject.layer != enemyLayer)
            {
                continue;
            }

            Vector2 pushDir = (transform.position - enemy.transform.position).normalized;
            transform.position += (Vector3)(pushDir * 2f * Time.deltaTime);
        }
    }
    #endregion

    #region Attack
    public virtual void Attack()
    {
        m_animator.SetTrigger(AnimKey.JUMP_ATTACK);
        PlayerBase.GetInstance().SetDamage(m_damage);
    }
    #endregion
}