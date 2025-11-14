using UnityEngine;
using Globals;

public class CoinBox : MonoBehaviour
{
    [SerializeField] private GameObject m_openBoxPrefab;
    [SerializeField] private GameObject m_coinPrefab;
    private int m_hitCount = 0;
    private int m_maxCount = 3;

    public void Hit()
    {
        m_hitCount++;

        if (m_hitCount >= m_maxCount)
        {
            Instantiate(m_coinPrefab, transform.position, Quaternion.identity);
            Instantiate(m_openBoxPrefab, transform.position, Quaternion.identity);

            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.transform.CompareTag(Tag.ARROW))
        {
            Hit();
            PoolManager.GetInstance().Return(_collision.gameObject);
        }
    }
}