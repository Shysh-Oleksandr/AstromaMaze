using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
        public bool shouldExpand;
    }

    public List<Pool> pools;
    public Dictionary<string, List<GameObject>> poolDictionary;
    public SprayItem sprayItem;

    public int sprayAmount;

    #region Singleton
    public static ObjectPooler instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    private void Start()
    {
        poolDictionary = new Dictionary<string, List<GameObject>>();

        foreach (Pool pool in pools)
        {
            List<GameObject> pooledObjects = new List<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = (GameObject)Instantiate(pool.prefab);
                obj.SetActive(false);
                pooledObjects.Add(obj);
                obj.transform.SetParent(this.transform);

                if (pool.tag == "Paint")
                {
                    pool.size = sprayItem.sprayAmount;
                    sprayAmount = pool.size;
                }
            }

            poolDictionary.Add(pool.tag, pooledObjects);
        }

    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        for (int i = 0; i < poolDictionary[tag].Count; i++)
        {
            // if the pooled objects is NOT active, return that object 
            if (!poolDictionary[tag][i].activeInHierarchy)
            {
                poolDictionary[tag][i].transform.position = position;
                poolDictionary[tag][i].transform.rotation = rotation;
                return poolDictionary[tag][i];
            }
        }

        foreach (Pool pool in pools)
        {
            if (pool.tag == tag)
            {
                if (pool.shouldExpand)
                {
                    GameObject obj = (GameObject)Instantiate(pool.prefab);
                    obj.SetActive(false);
                    obj.transform.position = position;
                    obj.transform.rotation = rotation;
                    poolDictionary[tag].Add(obj);
                    return obj;
                }
            }
        }


        Debug.Log("There are no objects in the pool.");
        // otherwise, return null   
        return null;

    }
}
