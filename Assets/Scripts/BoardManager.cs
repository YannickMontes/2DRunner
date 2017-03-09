using UnityEngine;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour
{
    public bool allowGeneration;
    public GameObject[] chunks;
    public GameObject ennemy;
    public GameObject background;
    public Transform playerPosition;
    public int chunkSize;
    public int nbChunksGenerated;

    private bool generate;
    private int beginGeneration = 0;
    private int endGeneration = 0;
    private int currentHeight = 0;
    private List<GameObject> chunksGenerated;
    private List<GameObject> backgroundGenerated;
    private List<GameObject> ennemiesGenerated;
    // Use this for initialization
    void Start ()
    {
        chunksGenerated = new List<GameObject>();
        backgroundGenerated = new List<GameObject>();
        ennemiesGenerated = new List<GameObject>();
        Generate();
	}
	
	// Update is called once per frame
	void Update ()
    {
        generate = (endGeneration - playerPosition.position.x < (chunkSize * nbChunksGenerated) ? true : false);
        Generate();
        DestroyOld();
	}

    #region Generate
    private void Generate()
    {
        if (allowGeneration && generate)
        {
            this.GenerateBackground();
            this.GenerateChunks();
            this.GenerateEnnemies();
            endGeneration += chunkSize;
        }
    }

    private void GenerateChunks()
    {
        if(beginGeneration !=0)
        {
            currentHeight += this.UpOrDown();       
        }
        GameObject chunkTmp = (GameObject)Instantiate(chunks[0], new Vector3(endGeneration, currentHeight*5, 0.0f), Quaternion.identity);
        chunksGenerated.Add(chunkTmp);
    }

    private void GenerateEnnemies()
    {
        int rand;
        do
        {
            rand = Random.Range(0, chunkSize);
        } while (!PositionOverlapOtherGameObject(new Vector2(endGeneration + rand, currentHeight * 5)));
        PositionOverlapOtherGameObject(new Vector2(endGeneration + rand, currentHeight * 5));
        GameObject ennemyTmp = Instantiate(ennemy, new Vector3(endGeneration + rand, currentHeight*5+2, 0.0f), Quaternion.identity);
        ennemiesGenerated.Add(ennemyTmp);
    }

    private void GenerateBackground()
    {
        GameObject bgTmp = (GameObject)Instantiate(background, new Vector3(endGeneration + ((chunkSize-1) / 2.0f), 10.0f, 0.0f), Quaternion.identity);
        backgroundGenerated.Add(bgTmp);
    }
    #endregion

    #region Destroy
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

    #endregion

    private bool PositionOverlapOtherGameObject(Vector2 position)
    {
        return Physics2D.OverlapBox(position, new Vector2(1, 1), 1);
    }

    private int UpOrDown()
    {
        float rand = Random.Range(0, 3);
        if (rand == 0)//No change
        {
            return 0;
        }
        else if (rand == 1)//UP
        {
            return 1;
        }
        else if (rand == 2)//down
        {
            return -1;
        }
        return 0;
    }
}
