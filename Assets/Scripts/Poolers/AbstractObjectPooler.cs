using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractObjectPooler : MonoBehaviour {

    public List<GameObject> pooledObject;
    public int pooledAmount;
    public bool expandable;

    public List<GameObject> pooledObjects;

    void Start ()
    {
        this.pooledObjects = new List<GameObject>();
        for (int i = 0; i < this.pooledAmount; i++)
        {
			GameObject tmp = (GameObject)Instantiate(GetRandomPrefab());
            tmp.SetActive(false);
            this.pooledObjects.Add(tmp);
        }	
	}

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                pooledObjects[i].SetActive(true);
                return pooledObjects[i];
            }
        }

        if (expandable)
        {
			GameObject obj = (GameObject)Instantiate(GetRandomPrefab());
            pooledObjects.Add(obj);
            return obj;
        }

        return null;
    }



	private GameObject GetRandomPrefab()
	{
		if (pooledObject.Count == 1)
			return pooledObject [0];
		else {
			return pooledObject [Random.Range(0, pooledObject.Count)];
		}
	}
}
