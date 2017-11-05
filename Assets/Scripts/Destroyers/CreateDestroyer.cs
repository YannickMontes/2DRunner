using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDestroyer : MonoBehaviour {

	private Transform playerPosition;

	public void Start()
	{
		playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}

    public void OnTriggerEnter2D(Collider2D collider)
    {
        Unable(collider);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Unable(collision.collider);
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        Unable(collision.collider);
    }

	private void FixedUpdate()
	{
		if (this.transform.position.x + 40 < playerPosition.transform.position.x) 
		{
			this.gameObject.SetActive (false);
		}
	}

    private void Unable(Collider2D collider)
    {
        if (collider.tag.Contains("Player") && PlayerController.sliding)
        {
            this.gameObject.SetActive(false);
        }
    }
}
