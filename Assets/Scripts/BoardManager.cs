using UnityEngine;
using System;
using System.Collections;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    public int blocks;
    public GameObject background;
    public GameObject[] floor;
	// Use this for initialization
	void Start ()
    {
        Instantiate(background);
	    for(int i=0; i<blocks; i++)
        {
            if(i==0)
            {
                Instantiate(floor[0], new Vector3(i,0.0f,0.0f), Quaternion.identity);
            }
            else if(i==blocks-1)
            {
                Instantiate(floor[2], new Vector3(i, 0.0f, 0.0f), Quaternion.identity);
            }
            else
            {
                Instantiate(floor[1], new Vector3(i, 0.0f, 0.0f), Quaternion.identity);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
