using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    public float movementRange;
    private Vector3 pointLeft;
    private Vector3 pointRight;

    [Header("Movement")]
    [SerializeField] public float speed;

    private Vector3 currentPatrolPoint;
    [HideInInspector]
    public bool isFacingRight = true;
    //[HideInInspector]
    public bool isMoving = true;

    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private Health health;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        SetPatrolPoint(transform.position);

        if (GetComponent<Health>() != null)
        {
            health = GetComponent<Health>();
        }
    }

    public void SetPatrolPoint(Vector3 newPos)
    {
        currentPatrolPoint = newPos;
        pointLeft = new Vector3(transform.position.x - movementRange, transform.position.y, transform.position.z);
        pointRight = new Vector3(transform.position.x + movementRange, transform.position.y, transform.position.z);

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

        if (transform.position.x < currentPatrolPoint.x)
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
        if (health != null)
        {
            if (health.isDead) { return; }
        }

        if (isMoving)
        {
            anim.SetBool("isMoving", true);
            transform.position = Vector2.MoveTowards(transform.position, currentPatrolPoint, speed * Time.deltaTime);

            if (transform.position.x <= pointLeft.x) // if the enemy is at the left point
            {
                isFacingRight = true;
                currentPatrolPoint = pointRight;


            }
            else if (transform.position.x >= pointRight.x) // if enemy is at the right point
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

    public void ToggleIsMoving()
    {
        isMoving = !isMoving;
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
