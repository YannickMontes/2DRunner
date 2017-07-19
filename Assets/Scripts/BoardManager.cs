using UnityEngine;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour
{
    public Transform playerPosition;

    private int currentXGeneration = 0;
    private int currentYGeneration = 0;
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
        int jumpSize = 0;
        if (currentXGeneration > 10)
        {
            //First choose a size for the jump
            jumpSize = RandomGeneration(0, 5);
            //Also choose at wich height it will be placed
            currentYGeneration += UpOrDown();
        }
        
        //Then choose a size for the plateform
        int plateformSize = RandomGeneration(3, 15);
        
        //Add to currentGeneration the size of the jump
        currentXGeneration += jumpSize;

        //Create the gameobject that will contain the plateform
        GameObject plateform = new GameObject();
        plateform.layer = LayerMask.NameToLayer("Ground");
        //Place it at the desired destination
        plateform.transform.position = new Vector2(currentXGeneration, currentYGeneration);
        //Then add composant to make graphical plateform
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
        rightTile.transform.localPosition = new Vector2(plateformSize-1, 0);

        BoxCollider2D plateformCollider = plateform.AddComponent<BoxCollider2D>();
        plateformCollider.size = new Vector2(plateformSize, 0.735f);
        float xOffset = ((plateformSize / 2) - 0.5f);
        plateformCollider.offset = new Vector2(xOffset, 0.0f);

        //Then add the size of the plateform to the currentGeneration
        currentXGeneration += plateformSize;
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
