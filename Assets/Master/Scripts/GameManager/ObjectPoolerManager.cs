using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolerManager : MonoBehaviour
{

    [System.Serializable]
    public class PoolObject
    {

        public string TagId;
        public GameObject Prefab;
        public int Amount;

    }

    #region Singleton

    private static ObjectPoolerManager _instance;
    private ObjectPoolerManager()
    {
        _instance = this;
    }

    public static ObjectPoolerManager Instance { get => _instance; }


    #endregion

    public List<PoolObject> poolObjects = new List<PoolObject>();

    public Dictionary<string, Queue<GameObject>> poolsDictionary = new Dictionary<string, Queue<GameObject>>();

    public void InitializePoolObject(string poolObjectTag)
    {
        foreach (PoolObject pool in poolObjects)
        {

            if (pool.TagId == poolObjectTag)
            {

                Queue<GameObject> objectPool = new Queue<GameObject>();

                for (int i = 0; i < pool.Amount; i++)
                {
                    GameObject obj = Instantiate(pool.Prefab);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                poolsDictionary.Add(pool.TagId, objectPool);

                break;

            }

        }
    }

    public GameObject SpawnObjectFromPool(string poolObjectsTag, Vector3 position, Quaternion rotation)
    {

        if (!poolsDictionary.ContainsKey(poolObjectsTag))
            InitializePoolObject(poolObjectsTag);
        
        GameObject cacheObjToSpawn = poolsDictionary[poolObjectsTag].Dequeue();

        cacheObjToSpawn.SetActive(true);
        cacheObjToSpawn.transform.position = position;
        cacheObjToSpawn.transform.rotation = rotation;

        poolsDictionary[poolObjectsTag].Enqueue(cacheObjToSpawn);

        return cacheObjToSpawn;

    }

    public void DestroyPoolObjectFromScene(string poolObjectsTag, GameObject gameObject)
    {
        if (poolsDictionary.ContainsKey(poolObjectsTag))
            if (poolsDictionary[poolObjectsTag].Contains(gameObject))
                gameObject.SetActive(false);    
    }

}