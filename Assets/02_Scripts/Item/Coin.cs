using UnityEngine;
using DG.Tweening;
using Globals;

public class Coin : MonoBehaviour
{
    private int m_value = 500;

    private void Start()
    {
        Vector3 startPos = transform.position;

        transform
            .DOMoveY(startPos.y + 2f, 0.5f / 2)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                transform
                .DOMoveY(startPos.y, 0.5f / 2)
                .SetEase(Ease.InQuad);
            });
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag(Tag.PLAYER))
        {
            SoundManager.GetInstance().PlaySFX(SoundType.SFX_COIN);

            transform
                .DOScale(Vector3.zero, 0.3f)
                .SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    Destroy(gameObject);
                });

            UserInfoManager.GetInstance().SetCoin(m_value);
        }
    }
}
