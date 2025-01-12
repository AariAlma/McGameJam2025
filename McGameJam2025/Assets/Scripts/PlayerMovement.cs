using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Animator done by Aari needed for animations
    // Try to seperate rigid body physics and player movement next
    // time please yp
    public Animator animator;


    private float move;
    public float speed;
    public float jump;


    private Rigidbody2D rb;

    public bool isJumping;
    private bool flipped = false;

    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;

    public bool KnockFromRight;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        animator.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        if (rb.velocity.x < 0 && !flipped) FlipCharacter();
        if (rb.velocity.x > 0 && flipped) FlipCharacter();
           
        if(KBCounter <= 0)
        {
            rb.velocity = new Vector2(move * speed, Mathf.Abs(rb.velocity.y));
        } 
        else
        {
            if(KnockFromRight ==true)
            {
                rb.velocity = new Vector2(-KBForce, KBForce);
            }
            if(KnockFromRight == false)
            {
                rb.velocity = new Vector2(KBForce, KBForce);
            }

            KBCounter-= Time.deltaTime;
        }


        move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.gameObject.CompareTag("Ground"))
         {
             isJumping = false;
         }
     }

     private void OnCollisionExit2D(Collision2D collision)
     {
         if (collision.gameObject.CompareTag("Ground"))
         {
             isJumping = true;
         }
     }

     private void FlipCharacter()
     {
        Vector3 factor = gameObject.transform.localScale;
        factor.x *= -1;
        gameObject.transform.localScale = factor;
        flipped = !flipped;
     }


}