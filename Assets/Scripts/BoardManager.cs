using UnityEngine;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour
{
    public bool allowGeneration;
    public GameObject[] chunks;
    public Transform playerPosition;
    public int chunkSize;

    private bool generate;
    public int beginGeneration = 0;
    private int endGeneration = 0;
    private List<GameObject> chunksGenerated;
	// Use this for initialization
	void Start ()
    {
        chunksGenerated = new List<GameObject>();
        GenerateTerrain();
	}
	
	// Update is called once per frame
	void Update ()
    {
        generate = (endGeneration - playerPosition.position.x < chunkSize ? true : false);
        GenerateTerrain();
        DeleteOldTerrain();
	}

    private void DeleteOldTerrain()
    {
        if(playerPosition.position.x - beginGeneration > chunkSize * 2)
        {
            GameObject tmp = chunksGenerated[0];
            chunksGenerated.RemoveAt(0);
            Destroy(tmp);
            beginGeneration += chunkSize;
        }
    }

    public void GenerateTerrain()
    {
        if(allowGeneration && generate)
        {
            GameObject tmp = (GameObject)Instantiate(chunks[0], new Vector3(endGeneration, 0.0f, 0.0f), Quaternion.identity);
            chunksGenerated.Add(tmp);
            endGeneration += chunkSize;
        }
    }
}
