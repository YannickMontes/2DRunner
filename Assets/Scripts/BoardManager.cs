using UnityEngine;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour
{
    public Transform playerPosition;

    private int currentXGeneration = 0;
    private int currentYGeneration = 0;
    private GameObject lastGeneratedPlateform;
    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (currentXGeneration - 25 < playerPosition.position.x)
        {
            Generate();
        }
	}

    private void Generate()
    {
        GeneratePlateform();
        GenerateObjects();
    }

    private void GeneratePlateform()
    {
        int jumpSize = 0;
        int plateformSize = 0;

        if (currentXGeneration == 0)
        {
            plateformSize = 30;
        }
        else
        {
            plateformSize = RandomGeneration(9, 25);
            jumpSize = RandomGeneration(2, 7);
            currentYGeneration += UpOrDown();
        }

        CreatePlateformObject(jumpSize, plateformSize);

        currentXGeneration += plateformSize;
    }

    private void CreatePlateformObject(int jumpSize, int plateformSize)
    {
        lastGeneratedPlateform = new GameObject();
        lastGeneratedPlateform.tag = "Ground";
        lastGeneratedPlateform.layer = LayerMask.NameToLayer("Ground");

        currentXGeneration += jumpSize;
        lastGeneratedPlateform.transform.position = new Vector2(currentXGeneration, currentYGeneration);

        ConstructPlateformWithTiles(lastGeneratedPlateform, plateformSize);

        BoxCollider2D plateformCollider = lastGeneratedPlateform.AddComponent<BoxCollider2D>();
        plateformCollider.size = new Vector2(plateformSize, 0.735f);
        float xOffset = ((plateformSize / 2f) - 0.5f);
        plateformCollider.offset = new Vector2(xOffset, 0.0f);
        lastGeneratedPlateform.AddComponent<PlateformDestroyer>();
    }

    private void ConstructPlateformWithTiles(GameObject plateform, int plateformSize)
    {
        GameObject leftTile = LeftSidePlateformPooler.current.GetPooledObject();
        leftTile.transform.parent = plateform.transform;
        leftTile.transform.localPosition = new Vector2(0, 0);

        for (int i = 1; i < plateformSize - 1; i++)
        {
            GameObject middleTile = MiddleTilePlateformPooler.current.GetPooledObject();
            middleTile.transform.parent = plateform.transform;
            middleTile.transform.localPosition = new Vector2(i, 0);
        }

        GameObject rightTile = RightSidePlateformPooler.current.GetPooledObject();
        rightTile.transform.parent = plateform.transform;
        rightTile.transform.localPosition = new Vector2(plateformSize - 1, 0);
    }

    private void GenerateObjects()
    {
        if (Random.Range(0f, 1f) > 0.25)
        {
            CreateCrate();
        }
    }

    private void CreateCrate()
    {
        GameObject crate = CratePooler.current.GetPooledObject();
        crate.transform.position = new Vector2(RandomGeneration((int)lastGeneratedPlateform.transform.position.x, this.currentXGeneration), this.currentYGeneration+0.85f);
    }
    
    private bool PositionOverlapOtherGameObject(Vector2 position)
    {
        return Physics2D.OverlapBox(position, new Vector2(1, 1), 1);
    }

    private int RandomGeneration(int min, int max)
    {
        return Random.Range(min, max);
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
