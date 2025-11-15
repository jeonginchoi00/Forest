using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private GameObject m_joyStick;
    [SerializeField] private GameObject m_handler;

    private Vector2 m_moveDir;
    private Vector2 m_touchPos;
    private Vector2 m_originPos;
    private Vector2 m_handlerBasePos = new Vector2(200, 200);
    private float m_radius;
    private float m_minX = 50f;
    private float m_maxX = 350f;
    private float m_minY = 50f;
    private float m_maxY = 350f;

    private void Start()
    {
        m_originPos = m_joyStick.transform.position;
        m_handler.transform.position = m_handlerBasePos;
        m_radius = m_joyStick.GetComponent<RectTransform>().sizeDelta.y / 3;
    }

    private void OnDestroy()
    {
        DOTweenAllKill();
    }

    private void DOTweenAllKill()
    {
        m_handler.transform.DOKill();
    }

    #region Interface
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 dragPos = eventData.position;
        dragPos.x = Mathf.Clamp(dragPos.x, m_minX, m_maxX);
        dragPos.y = Mathf.Clamp(dragPos.y, m_minY, m_maxY);

        m_moveDir = (dragPos - m_touchPos).normalized;
        float distance = (dragPos - m_originPos).sqrMagnitude;

        Vector2 newPos;

        if (distance < m_radius)
        {
            newPos = m_handlerBasePos + (m_moveDir * distance);
        }
        else
        {
            newPos = m_handlerBasePos + (m_moveDir * m_radius);
        }

        newPos.x = Mathf.Clamp(newPos.x, m_minX, m_maxX);
        newPos.y = Mathf.Clamp(newPos.y, m_minY, m_maxY);

        m_handler.transform.position = newPos;
        GameManager.GetInstance().Player.MoveDir = m_moveDir;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsWithinBounds(eventData.position))
        {
            m_touchPos = eventData.position;
            m_handler.transform.position = m_handlerBasePos;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_handler.transform.DOKill();

        m_moveDir = Vector2.zero;
        m_handler.transform
            .DOMove(m_handlerBasePos, 0.2f)
            .SetEase(Ease.OutBack);
        GameManager.GetInstance().Player.MoveDir = m_moveDir;
    }
    #endregion

    private bool IsWithinBounds(Vector2 _position)
    {
        return _position.x >= m_minX
            && _position.x <= m_maxX
            && _position.y >= m_minY
            && _position.y <= m_maxY;
    }
}
