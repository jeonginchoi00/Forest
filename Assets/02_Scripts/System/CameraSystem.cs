using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    private static CameraSystem m_instance;
    public static CameraSystem GetInstance() => m_instance;

    [SerializeField] private Transform m_player;

    private Vector3 m_offset = new Vector3(0, 0, -10);
    private Vector2 m_min = new Vector2(-10, -10);
    private Vector2 m_max = new Vector2(10, 10);

    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        m_player = m_player.transform;
    }

    private void OnDestroy()
    {
        DOTweenAllKill();
    }

    private void DOTweenAllKill()
    {
        if (Camera.main != null)
        {
            Camera.main.transform.DOKill();
        }
    }

    private void FixedUpdate()
    {
        Camera.main.transform.DOKill();

        Vector3 targetPos = m_player.position + m_offset;

        targetPos.x = Mathf.Clamp(targetPos.x, m_min.x, m_max.x);
        targetPos.y = Mathf.Clamp(targetPos.y, m_min.y, m_max.y);

        Camera.main.transform
            .DOMove(targetPos, 0.2f)
            .SetEase(Ease.OutQuad);
    }
}
