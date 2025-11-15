using UnityEngine;

public class Enemy_01 : EnemyBase
{
    public override void Start()
    {
        base.Start();

        m_speed = 1f;
        m_playerRange = 3f;
        m_playerDistance = 1.2f;
    }
}
