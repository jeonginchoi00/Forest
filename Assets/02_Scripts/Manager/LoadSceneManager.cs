using Globals;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    private static LoadSceneManager m_instance;
    public static LoadSceneManager GetInstance() => m_instance;

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

    public void LoadScene(string _scene)
    {
        if (string.IsNullOrEmpty(_scene))
        {
            return;
        }

        SceneManager.LoadScene(_scene);
    }

    public void LoadNextScene(string _scene)
    {
        switch (_scene)
        {
            case SceneName.MAIN:
                PlayerBase.GetInstance().SpawnPosition = new Vector2(-13, 7);
                LoadScene(SceneName.GAME);
                break;
            case SceneName.GAME:
                PlayerBase.GetInstance().SpawnPosition = new Vector2(0.3f, 10.5f);
                LoadScene(SceneName.BOSSGAME);
                break;
        }
    }

    public void LoadPreScene(string _scene)
    {
        switch (_scene)
        {
            case SceneName.BOSSGAME:
                PlayerBase.GetInstance().SpawnPosition = new Vector2(0.5f, -1);
                LoadScene(SceneName.GAME);
                break;
            case SceneName.GAME:
                PlayerBase.GetInstance().SpawnPosition = new Vector2(13, 7);
                LoadScene(SceneName.MAIN);
                break;
        }
    }
}
