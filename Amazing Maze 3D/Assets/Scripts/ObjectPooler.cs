using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler instance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool, sprayAmount, footprintAmountToPool;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // For paintings. 
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            obj.transform.SetParent(this.transform); // set as children of Spawn Manager
        }

        /*// For footprints.
        pooledFootprints = new List<GameObject>();
        for (int i = 0; i < footprintAmountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(footprintToPool);
            obj.SetActive(false);
            pooledFootprints.Add(obj);
            obj.transform.SetParent(this.transform); // set as children of Spawn Manager
        }*/

        sprayAmount = amountToPool;
    }

    public GameObject GetPooledObject()
    {
        // For as many objects as are in the pooledObjects list
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            // if the pooled objects is NOT active, return that object 
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        Debug.Log("There are no objects in the pool.");
        // otherwise, return null   
        return null;
    }

    /*public GameObject GetPooledFootprint()
    {
        // For as many objects as are in the pooledObjects list
        for (int i = 0; i < pooledFootprints.Count; i++)
        {
            // if the pooled objects is NOT active, return that object 
            if (!pooledFootprints[i].activeInHierarchy)
            {
                return pooledFootprints[i];
            }
        }

        Debug.Log("There are no footprints in the pool.");
        // otherwise, return null   
        return null;
    }*/


}
