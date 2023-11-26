using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OnEnemyDeath()
    {
        anim.SetTrigger("die");
        Instantiate(deathVFX, transform.position, Quaternion.identity);
    }

    public void DestroyEnemy()
    {
        StartCoroutine(DelayDestroy());
    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
