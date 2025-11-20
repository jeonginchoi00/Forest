using System.Collections;
using UnityEngine;

public class EffectPool : MonoBehaviour
{
    [SerializeField] private ParticleSystem m_particleSystem;
    [SerializeField] private Animator m_animator;

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(CoWaitAndReturn());
    }

    private IEnumerator CoWaitAndReturn()
    {
        if (m_particleSystem != null)
        {
            yield return new WaitWhile(() => m_particleSystem.IsAlive(true));
        }

        if (m_animator != null)
        {
            AnimatorStateInfo info = m_animator.GetCurrentAnimatorStateInfo(0);
            yield return new WaitForSeconds(info.length);
        }

        PoolManager.GetInstance().Return(gameObject);
    }
}
