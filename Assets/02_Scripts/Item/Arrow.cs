using UnityEngine;

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
}
