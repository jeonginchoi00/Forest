using AYellowpaper.SerializedCollections;
using Globals;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager m_instance;
    public static SoundManager GetInstance() => m_instance;

    [SerializeField] private AudioSource m_bgm;
    [SerializeField] private AudioSource m_sfx;
    [SerializeField] private AudioSource m_sfx_walk;

    [SerializeField] private SerializedDictionary<SoundType, AudioClip> m_bgmClips;
    [SerializeField] private SerializedDictionary<SoundType, AudioClip> m_sfxClips;

    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(SoundType _type)
    {
        if (m_bgmClips.TryGetValue(_type, out AudioClip _clip))
        {
            m_bgm.clip = _clip;
            m_bgm.loop = true;
            m_bgm.Play();
        }
    }

    public void PlaySFX(SoundType _type)
    {
        if (m_sfxClips.TryGetValue(_type, out AudioClip _clip))
        {
            m_sfx.PlayOneShot(_clip);
        }
    }

    public void PlaySFXWalk(SoundType _type)
    {
        if (m_sfxClips.TryGetValue(_type, out AudioClip _clip))
        {
            if (m_sfx_walk.clip != _clip || !m_sfx_walk.isPlaying)
            {
                m_sfx_walk.clip = _clip;
                m_sfx_walk.loop = true;
                m_sfx_walk.Play();
            }
        }
    }

    public void StopSFXWalk()
    {
        if (m_sfx_walk.isPlaying)
        {
            m_sfx_walk.Stop();
        }
    }
}
