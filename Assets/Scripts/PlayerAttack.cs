using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAttack : MonoBehaviour
{
    [Header("Assign First")]
    [SerializeField] private PlayerController playerController;


    [Header("Attack Settings")]
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] KeyCode attackButton;
    [SerializeField] KeyCode attackButton2;
    [SerializeField] private float attackInterval = 1f;

    [Header("Indicator")]
    [SerializeField] private GameObject indicator;

    [Header("Raycast Settings")]
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float distanceCollider = 1f;
    [SerializeField] private Collider2D collider;

    private GameObject enemyInRange;
    private float timeSinceLastAttack = 0f;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;



        if (CheckEnemy() && !GameManager.Instance.player.GetComponent<PlayerHide>().isHiding)
        {
            indicator.SetActive(true);
            if (Input.GetKeyDown(attackButton) || Input.GetKeyDown(attackButton2))
            {
                if (enemyInRange != null && timeSinceLastAttack > attackInterval)
                {
                    PlayAttackAnim();
                    timeSinceLastAttack = 0;
                }
            }
        }
        else
        {
            indicator.SetActive(false);
        }

    }


    public void AttackEnemy()
    {
        if (enemyInRange != null)
        {
            if (enemyInRange.TryGetComponent<Health>(out Health enemyHealth))
            {
                enemyHealth.TakeDamage(attackDamage);
            }
        }
    }

    private void PlayAttackAnim()
    {
        anim.SetTrigger("attack");
    }

    private bool CheckEnemy()
    {
        if (playerController.isFacingRight)
        {
            return CastRaycastRight();
        }
        else
        {
            return CastRaycastLeft();
        }
    }

    private bool CastRaycastRight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(collider.bounds.center + (transform.right * attackRange * transform.localScale.x * distanceCollider),
new Vector3(collider.bounds.size.x * attackRange, collider.bounds.size.y, collider.bounds.size.z), 0, Vector2.right, 0, enemyLayer);


        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                enemyInRange = hit.collider.gameObject;
                return true;
            }
            else
            {
                enemyInRange = null;
                return false;
            }
        }
        
        enemyInRange = null;
        return false;
    }

    private bool CastRaycastLeft()
    {
        RaycastHit2D hit = Physics2D.BoxCast(collider.bounds.center + (transform.right * attackRange * transform.localScale.x * distanceCollider * -1),
    new Vector3(collider.bounds.size.x * attackRange, collider.bounds.size.y, collider.bounds.size.z), 0, Vector2.left, 0, enemyLayer);


        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                enemyInRange = hit.collider.gameObject;
                return true;
            }
            else
            {
                enemyInRange = null;
                return false;
            }
        }

        enemyInRange = null;
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        if (playerController.isFacingRight)
        {
            Gizmos.DrawWireCube(collider.bounds.center + (transform.right * attackRange * transform.localScale.x * distanceCollider),
new Vector3(collider.bounds.size.x * attackRange, collider.bounds.size.y, collider.bounds.size.z));
        }
        else
        {
            Gizmos.DrawWireCube(collider.bounds.center + (transform.right * attackRange * transform.localScale.x * distanceCollider * -1),
new Vector3(collider.bounds.size.x * attackRange, collider.bounds.size.y, collider.bounds.size.z));
        }
    }

    public void PlaySwingSFX()
    {
        SFXPlayer.Instance.PlaySwingBambooSFX();
    }

    public void PlaySplatterSFX()
    {
        SFXPlayer.Instance.PlayHitSFX();
    }
}
