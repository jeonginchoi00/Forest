using UnityEngine;
using Globals;

public class Arrow : MonoBehaviour
{
    [SerializeField] private GameObject m_effectPrefab;

    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * 10 * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        PoolManager.GetInstance().Return(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag(Tag.PLAYER))
        {
            return;
        }

        if (_collision.gameObject.layer == LayerMask.NameToLayer(Layer.ENEMY))
        {
            PoolManager.GetInstance().Get(m_effectPrefab, transform.position, Quaternion.identity);

            EnemyBase enemy = _collision.GetComponent<EnemyBase>();

            if (enemy != null && enemy.Hp > 0)
            {
                enemy.SetDamage(UserInfoManager.GetInstance().Damage);
            }
        }

        PoolManager.GetInstance().Return(gameObject);
    }
}
