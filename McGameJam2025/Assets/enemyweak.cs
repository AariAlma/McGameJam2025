using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyweak : MonoBehaviour
{
    public int health = 3;
    public bool isInvulnerable = false;
    public float invulnerableTime = 0.5f;  // Time enemy is invulnerable after being hit
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if it's the player's punch hitbox AND player is punching
        if (collision.gameObject.CompareTag("PunchHitbox"))
        {
            PlayerMovement player = collision.transform.parent.GetComponent<PlayerMovement>();
            if (player != null && player.isPunching && !isInvulnerable)
            {
                TakeDamage();
            }
        }
    }

    private void TakeDamage()
    {
        health--;
        Debug.Log($"Enemy hit! Health: {health}");

        if (health <= 0)
        {
            Die();
        }
        else
        {
            //StartCoroutine(FlashDamage());
        }
    }

    //private IEnumerator FlashDamage()
    //{
    //    isInvulnerable = true;

    //    // Flash red
    //    spriteRenderer.color = Color.red;
    //    yield return new WaitForSeconds(0.1f);
    //    spriteRenderer.color = Color.white;

    //    yield return new WaitForSeconds(invulnerableTime);
    //    isInvulnerable = false;
    //}

    private void Die()
    {
        Debug.Log("Enemy defeated!");
        Destroy(gameObject);
    }
}
