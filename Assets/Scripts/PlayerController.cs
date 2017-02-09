using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public float jumpForce;

    public bool grounded;
    public LayerMask ground;

    private Collider2D playerCollider;

    private Rigidbody2D playerBody;

	// Use this for initialization
	void Start ()
    {
        this.playerBody = GetComponent<Rigidbody2D>();
        this.playerCollider = GetComponent<Collider2D>();

	}
	
	// Update is called once per frame
	void Update ()
    {
        grounded = Physics2D.IsTouchingLayers(this.playerCollider, ground);

        playerBody.velocity = new Vector2(moveSpeed, playerBody.velocity.y);
        
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, jumpForce);
        }
	}
}
