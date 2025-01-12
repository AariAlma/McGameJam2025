using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patrolDestination;

    public Transform playerTransform;
    public bool isChasing;
    public float chaseDistance;
    private float scaleValue;

    void Start()
    {
        // Check tag and set appropriate scale
        if (gameObject.CompareTag("Weak Point"))
        {
            scaleValue = 5f;
        }
        else if (gameObject.CompareTag("Enemy"))
        {
            scaleValue = 3f;
        }
    }
    void Update()
    {
        if(isChasing)
        {
            if(transform.position.x > playerTransform.position.x)
            {
                transform.localScale = new Vector3(scaleValue, scaleValue, 1);
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            }

            if (transform.position.x < playerTransform.position.x)
            {
                transform.localScale = new Vector3(-scaleValue, scaleValue, 1);
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }



        }

        else
        {
            if(Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
            {
                isChasing = true;
            }

            if (patrolDestination == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrolPoints[0].position) < 2f)
                {
                    transform.localScale = new Vector3(-scaleValue, scaleValue, 1);
                    patrolDestination = 1;
                }
            }

            else if (patrolDestination == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrolPoints[1].position) < 2f)
                {
                    transform.localScale = new Vector3(scaleValue, scaleValue, 1);
                    patrolDestination = 0;
                }
            }
        }






        
    }
}
