using DG.Tweening;
using TMPro;
using UnityEngine;

public class PrintDamage : MonoBehaviour
{
    [SerializeField] private TMP_Text m_printDamage;

    public void SetPrintDamage(int _damage)
    {
        transform.DOKill();
        m_printDamage.DOKill();

        Color color = m_printDamage.color;
        color.a = 1f;
        m_printDamage.color = color;
        m_printDamage.outlineWidth = 0.1f;
        m_printDamage.outlineColor = Color.black;

        m_printDamage.text = _damage.ToString();

        float randomPos = Random.Range(0.5f, 1.3f);

        transform
            .DOMoveY(transform.position.y + randomPos, 0.5f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                m_printDamage
                .DOFade(0f, 0.5f)
                .OnComplete(() =>
                {
                    PoolManager.GetInstance().Return(gameObject);
                });
            });
    }
}
