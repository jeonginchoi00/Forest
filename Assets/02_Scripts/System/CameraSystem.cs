using DG.Tweening;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [SerializeField] private Transform m_player;

    private Vector3 m_offset = new Vector3(0, 0, -10);
    private Vector2 m_min = new Vector2(-10, -10);
    private Vector2 m_max = new Vector2(10, 10);

    private void Awake()
    {
        m_player = m_player.transform;
    }

    private void FixedUpdate()
    {
        Vector3 targetPos = m_player.position + m_offset;

        targetPos.x = Mathf.Clamp(targetPos.x, m_min.x, m_max.x);
        targetPos.y = Mathf.Clamp(targetPos.y, m_min.y, m_max.y);

        Camera.main.transform
            .DOMove(targetPos, 0.2f)
            .SetEase(Ease.OutQuad);
    }
}
