using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float moveSpeedMax = 20;
    public float jumpForce;

    private float moveSpeed;
    private float speedCoeff = 7;

    public bool grounded;
    public bool extremeColliding; // To detect if the player colide with someting in front of him
    public LayerMask ground;

    private Collider2D playerCollider;
    private Rigidbody2D playerBody;
    private Animator playerAnimator;
    private Vector2 movement;

	// Use this for initialization
	void Start ()
    {
        this.moveSpeed = this.moveSpeedMax;
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

        this.HandleInputs();

        this.UpdateAnimatorVariables();
	}

    private void HandleInputs()
    {
        this.InputJump();
        this.InputSpeed();
    }

    private void InputJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            playerBody.AddForce(Vector2.up * jumpForce);
        }
    }

    private void InputSpeed()
    {
        if(Input.anyKey)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                this.moveSpeed = this.moveSpeedMax - speedCoeff;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                this.moveSpeed = this.moveSpeedMax + speedCoeff;
            }
        }
        else
        {
            this.moveSpeed = this.moveSpeedMax;
        }
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
