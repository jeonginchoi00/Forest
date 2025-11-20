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
        m_maxHp = 500;
        m_currentHp = m_maxHp;
        m_exp = 100;
        m_coin = 10000;
    }
}
