using UnityEngine;
using Globals;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] private GameObject m_arrowPrefab;

    private Animator m_animator;
    private Rigidbody2D m_rigidbody;
    private Vector2 m_position;
    private Vector2 m_moveDir;
    private Vector2 m_lastMoveDir;
    private float m_speed = 3f;

    private bool m_isHand = true;
    private bool m_isBow = false;

    private int m_damage = 10;

    #region Property
    public Vector2 SpawnPosition
    {
        get => m_position;
        set
        {
            m_position = value;
            transform.position = m_position;
            m_lastMoveDir = Vector2.down;
        }
    }
    public Vector2 MoveDir { get => m_moveDir; set => m_moveDir = value; }
    public bool IsHand { get => m_isHand; set => m_isHand = value; }
    public bool IsBow { get => m_isBow; set => m_isBow = value; }
    public int Damage { get => m_damage; set => m_damage = value; }
    #endregion

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (GameManager.GetInstance() != null)
        {
            if (GameManager.GetInstance().Player != null
                && GameManager.GetInstance().Player != this)
            {
                Destroy(gameObject);
                return;
            }
        }
    }

    private void Start()
    {
        GameManager.GetInstance().RegisterPlayer(this);

        m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        SpawnPosition = new Vector2(-13, 7);
    }

    public virtual void FixedUpdate()
    {
        Move();
    }

    public virtual void OnCollisionStay2D(Collision2D _collision)
    {
        if (_collision.transform.CompareTag(Tag.DOOR_NEXT))
        {
            GameManager.GetInstance().SetInteractionType(InteractionType.ENTER_NEXT);
        }

        if (_collision.transform.CompareTag(Tag.DOOR_PRE))
        {
            GameManager.GetInstance().SetInteractionType(InteractionType.ENTER_PRE);
        }
    }

    public virtual void OnCollisionExit2D(Collision2D _collision)
    {
        if (_collision.transform.CompareTag(Tag.DOOR_NEXT)
            || _collision.transform.CompareTag(Tag.DOOR_PRE))
        {
            if (m_isHand && !m_isBow)
            {
                GameManager.GetInstance().SetInteractionType(InteractionType.ATTACK);
            }
            else if (m_isBow && !m_isHand)
            {
                GameManager.GetInstance().SetInteractionType(InteractionType.ATTACK_BOW);
            }
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

    #region Attack
    public virtual void Attack()
    {
        m_animator.SetTrigger(AnimKey.ATTACK);

        // 코인 박스 처리
        float coinBoxRange = 1f;
        Vector2 playerPos = transform.position;

        Collider2D[] boxes = Physics2D.OverlapCircleAll(playerPos, coinBoxRange);

        foreach (Collider2D col in boxes)
        {
            CoinBox box = col.GetComponent<CoinBox>();

            if (box != null)
            {
                box.Hit();
            }
        }

        // 에너미 처리
        float enemyRange = 3f;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(playerPos, enemyRange);

        foreach (Collider2D col in enemies)
        {
            EnemyBase enemy = col.GetComponent<EnemyBase>();

            if (enemy != null)
            {
                enemy.SetDamage(m_damage);
            }
        }

    }

    public virtual void Attack_Bow()
    {
        m_animator.SetTrigger(AnimKey.ATTACK_BOW);

        Vector2 shootDir = (m_moveDir != Vector2.zero) ? m_moveDir : m_lastMoveDir;

        if (Mathf.Abs(shootDir.x) > Mathf.Abs(shootDir.y))
        {
            shootDir = new Vector2(Mathf.Sign(shootDir.x), 0); // 좌우
        }
        else
        {
            shootDir = new Vector2(0, Mathf.Sign(shootDir.y)); // 상하
        }

        float angle = 0f;

        if (shootDir == Vector2.up)
        {
            angle = 0f;
        }
        else if (shootDir == Vector2.right)
        {
            angle = -90f;
        }
        else if (shootDir == Vector2.down)
        {
            angle = 180f;
        }
        else if (shootDir == Vector2.left)
        {
            angle = 90f;
        }

        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        PoolManager.GetInstance().Get(m_arrowPrefab, transform.position, rotation);
    }

    public virtual void Interaction()
    {
        InteractionType type = GameManager.GetInstance().CurrentInteractionType;

        string currentScene = SceneManager.GetActiveScene().name;

        switch (type)
        {
            case InteractionType.ATTACK:
                Attack();
                break;
            case InteractionType.ATTACK_BOW:
                Attack_Bow();
                break;
            case InteractionType.ENTER_NEXT:
                LoadSceneManager.GetInstance().LoadNextScene(currentScene);
                break;
            case InteractionType.ENTER_PRE:
                LoadSceneManager.GetInstance().LoadPreScene(currentScene);
                break;
        }
    }
    #endregion

    #region Damage
    public virtual void SetDamage(int _damage)
    {
        UserInfoManager.GetInstance().CurrentHp -= _damage;

        if (UserInfoManager.GetInstance().CurrentHp <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log("플레이어 죽음");
    }
    #endregion
}
