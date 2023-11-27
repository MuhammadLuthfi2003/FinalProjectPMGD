using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] bool useScriptableHealth = false;
    [SerializeField] ScriptableInteger healthScriptable;

    [SerializeField] bool useScriptableShield = false;
    [SerializeField] ScriptableInteger shieldScriptable;

    public int health = 3;
    public int maxHealth = 3;

    public int maxShield = 2;
    public int shield = 0;

    public bool isDead = false;

    [Header("Animation Settings")]
    public string dieTrigger = "die";

    [Header("VFX Settings")]
    public GameObject deathVFX;

    [Header("Event health reaches 0")]
    public UnityEvent OnLifeReachedZero;

    private Animator anim;

    private void Start()
    {
        if (useScriptableHealth)
        {
            healthScriptable.value = maxHealth;
            shieldScriptable.value = shield;
        }
        else
        {
            health = maxHealth;
        }

        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        if (useScriptableShield)
        {
            if (shieldScriptable.value > 0)
            {
                if (damage > shieldScriptable.value)
                {
                    damage -= shieldScriptable.value;
                    shieldScriptable.value = 0;
                }
                else
                {
                    shieldScriptable.value -= damage;
                    damage = 0;
                    return;
                }
            }
        }

        if (useScriptableHealth)
        {
            healthScriptable.value -= damage;

            if (healthScriptable.value <= 0)
            {
                OnLifeReachedZero.Invoke();
                isDead = true;
            }

            Instantiate(deathVFX, transform.position, Quaternion.identity);

            return;
        }

        health -= damage;

        if (health <= 0)
        {
            OnLifeReachedZero.Invoke();
            isDead = true;
        }
    }

    public void Heal(int heal)
    {
        if (useScriptableHealth)
        {
            if (healthScriptable.value + heal > maxHealth)
            {
                healthScriptable.value = maxHealth;
            }
            else
            {
                healthScriptable.value += heal;
            }
            return;
        }

        if (health + heal > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += heal;
        }
    }

    public void GiveShield(int shieldAmount)
    {
        if (useScriptableShield)
        {
            if (shieldAmount > maxShield)
            {
                shield = maxShield;
                shieldScriptable.value = maxShield;
            }
            else
            {
                shield += shieldAmount;
                shieldScriptable.value += shieldAmount;
            }
        }
    }

    public void PlayDeathAnim()
    {
        anim.SetTrigger(dieTrigger);
    }
}
