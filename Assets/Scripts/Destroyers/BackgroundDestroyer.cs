using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundDestroyer : MonoBehaviour {
	
	private Transform playerPosition;

	// Use this for initialization
	void Awake ()
	{
		playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}

	// Update is called once per frame
	void Update ()
	{
		Destroy();
	}

	private void Destroy()
	{
		if (this.transform.position.x + 40 < playerPosition.transform.position.x)
		{
			this.gameObject.SetActive (false);
		}
	}
}
