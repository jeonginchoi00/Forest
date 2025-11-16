using UnityEngine;
using Globals;

public class Arrow : MonoBehaviour
{
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
            EnemyBase enemy = _collision.GetComponent<EnemyBase>();

            if (enemy != null && enemy.Hp > 0)
            {
                enemy.SetDamage(GameManager.GetInstance().Player.Damage);
            }
        }

        PoolManager.GetInstance().Return(gameObject);
    }
}
