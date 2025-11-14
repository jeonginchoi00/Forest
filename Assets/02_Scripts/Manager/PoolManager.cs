using AYellowpaper.SerializedCollections;
using UnityEngine;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{
    private static PoolManager m_instance;
    public static PoolManager GetInstance() => m_instance;

    [SerializeField] private SerializedDictionary<GameObject, List<GameObject>> m_pool;
    [SerializeField] private SerializedDictionary<GameObject, Transform> m_parentTransform;

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

    public GameObject Get(GameObject _prefab, Vector3 _position, Quaternion _rotation)
    {
        if (!m_pool.ContainsKey(_prefab))
        {
            m_pool[_prefab] = new List<GameObject>();
        }

        List<GameObject> list = m_pool[_prefab];

        for (int i = 0; i < list.Count; i++)
        {
            if (!list[i].activeSelf)
            {
                GameObject obj = list[i];
                obj.transform.SetPositionAndRotation(_position, _rotation);
                obj.SetActive(true);
                return obj;
            }
        }

        GameObject newObj = Instantiate(_prefab, _position, _rotation);
        newObj.transform.SetParent(m_parentTransform[_prefab]);
        list.Add(newObj);

        return newObj;
    }

    public void Return(GameObject _obj)
    {
        _obj.SetActive(false);
    }
}
