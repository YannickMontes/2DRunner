using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyMovement : MonoBehaviour {

    public LayerMask ground;

    private int framesLeft = 0;
    private Direction actualDirection;
    private float speed = 5.0f;
    private bool grounded;
    private Vector2 movement;
    private Animator ennemyAnimator;

	// Use this for initialization
	void Start ()
    {
        this.framesLeft = 0;
        actualDirection = this.RandomMovement();
        this.ennemyAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.grounded = Physics2D.IsTouchingLayers(GetComponent<BoxCollider2D>(), ground);
        this.HandleDirection();
        this.UpdateAnimatorVariables();
        this.framesLeft--;
    }

    private void HandleDirection()
    {
        if (this.framesLeft == 0)
        {
            this.InverseDirection();
            this.SetNumberFrameForThisAction(180, 180);
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2((int)this.actualDirection * this.speed, 0.0f);
    }

    private Direction RandomMovement()
    {
        int rand = Random.Range(0, 2);
        if(rand == 0)
        {
            return Direction.LEFT;
        }
        else
        {
            return Direction.RIGHT;
        }
    }

    private void SetNumberFrameForThisAction(int min, int max)
    {
        this.framesLeft = Random.Range(min, max);
    }

    private void InverseDirection()
    {
        if (this.actualDirection == Direction.RIGHT)
        {
            this.actualDirection = Direction.LEFT;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
            this.actualDirection = Direction.RIGHT;
        }
    }

    private void UpdateAnimatorVariables()
    {
        this.ennemyAnimator.SetFloat("Speed", speed);
        this.ennemyAnimator.SetBool("Grounded", grounded);
        this.ennemyAnimator.SetBool("Alive", true);
    }
}

enum Direction
{
    LEFT = -1, 
    RIGHT = 1
}