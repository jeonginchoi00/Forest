using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using AYellowpaper.SerializedCollections;
using Globals;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    private static GameUIManager m_instance;
    public static GameUIManager GetInstance() => m_instance;

    private PageType m_currentPage;
    private PopupType m_currentPopup;

    [SerializeField] private SerializedDictionary<PageType, PageTemplate> m_pageTemplates;
    [SerializeField] private SerializedDictionary<PopupType, PopupTemplate> m_popupTemplates;

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

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        // 페이지 초기화
        foreach (KeyValuePair<PageType, PageTemplate> page in m_pageTemplates)
        {
            m_pageTemplates[page.Key].Initialize();
        }

        // 팝업 초기화
        foreach (KeyValuePair<PopupType, PopupTemplate> popup in m_popupTemplates)
        {
            m_popupTemplates[popup.Key].Initialize();
            m_popupTemplates[popup.Key].InActivePopup();
        }
    }

    #region Page
    public T GetPage<T>(PageType _pageType) where T : PageTemplate
    {
        if (m_pageTemplates.ContainsKey(_pageType))
        {
            return m_pageTemplates[_pageType] as T;
        }

        return null;
    }

    public void SetCurrentPage(PageType _pageType)
    {
        if (m_pageTemplates.ContainsKey(m_currentPage))
        {
            m_pageTemplates[m_currentPage].InActivePage();
        }

        if (m_pageTemplates.ContainsKey(_pageType))
        {
            m_pageTemplates[_pageType].ActivePage();
            m_currentPage = _pageType;
        }
    }
    #endregion

    #region Popup
    public T GetPopup<T>(PopupType _popupType) where T : PopupTemplate
    {
        if (m_popupTemplates.ContainsKey(_popupType))
        {
            return m_popupTemplates[_popupType] as T;
        }

        return null;
    }

    public void ShowPopup(PopupType _popupType)
    {
        if (m_popupTemplates.ContainsKey(m_currentPopup))
        {
            m_popupTemplates[m_currentPopup].InActivePopup();
        }

        if (m_popupTemplates.ContainsKey(_popupType))
        {
            m_popupTemplates[_popupType].ActivePopup();
            m_currentPopup = _popupType;
        }
    }

    public void ClosePopup(PopupType _popupType)
    {
        if (m_popupTemplates.ContainsKey(_popupType))
        {
            m_popupTemplates[_popupType].InActivePopup();

            if (m_currentPopup == _popupType)
            {
                m_currentPopup = PopupType.NONE;
            }
        }
    }
    #endregion
}
