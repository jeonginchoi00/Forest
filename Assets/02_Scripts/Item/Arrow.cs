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

        PoolManager.GetInstance().Return(gameObject);
    }
}
