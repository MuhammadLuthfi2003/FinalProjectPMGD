using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    public Transform pointLeft;
    public Transform pointRight;

    [Header("Movement")]
    [SerializeField] public float speed;

    private Transform currentPatrolPoint;
    [HideInInspector]
    public bool isFacingRight = true;
    //[HideInInspector]
    public bool isMoving = true;

    private SpriteRenderer spriteRenderer;



    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        int randomNum = Random.Range(0, 1);

        if (randomNum == 0)
        {
            currentPatrolPoint = pointLeft;
        }
        else
        {
            currentPatrolPoint = pointRight;
        }

        if (transform.position.x < currentPatrolPoint.position.x)
        {
            isFacingRight = true;
        }
        else
        {
            isFacingRight = false;
            spriteRenderer.flipX = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, currentPatrolPoint.position, speed * Time.deltaTime);

            if (transform.position.x <= pointLeft.position.x) // if the enemy is at the left point
            {
                isFacingRight = true;
                currentPatrolPoint = pointRight;


            }
            else if (transform.position.x >= pointRight.position.x) // if enemy is at the right point
            {
                isFacingRight = false;
                currentPatrolPoint = pointLeft;


            }
        }

        if (isFacingRight)
        {
            spriteRenderer.flipX = false;

            if (currentPatrolPoint != pointRight)
            {
                currentPatrolPoint = pointRight;
            }
        }
        else if (!isFacingRight)
        {
            spriteRenderer.flipX = true;

            if (currentPatrolPoint != pointLeft)
            {
                currentPatrolPoint = pointLeft;
            }
        }


    }

    public void TogglecurrentPatrolPoint()
    {
        isMoving = true; 
        if (currentPatrolPoint == pointLeft)
        {
            currentPatrolPoint = pointRight;
        }
        else
        {
            currentPatrolPoint = pointLeft;
        }
    }

}
