using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDetect : MonoBehaviour
{

    [Header("Raycast Settings")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float distanceCollider = 1f;

    [Header("Assign Directly from Inspector")]
    public GameObject player;
    [SerializeField] Collider2D collider;

    [Header("Use Enemy Patrol Script, also assign directly from Inspector")]
    [SerializeField] bool useEnemyPatrol = false;
    public EnemyPatrol enemyPatrol;

    [Header("When not using Patrol Script use this, also assign directly from Inspector")]
    public bool castToRightSide = false;

    [HideInInspector]
    public bool isDetectingPlayer = false;

    [Header("Event when it detects the player")]
    public UnityEvent OnFoundPlayer;

    [Header("Event when it doesnt detect the player")]
    public UnityEvent OnPlayerNotFound;

    private PlayerHide playerHide;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.player;
        playerHide = player.GetComponent<PlayerHide>();
    }

    // Update is called once per frame
    void Update()
    {

        if (CheckPlayer())
        {
            if (isDetectingPlayer && !player.GetComponent<PlayerHide>().isHiding)
            {
                OnFoundPlayer.Invoke();

                if (useEnemyPatrol)
                {
                    enemyPatrol.isMoving = false;
                }
            }

        }
        else
        {
            if (useEnemyPatrol)
            {
                enemyPatrol.isMoving = true;
            }
            OnPlayerNotFound.Invoke();

        }

    }

    private bool CheckPlayer()
    {
        if (useEnemyPatrol)
        {
            if (!enemyPatrol.isFacingRight)
            {
                return CastRaycastLeft();
            }
            else
            {
                return CastRaycastRight();
            }
        }
        else
        {
            if (castToRightSide)
            {
                return CastRaycastRight();
            }
            else
            {
                return CastRaycastLeft();
            }
        }
    }

    private bool CastRaycastRight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(collider.bounds.center + (transform.right * attackRange * transform.localScale.x * distanceCollider),
new Vector3(collider.bounds.size.x * attackRange, collider.bounds.size.y, collider.bounds.size.z), 0, Vector2.right, 0, playerLayer);


        if (hit.collider != null)
        {

            if ((!playerHide.isHiding && !playerHide.isInvincible) && hit.collider.CompareTag("Player") && !player.GetComponent<Health>().isDead)
            {
                isDetectingPlayer = true;
            }
            else
            {
                isDetectingPlayer = false;
            }
        }

        return hit.collider != null;
    }

    private bool CastRaycastLeft()
    {
        RaycastHit2D hit = Physics2D.BoxCast(collider.bounds.center + (transform.right * attackRange * transform.localScale.x * distanceCollider * -1),
    new Vector3(collider.bounds.size.x * attackRange, collider.bounds.size.y, collider.bounds.size.z), 0, Vector2.left, 0, playerLayer);


        if (hit.collider != null)
        {
            if ((!playerHide.isHiding && !playerHide.isInvincible) && hit.collider.CompareTag("Player") && !player.GetComponent<Health>().isDead)
            {
                isDetectingPlayer = true;
            }
            else
            {
                isDetectingPlayer = false;
            }
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;


        if (useEnemyPatrol)
        {
            if (!enemyPatrol.isFacingRight)
            {
                Gizmos.DrawWireCube(collider.bounds.center + (transform.right * attackRange * transform.localScale.x * distanceCollider * -1),
            new Vector3(collider.bounds.size.x * attackRange, collider.bounds.size.y, collider.bounds.size.z));
            }
            else
            {
                Gizmos.DrawWireCube(collider.bounds.center + (transform.right * attackRange * transform.localScale.x * distanceCollider),
    new Vector3(collider.bounds.size.x * attackRange, collider.bounds.size.y, collider.bounds.size.z));
            }
        }
        else
        {
            if (castToRightSide)
            {
                Gizmos.DrawWireCube(collider.bounds.center + (transform.right * attackRange * transform.localScale.x * distanceCollider * -1),
            new Vector3(collider.bounds.size.x * attackRange, collider.bounds.size.y, collider.bounds.size.z));
            }
            else
            {
                Gizmos.DrawWireCube(collider.bounds.center + (transform.right * attackRange * transform.localScale.x * distanceCollider),
    new Vector3(collider.bounds.size.x * attackRange, collider.bounds.size.y, collider.bounds.size.z));
            }
        }

    }

}
