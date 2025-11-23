using UnityEngine;
using Globals;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] private GameObject m_arrowPrefab;
    [SerializeField] private GameObject m_attackEffectPrefab;

    private Animator m_animator;
    private Rigidbody2D m_rigidbody;
    private SpriteRenderer m_spriteRenderer;
    private Color m_hitColor = Color.red;
    private Vector2 m_position;
    private Vector2 m_moveDir;
    private Vector2 m_lastMoveDir;
    private float m_speed = 3f;

    private bool m_isHand = true;
    private bool m_isBow = false;

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
        GameManager.GetInstance().SetPlayerState(PlayerState.LIVE);
        GameManager.GetInstance().RegisterPlayer(this);

        m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        SpawnPosition = new Vector2(-13, 7);

        Initialize();
    }

    public virtual void Initialize()
    {

    }

    public virtual void FixedUpdate()
    {
        if (GameManager.GetInstance().PlayerState == PlayerState.DIE)
        {
            return;
        }

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
        bool isMoving = m_moveDir.magnitude > 0;

        if (isMoving)
        {
            SoundManager.GetInstance().PlaySFXWalk(SoundType.SFX_WALK);
        }
        else
        {
            SoundManager.GetInstance().StopSFXWalk();
        }

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
        SoundManager.GetInstance().PlaySFX(SoundType.SFX_ATTACK);
        m_animator.SetTrigger(AnimKey.ATTACK);
        PoolManager.GetInstance().Get(m_attackEffectPrefab, transform.position, Quaternion.identity);

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

            if (enemy != null && enemy.Hp > 0)
            {
                enemy.SetDamage(UserInfoManager.GetInstance().Damage);
            }
        }

    }

    public virtual void Attack_Bow()
    {
        SoundManager.GetInstance().PlaySFX(SoundType.SFX_ATTACK_BOW);
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
    #endregion

    #region Damage
    public virtual void SetDamage(int _damage)
    {
        UserInfoManager.GetInstance().SetHp(_damage);
        StartCoroutine(CoHitEffect());

        if (UserInfoManager.GetInstance().CurrentHp <= 0)
        {
            Die();
        }
    }

    private IEnumerator CoHitEffect()
    {
        m_spriteRenderer.color = m_hitColor;
        yield return new WaitForSeconds(0.2f);
        m_spriteRenderer.color = Color.white;
    }

    public virtual void Die()
    {
        SoundManager.GetInstance().StopSFXWalk();

        GameManager.GetInstance().SetPlayerState(PlayerState.DIE);
        GameUIManager.GetInstance().ShowPopup(PopupType.DIE);
    }
    #endregion
}
