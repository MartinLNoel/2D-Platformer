using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;
    

    bool isGrounded;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    private float runSpeed = 2f;

    [SerializeField]
    private float jumpSpeed = 5f;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();



    }

    private void Update()
    {
        //set the yVelocity in the animator
        animator.SetFloat("yVelocity", rb2d.velocity.y);

    }

    private void FixedUpdate()
    {

        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;
            animator.SetBool("Jump", !isGrounded);
        }
        else
        {
            isGrounded = false;
        }
       

        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb2d.velocity = new Vector2(runSpeed, rb2d.velocity.y);

            if(isGrounded)
                animator.Play("BunnyRight");

        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rb2d.velocity = new Vector2(-runSpeed, rb2d.velocity.y);

            if (isGrounded)
                animator.Play("BunnyLeft");

        }
        else
        {
            if (isGrounded)
                animator.Play("BunnyStanding");

            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }

        if(Input.GetKey("space") && isGrounded)
        {
            
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
            animator.SetBool("Jump", true);
            //animator.Play("Jumping");
        }

    }
}
