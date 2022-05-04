using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectPoolExtension
{
    public static GameObject InstantiatePool(this GameObject objectContext, GameObject gameObject, Vector3 position, Quaternion rotation)
    {

        return ObjectPoolerManager.Instance.SpawnObjectFromPool(gameObject, position, rotation);

    }

    public static void DestroyPool(this GameObject objectContext, GameObject gameObject)
    {

        ObjectPoolerManager.Instance.DestroyPoolObjectFromScene(gameObject);

    }

    public static void DestroyAllPool()
    {
        ObjectPoolerManager.Instance.DestroyAllPool();
    }

}

public class ObjectPoolerManager : MonoBehaviour
{
    [System.Serializable]
    public struct PoolData<T>
    {
        public PoolData(T poolObj, bool expandable, int poolAmount = 5)
        {
            poolObject = poolObj;
            amount = poolAmount;
            isExpandable = expandable;
        }

        public T poolObject;
        public int amount;
        public bool isExpandable;
    }


    #region Singleton

    private static ObjectPoolerManager _instance;
    private ObjectPoolerManager()
    {
        _instance = this;
    }

    public static ObjectPoolerManager Instance { get => _instance; }

    #endregion

    [Tooltip("Hide Object at Hierarchy at spawning")]
    public bool hiddenOnSpawn = false;


    [HideInInspector] public PoolData<GameObject>[] m_poolObjects;

    public Dictionary<string, List<GameObject>> _poolsDictionary = new Dictionary<string, List<GameObject>>();

    private void Start()
    {
        InitializePoolObjects();
    }

    public void InitializePoolObjects()
    {
        _poolsDictionary.Clear();

        List<GameObject> cachePoolList = new List<GameObject>();

        for (int i = 0; i < m_poolObjects.Length; i++)
        {
            cachePoolList.Clear();
            string cachePoolId = m_poolObjects[i].poolObject.name.ToLower();


            for (int it = 0; it < m_poolObjects[i].amount; it++)
            {

                GameObject cacheGO = null;

                cacheGO = Instantiate(m_poolObjects[i].poolObject);

                if (hiddenOnSpawn)
                    cacheGO.hideFlags = HideFlags.HideInHierarchy;
                
                cacheGO.transform.SetParent(transform);
                cacheGO.SetActive(false);

                cachePoolList.Add(cacheGO);

            }

            _poolsDictionary.Add(cachePoolId, cachePoolList);

        }
    }

    public PoolData<GameObject> GetPoolObject(GameObject gObject)
    {
        PoolData<GameObject> cachePoolObj = new PoolData<GameObject>();

        for (int i = 0; i < m_poolObjects.Length; i++)
        {
            if (m_poolObjects[i].poolObject.name == gObject.name)
            {
                cachePoolObj = m_poolObjects[i];
                break;
            }
        }

        return cachePoolObj;

    }

    public GameObject SpawnObjectFromPool(GameObject gObject, Vector3 position, Quaternion rotation)
    {
        string cacheID = gObject.name.ToLower();
        GameObject cachedPoolObj = null;

        if (_poolsDictionary.ContainsKey(cacheID))
        {
            cachedPoolObj = _poolsDictionary[cacheID].Find((predicate) => 
            {
                if (predicate.activeInHierarchy == false)
                    return true;
                else
                    return false;
            
            });

            if (GetPoolObject(gObject).isExpandable && !cachedPoolObj)
            {
                cachedPoolObj = Instantiate(gObject);
                cachedPoolObj.transform.SetParent(transform);
                if (hiddenOnSpawn)
                    cachedPoolObj.hideFlags = HideFlags.HideInHierarchy;

                for (int i = 0; i < m_poolObjects.Length; i++)
                {
                    if (m_poolObjects[i].poolObject.name == gObject.name)
                    {
                        m_poolObjects[i].amount++;
                        break;
                    }
                }

                _poolsDictionary[cacheID].Add(cachedPoolObj);
            }
        }
        //Add New Categories in Pooling
        else
        {
            cachedPoolObj = Instantiate(gObject);
            cachedPoolObj.transform.SetParent(transform);
            if (hiddenOnSpawn)
                cachedPoolObj.hideFlags = HideFlags.HideInHierarchy;
            
            _poolsDictionary.Add(cacheID, new List<GameObject> { cachedPoolObj });

            int nextSize = m_poolObjects.Length + 1;
            System.Array.Resize(ref m_poolObjects, nextSize);
            m_poolObjects[nextSize - 1] = new PoolData<GameObject>(gObject, true, 1);

        }

        if (cachedPoolObj)
        {
            cachedPoolObj.SetActive(true);
            cachedPoolObj.transform.position = position;
            cachedPoolObj.transform.rotation = rotation;
        }

        return cachedPoolObj;

    }

    public void DestroyPoolObjectFromScene(GameObject gObject)
    {
        gObject.SetActive(false);
    }

    public void DestroyAllPool()
    {
        foreach (var pcached in _poolsDictionary.Values)
        {
            foreach (var poolItem in pcached)
            {
                if (poolItem.activeInHierarchy)
                    poolItem.SetActive(false);
            }
        }
    }

}