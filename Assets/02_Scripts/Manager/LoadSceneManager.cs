using DG.Tweening;
using Globals;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    private static LoadSceneManager m_instance;
    public static LoadSceneManager GetInstance() => m_instance;

    private string m_currentScene;
    public string CurrentScene { get => m_currentScene; set => m_currentScene = value; }

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
        SetCurrentScene(SceneName.TITLE);
    }

    public void LoadScene(string _scene)
    {
        SetCurrentScene(_scene);

        if (string.IsNullOrEmpty(_scene))
        {
            return;
        }

        DOTween.KillAll();
        SceneManager.LoadScene(_scene);
    }

    public void LoadNextScene(string _scene)
    {
        switch (_scene)
        {
            case SceneName.MAIN:
                GameManager.GetInstance().Player.SpawnPosition = new Vector2(-13, 7);
                LoadScene(SceneName.GAME);
                break;
            case SceneName.GAME:
                GameManager.GetInstance().Player.SpawnPosition = new Vector2(0.3f, 10.5f);
                LoadScene(SceneName.BOSSGAME);
                break;
        }
    }

    public void LoadPreScene(string _scene)
    {
        switch (_scene)
        {
            case SceneName.BOSSGAME:
                GameManager.GetInstance().Player.SpawnPosition = new Vector2(0.5f, -1);
                LoadScene(SceneName.GAME);
                break;
            case SceneName.GAME:
                GameManager.GetInstance().Player.SpawnPosition = new Vector2(13, 7);
                LoadScene(SceneName.MAIN);
                break;
        }
    }

    public void SetCurrentScene(string _scene)
    {
        m_currentScene = _scene;

        switch (m_currentScene)
        {
            case SceneName.TITLE:
                if (GameManager.GetInstance() != null && GameManager.GetInstance().Player != null)
                {
                    GameManager.GetInstance().Player.SpawnPosition = new Vector2(-13, 7);
                }
                break;
            case SceneName.MAIN:
                if (GameUIManager.GetInstance() != null)
                {
                    GameUIManager.GetInstance().Initialize();
                }
                break;
            case SceneName.GAME:
                break;
            case SceneName.BOSSGAME:
                break;
        }
    }
}
