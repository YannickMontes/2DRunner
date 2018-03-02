using UnityEngine;
using UnityEngine.SceneManagement;

public class BoardManager : MonoBehaviour
{
	private const int Y_LOWER_LIMIT = -3;
	private const int Y_HIGHER_LIMIT = 5;
	private const int X_DISTANCE_TO_GENERATION = 25;
	private const float DISTANCE_BETWEEN_2_BGS = 51.2f;
	private const int MIN_JUMP_SIZE = 3;
	private const int MAX_JUMP_SIZE = 10;
	private const int MIN_PLATEFORM_SIZE = 9;
	private const int MAX_PLATEFORM_SIZE = 40;


    public Transform playerPosition;

    private ObjectPooler objectPooler;
    private int currentXGeneration = 0;
    private int currentYGeneration = 0;
    private GameObject lastGeneratedPlateform;
	private float lastBGPosition = -77.4f;
    private bool gameOver;
    // Use this for initialization
    void Start ()
    {
        this.objectPooler = ObjectPooler.instance;
        this.gameOver = false;
		GenerateBackground ();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!gameOver)
        {
            if (currentXGeneration - X_DISTANCE_TO_GENERATION < playerPosition.position.x)
            {
                Generate();
            }
            if (playerPosition.position.x - lastBGPosition > -5)
            {
                GenerateBackground();
            }
        }
	}

    public bool IsGameOver()
    {
        return gameOver;
    }

    private void Generate()
    {
        GeneratePlateform();
        if (lastGeneratedPlateform.transform.position.x > 0)
        {
            GenerateObjects();
            GenerateEnnemies();
        }
    }

	private void GenerateBackground()
	{
		lastBGPosition += DISTANCE_BETWEEN_2_BGS;
        objectPooler.SpawnFromPool("Background", new Vector3(lastBGPosition, 3.0f, 0.0f), Quaternion.identity);
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
			plateformSize = RandomGeneration(MIN_PLATEFORM_SIZE, MAX_PLATEFORM_SIZE);
			jumpSize = RandomGeneration(MIN_JUMP_SIZE, MAX_JUMP_SIZE);
			if (HeightLimitsReached()) {
				currentYGeneration += UpOrDown();
			}
            
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

		if (lastGeneratedPlateform.GetComponent<BoxCollider2D> () == null) {
			BoxCollider2D plateformCollider = lastGeneratedPlateform.AddComponent<BoxCollider2D>();
			plateformCollider.size = new Vector2(plateformSize, 0.735f);
			float xOffset = ((plateformSize / 2f) - 0.5f);
			plateformCollider.offset = new Vector2(xOffset, 0.0f);
		}
		if(lastGeneratedPlateform.GetComponent<PlateformDestroyer>() == null)
        	lastGeneratedPlateform.AddComponent<PlateformDestroyer>();
    }

    private void ConstructPlateformWithTiles(GameObject plateform, int plateformSize)
    {
        GameObject leftTile = objectPooler.SpawnFromPool("LeftTile", plateform.transform.position, Quaternion.identity);
        leftTile.transform.parent = plateform.transform;
        leftTile.transform.localPosition = new Vector2(0, 0);

        for (int i = 1; i < plateformSize - 1; i++)
        {
            GameObject middleTile = objectPooler.SpawnFromPool("MiddleTile", plateform.transform.position, Quaternion.identity);
            middleTile.transform.parent = plateform.transform;
            middleTile.transform.localPosition = new Vector2(i, 0);
        }

        GameObject rightTile = objectPooler.SpawnFromPool("RightTile", plateform.transform.position, Quaternion.identity);
        rightTile.transform.parent = plateform.transform;
        rightTile.transform.localPosition = new Vector2(plateformSize - 1, 0);
    }

    private void GenerateObjects()
    {
		int positionUsed = 0;
        if (Random.Range(0f, 1f) > 0.25)
        {
			positionUsed = CreateCrate();
        }
		if (Random.Range (0f, 1f) > 0.1) 
		{
			CreateCactus (positionUsed);
		}
    }

    private int CreateCrate()
    {
        GameObject crate = objectPooler.SpawnFromPool("Crate", new Vector2(RandomGeneration((int)lastGeneratedPlateform.transform.position.x, this.currentXGeneration), this.currentYGeneration + 0.85f), Quaternion.identity);
		return (int)crate.transform.position.x;
    }

	private void CreateCactus(int usedPos)
	{
		GameObject cactus = CactusPooler.current.GetPooledObject ();
		float height = 0f;
		string name = cactus.GetComponent<SpriteRenderer> ().sprite.name;
		if(name == "Cactus (2)")
		{
			height = this.currentYGeneration + 0.81f;
		}
		else if(name == "Cactus (3)")
		{
			height = this.currentYGeneration + 1.33f;
		}
		else if(name == "Cactus (1)")
		{
			height = this.currentYGeneration + 1.48f;
		}
		do {
			cactus.transform.position = new Vector2(RandomGeneration((int)lastGeneratedPlateform.transform.position.x, this.currentXGeneration), height);
		} while(cactus.transform.position.x == usedPos);
	}

    private void GenerateEnnemies()
    {
        if (Random.Range(0f, 1f) > 0.25)
        {
            CreateZombie();
        }
    }

    private void CreateZombie()
    {

    }
    
    private bool PositionOverlapOtherGameObject(Vector2 position)
    {
        return Physics2D.OverlapBox(position, new Vector2(1, 1), 1);
    }

    private int RandomGeneration(int min, int max)
    {
        return Random.Range(min, max);
    }

	private bool HeightLimitsReached()
	{
		return (currentYGeneration > Y_LOWER_LIMIT && currentYGeneration < Y_HIGHER_LIMIT);
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

    public void GameOver()
    {
        if (!gameOver)
        {
            this.gameOver = true;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
