using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [Header("Use Enemy Patrol Script?")]
    [SerializeField] bool useEnemyPatrol = false;

    [Header("Shoot Points, if useEnemyPatrol is true, use both")]
    [Header("If useEnemyPatrol is false, use only one")]
    [SerializeField] Transform shootPointLeft;
    [SerializeField] Transform shootPointRight;

    [Header("Shoot Settings")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float shootInterval = 3f;
    [SerializeField] bool shootRightDirection = false;
    [SerializeField] int bulletDamage = 1;

    [Header("VFX Settings")]
    [SerializeField] GameObject shootVFX;
    [SerializeField] bool isVFXFacingRight = false;

    private float shootTimer = 0f;

    private EnemyPatrol enemyPatrol;
    private Transform shootPointToUse;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if (useEnemyPatrol)
        {
            enemyPatrol = GetComponent<EnemyPatrol>();
        }
        else
        {
            if (shootPointLeft != null)
            {
                shootPointToUse = shootPointLeft;
            }
            else if (shootPointRight != null) 
            {
                shootPointToUse = shootPointRight; 
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;
    }

    public void PlayShootAnimation()
    {
        if (shootTimer >= shootInterval)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("shoot")) { return; }
            anim.SetTrigger("shoot");
        }
    }

    public void Shoot()
    {
        if (shootTimer >= shootInterval)
        {
            if (useEnemyPatrol)
            {
                if (enemyPatrol.isFacingRight)
                {
                    GameObject bullet = Instantiate(bulletPrefab, shootPointRight.position, Quaternion.identity);
                    bullet.GetComponent<Moveable>().direction = Direction.Right;
                    bullet.GetComponent<Projectile>().damage = bulletDamage;
                    
                    if (isVFXFacingRight)
                    {
                        GameObject vfx = Instantiate(shootVFX, shootPointRight.position, Quaternion.identity);
                        vfx.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else
                    {
                        GameObject vfx = Instantiate(shootVFX, shootPointRight.position, Quaternion.identity);
                        vfx.GetComponent<SpriteRenderer>().flipX = false;
                    }
                }
                else
                {
                    GameObject bullet = Instantiate(bulletPrefab, shootPointLeft.position, Quaternion.identity);
                    bullet.GetComponent<Moveable>().direction = Direction.Left;
                    bullet.GetComponent<Projectile>().damage = bulletDamage;

                    if (isVFXFacingRight)
                    {
                        GameObject vfx = Instantiate(shootVFX, shootPointLeft.position, Quaternion.identity);
                        vfx.GetComponent<SpriteRenderer>().flipX = false;
                    }
                    else
                    {
                        GameObject vfx = Instantiate(shootVFX, shootPointLeft.position, Quaternion.identity);
                        vfx.GetComponent<SpriteRenderer>().flipX = true;
                    }
                }
            }
            else
            {
                if (shootRightDirection)
                {
                    GameObject bullet = Instantiate(bulletPrefab, shootPointToUse.position, Quaternion.identity);
                    bullet.GetComponent<Moveable>().direction = Direction.Right;
                }
                else
                {
                    GameObject bullet = Instantiate(bulletPrefab, shootPointToUse.position, Quaternion.identity);
                    bullet.GetComponent<Moveable>().direction = Direction.Left;
                }
            }
            shootTimer = 0f;
        }

    }

}
