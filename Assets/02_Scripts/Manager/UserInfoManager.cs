using UnityEngine;
using Globals;

public class UserInfoManager : MonoBehaviour
{
    private static UserInfoManager m_instance;
    public static UserInfoManager GetInstance() => m_instance;

    private int m_coin = 0;
    private int m_level = 1;
    private int m_currentHp;
    private int m_maxHp;
    private int m_exp;

    public int Coin => m_coin;
    public int Level => m_level;
    public int CurrentHp => m_currentHp;
    public int MaxHp => m_maxHp;
    public int Exp => m_exp;

    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
            DontDestroyOnLoad(gameObject);

            LoadUserData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadUserData()
    {
        m_coin = PlayerPrefs.GetInt(UserInfoKey.USER_COIN);
    }

    private void SaveUserData()
    {
        PlayerPrefs.SetInt(UserInfoKey.USER_COIN, m_coin);

        PlayerPrefs.Save();
    }

    public void SetCoin(int _value)
    {
        m_coin += _value;
        SaveUserData();
    }
}
