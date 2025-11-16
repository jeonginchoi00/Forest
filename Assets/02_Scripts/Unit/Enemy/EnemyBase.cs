using UnityEngine;
using UnityEngine.UI;
using Globals;
using System.Collections;
using DG.Tweening;
using TMPro;

public class EnemyBase : MonoBehaviour
{
    [Header("Move")]
    private Animator m_animator;
    private Vector3 m_originPos;
    private Transform m_player;
    private float m_enemyRange = 0.5f; // Enemy 간격
    protected float m_speed;
    protected float m_playerRange; // Player 공격 범위
    protected float m_playerDistance; // Player, Enemy 간격

    [Header("Attack")]
    private float m_attackCool = 5f; // 공격 쿨타임
    private float m_lastAttack;
    protected int m_damage; // Enemy 데미지

    [Header("Damage")]
    [SerializeField] private GameObject m_printDamage;
    private bool m_isAttack = true;
    protected int m_maxHp;
    protected int m_currentHp;

    [Header("HpBar")]
    [SerializeField] private Image m_hpBar;
    [SerializeField] private TMP_Text m_levelTxt;

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

        SetInfoUI();
    }

    #region Move
    public virtual void Move()
    {
        if (!m_isAttack)
        {
            return;
        }

        float distance = Vector2.Distance(transform.position, m_player.position);

        if (distance <= m_playerRange && distance > m_playerDistance) // 플레이어
        {
            // 5초 후 공격
            if (m_lastAttack == 0f)
            {
                m_lastAttack = Time.time;
            }

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
        if (!m_isAttack)
        {
            return;
        }

        m_animator.SetTrigger(AnimKey.JUMP_ATTACK);
        GameManager.GetInstance().Player.SetDamage(m_damage);
    }
    #endregion

    #region Damage
    public virtual void SetDamage(int _damage)
    {
        m_currentHp -= _damage;
        m_animator.SetTrigger(AnimKey.HURT);

        GameObject damagePrefab = PoolManager.GetInstance().Get(m_printDamage, transform.position, Quaternion.identity);

        PrintDamage damage = damagePrefab.GetComponent<PrintDamage>();
        damage.SetPrintDamage(_damage);

        KnockBack();

        if (m_currentHp <= 0)
        {
            Die();
        }
    }

    public virtual void KnockBack()
    {
        transform.DOKill();

        Vector2 dir = (transform.position - m_player.position).normalized;
        Vector2 knockPos = (Vector2)transform.position + dir * 0.5f;

        transform.DOMove(knockPos, 0.2f).SetEase(Ease.OutQuad);
    }

    public virtual void Die()
    {
        m_isAttack = false;
        StartCoroutine(CoRespawn());
    }

    private IEnumerator CoRespawn()
    {
        m_animator.SetTrigger(AnimKey.DEATH);

        yield return new WaitForSeconds(0.7f);

        SetRespawn(false);

        yield return new WaitForSeconds(3f);

        m_currentHp = m_maxHp;
        transform.position = m_originPos;

        SetRespawn(true);
        m_isAttack = true;
    }
    
    private void SetRespawn(bool _active)
    {
        GetComponent<SpriteRenderer>().enabled = _active;
        GetComponent<Collider2D>().enabled = _active;
        transform.GetChild(0).gameObject.SetActive(_active);
    }

    private void SetInfoUI()
    {
        m_levelTxt.outlineWidth = 0.2f;
        m_levelTxt.outlineColor = Color.black;

        m_hpBar.DOKill();

        float hpRatio = (float)m_currentHp / m_maxHp;

        m_hpBar.DOFillAmount(hpRatio, 0.5f).SetEase(Ease.OutQuad);
    }
    #endregion
}