using Globals;
using UnityEngine;

public class PageTemplate : MonoBehaviour
{
    [SerializeField] private PageType m_pageType;

    public virtual void Initialize()
    {

    }

    public virtual void ActivePage()
    {
        gameObject.SetActive(true);
    }

    public virtual void InActivePage()
    {
        gameObject.SetActive(false);
    }
}
