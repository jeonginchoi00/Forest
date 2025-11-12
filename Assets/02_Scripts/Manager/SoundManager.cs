using AYellowpaper.SerializedCollections;
using Globals;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager m_instance;
    public static SoundManager GetInstance() => m_instance;

    [SerializeField] private AudioSource m_bgm;
    [SerializeField] private AudioSource m_sfx;

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
}
