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
}
