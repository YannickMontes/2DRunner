using UnityEngine;
using System.Collections;

public class CharBoxCollider : MonoBehaviour
{

    public void Start()
    {
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Crate")
        {
            collision.gameObject.SetActive(false);
        }
    }
}
