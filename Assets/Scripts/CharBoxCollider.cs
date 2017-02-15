using UnityEngine;
using System.Collections;

public class CharBoxCollider : MonoBehaviour
{
    private PlayerController player;

    public void Start()
    {
        this.player = this.gameObject.GetComponentInParent<PlayerController>();
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        player.extremeColliding = true;
    }

    public void OnCollisionExit2D(Collision2D coll)
    {
        player.extremeColliding = false;
    }
}
