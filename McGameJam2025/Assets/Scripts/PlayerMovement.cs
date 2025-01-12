using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;

    private float move;
    public float speed;
    public float jump;

    private Rigidbody2D rb;

    public bool isFalling;
    public bool isJumping;
    private bool flipped = false;

    // Knockback variables stay exactly the same
    public float KBForce = 3f;
    public float KBCounter;
    public float KBTotalTime = 0.4f;
    public bool KnockFromRight;
    private bool hasInitialized = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        KBCounter = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        StartCoroutine(InitializeAfterDelay());
    }

    private IEnumerator InitializeAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        hasInitialized = true;
    }

    void Update()
    {
        // Get input
        move = Input.GetAxis("Horizontal");

        // Falling/Jumping state checks from second script
        if (rb.velocity.y < 0)
        {
            isFalling = true;
            isJumping = false;
        }
        else if (rb.velocity.y > 0)
        {
            isJumping = true;
            isFalling = false;
        }
        else
        {
            isJumping = false;
            isFalling = false;
        }

        // Knockback takes priority
        if (KBCounter > 0)
        {
            if (KnockFromRight)
            {
                rb.velocity = new Vector2(-KBForce * 1.5f, KBForce);
            }
            else
            {
                rb.velocity = new Vector2(KBForce * 1.5f, KBForce);
            }
            KBCounter -= Time.deltaTime;
        }
        else
        {
            // Normal movement when not in knockback
            rb.velocity = new Vector2(move * speed, rb.velocity.y);

            // Simplified jump from second script
            if (Input.GetButtonDown("Jump") && !isFalling && !isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, jump);
            }
        }

        // Animation states
        animator.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("isFalling", isFalling);
        animator.SetBool("isJumping", isJumping);

        // Character flipping
        if (rb.velocity.x < 0 && !flipped) FlipCharacter();
        if (rb.velocity.x > 0 && flipped) FlipCharacter();
    }

    // Keep all your collision methods exactly as they were
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Collision detected with tag: '{collision.gameObject.tag}'");
        Debug.Log($"GameObject name: {collision.gameObject.name}");
        Debug.Log($"HasInitialized: {hasInitialized}");

        if (!hasInitialized)
        {
            Debug.Log("Not initialized yet");
            rb.velocity = Vector2.zero;
            return;
        }

        string collisionTag = collision.gameObject.tag;
        Debug.Log($"Checking tag: '{collisionTag}'");
        Debug.Log($"Is Weak Point tag match: {collisionTag == "Weak Point"}");
        Debug.Log($"Is Enemy tag match: {collisionTag == "Enemy"}");

        if (collisionTag == "Weak Point" || collisionTag == "Enemy")
        {
            Debug.Log($"Hit {collisionTag}!");
            Debug.Log($"KBTotalTime value: {KBTotalTime}");
            KBCounter = KBTotalTime;
            Debug.Log($"KBCounter after setting: {KBCounter}");

            KBTotalTime = 0.4f;
            KBCounter = 0.4f;
            KBForce = 3f;

            KnockFromRight = collision.transform.position.x > transform.position.x;
            rb.velocity = Vector2.zero;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("OneWayPlatform"))
        {
            // Update jumping/falling states
            if (rb.velocity.y < 0)
            {
                isFalling = true;
            }
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