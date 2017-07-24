using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDestroyer : MonoBehaviour {

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

    private void Unable(Collider2D collider)
    {
        if (collider.tag.Contains("Player") && PlayerController.sliding)
        {
            this.gameObject.SetActive(false);
        }
    }
}
