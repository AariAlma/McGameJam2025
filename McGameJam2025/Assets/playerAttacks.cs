using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    // Start is called before the first frame update


    public int damage;
    public Health health;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("WeakToPunch"))
        {
            health.TakeDamage(damage);
        }


    }
}