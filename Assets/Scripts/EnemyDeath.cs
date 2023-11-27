using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemDrop
{
    public GameObject item;
    [Range(0,100)] public int dropChance;
}

public class EnemyDeath : MonoBehaviour
{
    [Header("Score Settings")]
    [SerializeField] ScriptableInteger scoreScriptable;
    [SerializeField] int scoreValue = 100;

    [Header("Item Drop Settings")]
    [SerializeField] bool useItemDrops = false;
    [SerializeField] ItemDrop[] itemDrops;   

    [SerializeField] GameObject deathVFX;
    private Animator anim;
    private int totalDropChance;

    private void Start()
    {
        anim = GetComponent<Animator>();

        foreach (ItemDrop item in itemDrops)
        {
            totalDropChance += item.dropChance;
        }
        totalDropChance--;
    }

    public void OnEnemyDeath()
    {
        anim.SetTrigger("die");
        scoreScriptable.value += scoreValue;
        Instantiate(deathVFX, transform.position, Quaternion.identity);
        SpawnRandomItem();
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

    private void SpawnRandomItem()
    {
        if (useItemDrops)
        {
            int randomValue = Random.Range(0, totalDropChance);

            foreach (ItemDrop item in itemDrops)
            {
                if (randomValue <= item.dropChance)
                {
                    if (item.item != null)
                    {
                        Instantiate(item.item, transform.position, Quaternion.identity);
                    }
                    return;
                }
                else
                {
                    randomValue -= item.dropChance;
                }
            }
        }
    }
}
