using UnityEngine;

public class Enemy_03 : EnemyBase
{
    public override void Initialize()
    {
        base.Initialize();

        m_speed = 2f;
        m_playerRange = 7f;
        m_playerDistance = 3.5f;
        m_damage = 10;
        m_maxHp = 100;
        m_currentHp = m_maxHp;
    }
}
