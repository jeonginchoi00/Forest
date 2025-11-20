using UnityEngine;
using Globals;

public class CoinBox : MonoBehaviour
{
    [SerializeField] private GameObject m_openBoxPrefab;
    [SerializeField] private GameObject m_coinPrefab;
    [SerializeField] private GameObject m_effectPrefab;
    private int m_hitCount = 0;
    private int m_maxCount = 3;

    public void Hit()
    {
        m_hitCount++;

        if (m_hitCount >= m_maxCount)
        {
            Vector3 effectPos = transform.position + new Vector3(0f, 0.3f, 0f);
            PoolManager.GetInstance().Get(m_effectPrefab, effectPos, Quaternion.identity);

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
        }
    }
}