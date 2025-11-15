using UnityEngine;
using Globals;

public class EnemyBase : MonoBehaviour
{
    private Animator m_animator;
    private Vector3 m_originPos;
    private float m_enemyRange = 0.5f; // Enemy 간격

    private Transform m_player;

    protected float m_speed;
    protected float m_playerRange; // Player 공격 범위
    protected float m_playerDistance; // Player, Enemy 간격

    public virtual void Start()
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

    public virtual void Move()
    {
        float distance = Vector2.Distance(transform.position, m_player.position);

        if (distance <= m_playerRange && distance > m_playerDistance) // 플레이어
        {
            Vector2 dir = (m_player.position - transform.position).normalized;
            transform.position += (Vector3)(dir * m_speed * Time.deltaTime);
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
}