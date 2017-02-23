using UnityEngine;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour
{
    public bool allowGeneration;
    public GameObject[] chunks;
    public GameObject background;
    public Transform playerPosition;
    public int chunkSize;

    private bool generate;
    public int beginGeneration = 0;
    private int endGeneration = 0;
    private List<GameObject> chunksGenerated;
    private List<GameObject> backgroundGenerated;
	// Use this for initialization
	void Start ()
    {
        chunksGenerated = new List<GameObject>();
        backgroundGenerated = new List<GameObject>();
        Generate();
	}
	
	// Update is called once per frame
	void Update ()
    {
        generate = (endGeneration - playerPosition.position.x < chunkSize ? true : false);
        Generate();
        DestroyOld();
	}

    private void Generate()
    {
        if (allowGeneration && generate)
        {
            this.GenerateBackground();
            this.GenerateChunks();
            endGeneration += chunkSize;
        }
    }

    private void GenerateChunks()
    {
        GameObject chunkTmp = (GameObject)Instantiate(chunks[0], new Vector3(endGeneration, 0.0f, 0.0f), Quaternion.identity);
        chunksGenerated.Add(chunkTmp);
    }

    private void GenerateBackground()
    {
        GameObject bgTmp = (GameObject)Instantiate(background, new Vector3(endGeneration + ((chunkSize-1) / 2.0f), 10.0f, 0.0f), Quaternion.identity);
        backgroundGenerated.Add(bgTmp);
    }

    private void DestroyOld()
    {
        if (playerPosition.position.x - beginGeneration > chunkSize * 2)
        {
            this.DestroyOldBackground();
            this.DestroyOldChunks();
            beginGeneration += chunkSize;
        }
    }

    private void DestroyOldChunks()
    { 
        GameObject chunkTmp = chunksGenerated[0];
        chunksGenerated.RemoveAt(0);
        Destroy(chunkTmp);
    }

    private void DestroyOldBackground()
    {
        GameObject bgTmp = backgroundGenerated[0];
        backgroundGenerated.RemoveAt(0);
        Destroy(bgTmp);
    }

    
}
