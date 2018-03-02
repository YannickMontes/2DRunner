using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour {
	
	private Transform playerPosition;
    private BoardManager boardManager;

    // Use this for initialization
    void Awake ()
    {
        boardManager = FindObjectOfType<BoardManager>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}

	// Update is called once per frame
	void Update ()
	{
		Destroy();
	}

	private void Destroy()
	{
		if (this.transform.position.x + 60 < playerPosition.transform.position.x && !boardManager.IsGameOver())
		{
			this.gameObject.SetActive (false);
		}
	}
}
