using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Public Fields
    public float speed = 1;
    // Private Fields
    private Rigidbody2D rb;
    private float horizontalValue;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {

    }

    private void Move(float dir)
    {
        float xVal = dir * speed * Time.deltaTime;
        Vector2 targetVelocity = new Vector2(dir, rb.velocity.y);
        rb.velocity = targetVelocity;
    }
}
