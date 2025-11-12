using UnityEngine;

public class UserInfoManager : MonoBehaviour
{
    private static UserInfoManager m_instance;
    public static UserInfoManager GetInstance() => m_instance;

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
}
