using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public float jumpForce;

    public bool grounded;
    public bool extremeColliding;
    public LayerMask ground;

    private Collider2D playerCollider;
    private Rigidbody2D playerBody;
    private Animator playerAnimator;
    private Vector2 movement;

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

        if(!extremeColliding)
            this.movement = new Vector2(moveSpeed, this.playerBody.velocity.y);
        else
            this.movement = new Vector2(0.0f, this.playerBody.velocity.y);

        //playerBody.velocity = new Vector2(moveSpeed, playerBody.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            playerBody.AddForce(Vector2.up * jumpForce);
        }

        this.UpdateAnimatorVariables();
	}

    void FixedUpdate()
    {
        this.playerBody.velocity = movement;
    }

    private void UpdateAnimatorVariables()
    {
        this.playerAnimator.SetFloat("Speed", this.playerBody.velocity.x);
        this.playerAnimator.SetBool("Grounded", this.grounded);
    }
}
