using UnityEngine;
using System.Collections;

public class CharBoxCollider : MonoBehaviour
{
    private PlayerController player;

    public void Start()
    {
        this.player = this.gameObject.GetComponentInParent<PlayerController>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Crate")
        {
            collision.gameObject.SetActive(false);
        }
    }
}
