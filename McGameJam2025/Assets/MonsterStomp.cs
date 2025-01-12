using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStomp : MonoBehaviour
{
    private PlayerMovement playerMovement;  // Reference to PlayerMovement script

    void Start()
    {
        // Get the PlayerMovement component from the parent object
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Only destroy if player is falling and hits a weak point
        if (playerMovement.isFalling && collision.gameObject.CompareTag("Weak Point"))
        {
            Destroy(collision.gameObject);
        }
    }
}