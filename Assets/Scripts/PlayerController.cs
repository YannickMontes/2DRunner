using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public float jumpForce;

    public bool grounded;
    public LayerMask ground;

    private Collider2D playerCollider;

    private Rigidbody2D playerBody;

    private Animator playerAnimator;

	// Use this for initialization
	void Start ()
    {
        this.playerBody = GetComponent<Rigidbody2D>();
        this.playerCollider = GetComponent<Collider2D>();
        this.playerAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        grounded = Physics2D.IsTouchingLayers(this.playerCollider, ground);

        playerBody.velocity = new Vector2(moveSpeed, playerBody.velocity.y);
        
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            playerBody.AddForce(Vector2.up * jumpForce);
        }

        this.UpdateAnimatorVariables();
	}

    private void UpdateAnimatorVariables()
    {
        this.playerAnimator.SetFloat("Speed", this.playerBody.velocity.x);
        this.playerAnimator.SetBool("Grounded", this.grounded);
    }
}
