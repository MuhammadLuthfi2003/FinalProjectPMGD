using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    public float movementRange;
    private Vector3 pointLeft;
    private Vector3 pointRight;

    [Header("Sound Settings")]
    public float soundRange = 5;
    private int stepSoundIndex = 0;
    [SerializeField] private AudioClip stepSound;
    [SerializeField] private AudioClip stepSound2;
    [SerializeField] private AudioClip stepSound3;
    [SerializeField] private AudioClip stepSound4;
    bool canPlayStepSound = false;
    

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

    private Transform playerPos;


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

        playerPos = GameManager.Instance.player.transform;
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

        if (Vector2.Distance(transform.position, playerPos.position) <= soundRange)
        {
            canPlayStepSound = true;
        }
        else
        {
            canPlayStepSound = false;
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

    public void PlayStepSound()
    {
        if (canPlayStepSound)
        {
            if (stepSoundIndex % 4 == 0)
            {
                SFXPlayer.Instance.audioSource.PlayOneShot(stepSound);
            }
            else if (stepSoundIndex % 4 == 1)
            {
                SFXPlayer.Instance.audioSource.PlayOneShot(stepSound2);
            }
            else if (stepSoundIndex % 4 == 2)
            {
                SFXPlayer.Instance.audioSource.PlayOneShot(stepSound);
            }
            else if (stepSoundIndex % 4 == 3)
            {
                SFXPlayer.Instance.audioSource.PlayOneShot(stepSound);
            }
        }
    }

}
