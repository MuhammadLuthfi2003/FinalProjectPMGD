using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    [Header("Projectile Properties")]
    [SerializeField] string tagToHit;
    [SerializeField] int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tagToHit))
        {
            if (tagToHit == "Player" && (collision.GetComponent<PlayerHide>().isHiding || collision.GetComponent<PlayerHide>().isInvincible)) return;
            collision.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
