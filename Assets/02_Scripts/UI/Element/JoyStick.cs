using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform m_joyStick;
    [SerializeField] private RectTransform m_handler;

    private Vector2 m_moveDir;
    private Vector2 m_originPos;
    private Vector2 m_handlerBasePos;
    private float m_radius;
    private float m_radiusRatio = 0.33f;
    private bool m_isAvailable = true;

    private void Start()
    {
        GameManager.GetInstance().RegisterJoyStick(this);

        m_originPos = m_joyStick.anchoredPosition;
        m_handlerBasePos = m_handler.anchoredPosition;
        m_radius = m_joyStick.sizeDelta.y * m_radiusRatio;
    }

    private void OnDestroy()
    {
        m_handler.DOKill();
    }

    public void SetAvailable(bool _value)
    {
        m_isAvailable = _value;

        if (!_value)
        {
            ResetJoyStick();
        }
    }

    public void ResetJoyStick()
    {
        m_handler.DOKill();
        m_moveDir = Vector2.zero;
        GameManager.GetInstance().Player.MoveDir = m_moveDir;
        m_handler.anchoredPosition = m_handlerBasePos;
    }

    #region Interface
    public void OnDrag(PointerEventData eventData)
    {
        if (!m_isAvailable)
        {
            return;
        }

        Vector2 localPoint;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            m_joyStick.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint);

        Vector2 offset = localPoint - m_originPos;

        if (offset.magnitude > m_radius)
        {
            offset = offset.normalized * m_radius;
        }

        m_handler.anchoredPosition = m_handlerBasePos + offset;

        m_moveDir = offset.normalized;
        GameManager.GetInstance().Player.MoveDir = m_moveDir;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!m_isAvailable)
        {
            return;
        }

        m_handler.anchoredPosition = m_handlerBasePos;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!m_isAvailable)
        {
            return;
        }

        m_handler.DOKill();
        m_moveDir = Vector2.zero;
        GameManager.GetInstance().Player.MoveDir = m_moveDir;

        m_handler.DOAnchorPos(m_handlerBasePos, 0.2f)
                 .SetEase(Ease.OutQuad);
    }
    #endregion
}
