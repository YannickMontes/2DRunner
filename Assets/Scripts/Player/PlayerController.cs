﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float moveSpeedMax = 20;
    public float jumpForce;

    private float moveSpeed;
    private float speedCoeff = 7;

    public static bool grounded;
    public static bool sliding;
    public static bool extremeColliding; // To detect if the player colide with someting in front of him
    public LayerMask ground;

    //private Collider2D playerCollider;
    private Rigidbody2D playerBody;
    private Animator playerAnimator;
    private Vector2 movement;

	// Use this for initialization
	void Start ()
    {
        this.moveSpeed = this.moveSpeedMax;
        this.playerBody = GetComponent<Rigidbody2D>();
        //this.playerCollider = GetComponent<Collider2D>();
        this.playerAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        grounded = IsGrounded();

        if(!extremeColliding)
            this.movement = new Vector2(moveSpeed, this.playerBody.velocity.y);
        else
            this.movement = new Vector2(0.0f, this.playerBody.velocity.y);

        //playerBody.velocity = new Vector2(moveSpeed, playerBody.velocity.y);

        this.HandleInputs();

        this.UpdateAnimatorVariables();

        //Debug.Log(this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("AdventureGirlSlide"));
	}

    private bool IsGrounded()
    {
        return GameObject.FindGameObjectWithTag("PlayerBotCollider").GetComponent<BoxCollider2D>().IsTouchingLayers(ground);
    }

    private void HandleInputs()
    {
        this.InputJump();
        this.InputSpeed();
        this.InputSlide();
    }

    private void InputJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            playerBody.AddForce(Vector2.up * jumpForce);
        }
    }

    private void InputSlide()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("AdventureGirlSlide"))
        {
            PlayerController.sliding = true;
        }
        else
        {
            PlayerController.sliding = false;
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
        this.playerAnimator.SetBool("Grounded", PlayerController.grounded);
        this.playerAnimator.SetBool("Sliding", PlayerController.sliding);
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && !IsGrounded() && this.playerBody.velocity.x == 0)
        {
            this.playerBody.position = new Vector2(this.playerBody.position.x, this.playerBody.position.y - 1);
        }
    }
}