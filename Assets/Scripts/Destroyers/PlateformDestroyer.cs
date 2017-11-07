using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformDestroyer : MonoBehaviour {

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
        if (this.transform.position.x + 60 < playerPosition.transform.position.x)
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                this.transform.GetChild(i).gameObject.SetActive(false);
            }
            this.transform.DetachChildren();
			this.gameObject.SetActive (false);
        }
    }
}
