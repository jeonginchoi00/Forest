using UnityEngine;

public class Enemy_01 : EnemyBase
{
    public override void Initialize()
    {
        base.Initialize();

        m_speed = 1f;
        m_playerRange = 3f;
        m_playerDistance = 1.2f;
        m_damage = 5;
        m_maxHp = 100;
        m_currentHp = m_maxHp;
    }
}
