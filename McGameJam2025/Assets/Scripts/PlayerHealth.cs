using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;
    private bool check1 = true;
    private bool check2 = true;
    private bool check3 = true;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 70 && check1)
        {
            check1 = false;
            FindObjectOfType<AudioManager>().Play("myBackSFX");
        }
        if (health < 50 && check2)
        {
            check2 = false;
            FindObjectOfType<AudioManager>().Play("dogSFX");
        }
        if (health < 30 && check3)
        {
            check3 = false;
            FindObjectOfType<AudioManager>().Play("bzzSFX");
        }
        if (health <= 0)
        {
            FindObjectOfType<AudioManager>().Play("myBackSFX");
            Destroy(gameObject);
        }
    }
}
