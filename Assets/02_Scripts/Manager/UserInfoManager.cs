using UnityEngine;
using Globals;
using System.Collections.Generic;
using TMPro.EditorUtilities;

public class UserInfoManager : MonoBehaviour
{
    private static UserInfoManager m_instance;
    public static UserInfoManager GetInstance() => m_instance;

    private int m_coin;
    private int m_level;
    private int m_currentHp;
    private int m_maxHp;
    private int m_currentExp;
    private int m_maxExp;
    private bool m_hasBow;
    private Dictionary<QuestType, bool> m_questCompleted = new Dictionary<QuestType, bool>();

    public int Coin { get => m_coin; set => m_coin = value; }
    public int Level { get => m_level; set => m_level = value; }
    public int CurrentHp { get => m_currentHp; set => m_currentHp = value; }
    public int MaxHp { get => m_maxHp; set => m_maxHp = value; }
    public int CurrentExp { get => m_currentExp; set => m_currentExp = value; }
    public int MaxExp { get => m_maxExp; set => m_maxExp = value; }
    public bool HasBow { get => m_hasBow; set => m_hasBow = value; }

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

    public bool IsSave()
    {
        return
            PlayerPrefs.HasKey(UserInfoKey.USER_COIN)
            && PlayerPrefs.HasKey(UserInfoKey.USER_LEVEL)
            && PlayerPrefs.HasKey(UserInfoKey.USER_MAXHP)
            && PlayerPrefs.HasKey(UserInfoKey.USER_CURRENTHP)
            && PlayerPrefs.HasKey(UserInfoKey.USER_MAXEXP)
            && PlayerPrefs.HasKey(UserInfoKey.USER_CURRENTEXP);
    }

    public void UserInitialize() // 새로 시작
    {
        PlayerPrefs.DeleteAll();

        m_coin = 50000;
        m_level = 5;
        m_maxHp = 100;
        m_currentHp = m_maxHp;
        m_maxExp = 100;
        m_currentExp = 0;

        SaveUserData();
    }

    public void UserRebirth()
    {
        m_currentHp = m_maxHp / 10;

        SaveUserData();
    }

    public void LoadUserData() // 이어하기
    {
        m_coin = PlayerPrefs.GetInt(UserInfoKey.USER_COIN);
        m_level = PlayerPrefs.GetInt(UserInfoKey.USER_LEVEL);
        m_maxHp = PlayerPrefs.GetInt(UserInfoKey.USER_MAXHP);
        m_currentHp = PlayerPrefs.GetInt(UserInfoKey.USER_CURRENTHP);
        m_maxExp = PlayerPrefs.GetInt(UserInfoKey.USER_MAXEXP);
        m_currentExp = PlayerPrefs.GetInt(UserInfoKey.USER_CURRENTEXP);
        m_hasBow = PlayerPrefs.GetInt(UserInfoKey.USER_BOW, 0) == 1;

        foreach (QuestType _type in System.Enum.GetValues(typeof(QuestType)))
        {
            m_questCompleted[_type] = PlayerPrefs.GetInt($"QUEST_{_type}", 0) == 1;
        }
    }

    private void SaveUserData()
    {
        PlayerPrefs.SetInt(UserInfoKey.USER_COIN, m_coin);
        PlayerPrefs.SetInt(UserInfoKey.USER_LEVEL, m_level);
        PlayerPrefs.SetInt(UserInfoKey.USER_MAXHP, m_maxHp);
        PlayerPrefs.SetInt(UserInfoKey.USER_CURRENTHP, m_currentHp);
        PlayerPrefs.SetInt(UserInfoKey.USER_MAXEXP, m_maxExp);
        PlayerPrefs.SetInt(UserInfoKey.USER_CURRENTEXP, m_currentExp);
        PlayerPrefs.SetInt(UserInfoKey.USER_BOW, m_hasBow ? 1 : 0);

        foreach (KeyValuePair<QuestType, bool> _quest in m_questCompleted)
        {
            PlayerPrefs.SetInt($"QUEST_{_quest.Key}", _quest.Value ? 1 : 0);
        }

        PlayerPrefs.Save();
    }

    public void SetCoin(int _value)
    {
        m_coin += _value;
        SaveUserData();
    }

    public void SetHp(int _value)
    {
        m_currentHp -= _value;
        SaveUserData();
    }

    public void SetHpFull()
    {
        m_currentHp = m_maxHp;
        SaveUserData();
    }

    public void SetExp(int _value)
    {
        m_currentExp += _value;
        
        while (m_currentExp >= m_maxExp)
        {
            m_currentExp -= m_maxExp;
            SetLevel();
        }

        SaveUserData();
    }

    public void SetLevel()
    {
        m_level++;

        m_maxExp = Mathf.RoundToInt(m_maxExp * 1.20f);
        m_maxHp = Mathf.RoundToInt(m_maxHp * 1.10f);
        m_currentHp = m_maxHp;
        m_coin += 1000;

        SaveUserData();
    }

    public void SetBow(bool _value)
    {
        m_hasBow = _value;

        Page_HUD page = GameUIManager.GetInstance().GetPage<Page_HUD>(PageType.HUD);
        page.SetBuyBow();

        SaveUserData();
    }

    public void SetQuestCompleted(QuestType _type, bool _completed)
    {
        m_questCompleted[_type] = _completed;

        SaveUserData();
    }

    public bool IsQuestCompleted(QuestType _type)
    {
        if (m_questCompleted.ContainsKey(_type))
        {
            return m_questCompleted[_type];
        }

        return false;
    }
}
