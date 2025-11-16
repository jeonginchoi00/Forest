using UnityEngine;
using UnityEngine.UI;
using Globals;
using DG.Tweening;

public class Page_Title : MonoBehaviour
{
    [SerializeField] private RawImage m_rawImage;
    [SerializeField] private Button m_startBtn;
    [SerializeField] private Button m_continueBtn;
    [SerializeField] private RectTransform m_title;

    private void Start()
    {
        TitleAnim();

        m_continueBtn.interactable = UserInfoManager.GetInstance().IsSave();

        m_startBtn.onClick.AddListener(OnClickStartBtn);
        m_continueBtn.onClick.AddListener(OnClickContinueBtn);
    }

    private void Update()
    {
        BackgroundAnim();
    }

    private void OnDestroy()
    {
        DOTweenAllKill();
    }

    private void DOTweenAllKill()
    {
        m_title.DOKill();
    }

    private void BackgroundAnim()
    {
        Rect rect = m_rawImage.uvRect;
        rect.x += 0.2f * Time.deltaTime;
        m_rawImage.uvRect = rect;
    }

    private void TitleAnim()
    {
        m_title.DOKill();

        Vector2 startPos = m_title.anchoredPosition;

        m_title
            .DOAnchorPosY(startPos.y + -20f, 0.5f)
            .SetEase(Ease.InOutQuad)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnClickStartBtn()
    {
        UserInfoManager.GetInstance().UserInitialize();
        LoadSceneManager.GetInstance().LoadScene(SceneName.MAIN);
    }

    private void OnClickContinueBtn()
    {
        UserInfoManager.GetInstance().LoadUserData();
        LoadSceneManager.GetInstance().LoadScene(SceneName.MAIN);
    }
}
