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
    public float punchCooldown;



    private Rigidbody2D rb;
    public bool isPunching;
    public bool isJumping;
    public bool isFalling;
    private bool flipped = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        punchCooldown = 0.7f;
    }

    void Update()
    {
        // Falling Tag
        if (rb.velocity.y < 0) 
        {
            isFalling = true;
        }
        else 
        {
            isFalling = false;
        }
        if (rb.velocity.y > 0)
        {
            isJumping = true;

        }
        else
        {
            isJumping = false;
        }
        animator.SetBool("isFalling", isFalling);
        animator.SetBool("isJumping", isJumping);

        // Animation
        animator.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        if (Input.GetButtonDown("Jump") && rb.velocity.y == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            FindObjectOfType<AudioManager>().Play("jumpSFX");
        }
        if (Input.GetKeyDown(KeyCode.J)) StartCoroutine(Punch());
        if (rb.velocity.x < 0 && !flipped) FlipCharacter();
        if (rb.velocity.x > 0 && flipped) FlipCharacter();
           
        move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);
        if (rb.velocity.x > 1.0f)
            FindObjectOfType<AudioManager>().Play("walkSFX");
    }
   
    private void FlipCharacter()
    {
        Vector3 factor = gameObject.transform.localScale;
        factor.x *= -1;
        gameObject.transform.localScale = factor;
        flipped = !flipped;
    }

    private IEnumerator Punch()
    {
        isPunching = true;
        if (isJumping || isFalling)
        {
            animator.SetBool("airPunch", true);
            FindObjectOfType<AudioManager>().Play("airPunchSFX");
        }
        else
        {
            animator.SetBool("groundPunch", true);
            FindObjectOfType<AudioManager>().Play("punchSFX");
        }
        yield return new WaitForSeconds(punchCooldown);
        animator.SetBool("airPunch", false);
        animator.SetBool("groundPunch", false);
        isPunching =  false;
    }


}