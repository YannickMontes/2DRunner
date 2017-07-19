using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMultipleObjectPooler : MonoBehaviour
{

    public List<GameObject> pooledObject;
    public int pooledAmount;
    public bool expandable;

    private List<List<GameObject>> pooledObjects;

    void Start()
    {
        this.pooledObjects = new List<List<GameObject>>();
        for (int i = 0; i < this.pooledObject.Count; i++)
        {
            this.pooledObjects[i] = new List<GameObject>();
            for (int j = 0; j < this.pooledAmount; j++)
            {
                GameObject tmp = (GameObject)Instantiate(pooledObject[i]);
                tmp.SetActive(false);
                this.pooledObjects[i].Add(tmp);
            }         
        }
    }
    /*
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        if (expandable)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            pooledObjects.Add(obj);
            return obj;
        }

        return null;
    }*/
}
