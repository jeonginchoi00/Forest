using UnityEngine;

public class Enemy_03 : EnemyBase
{
    public override void Start()
    {
        base.Start();

        m_speed = 2f;
        m_playerRange = 7f;
        m_playerDistance = 3.5f;
    }
}
