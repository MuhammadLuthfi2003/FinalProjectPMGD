using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] bool useScriptableHealth = false;
    [SerializeField] ScriptableInteger healthScriptable;

    public int health = 3;
    public int maxHealth = 3;

    public bool isDead = false;

    [Header("Animation Settings")]
    public string dieTrigger = "die";

    [Header("Event health reaches 0")]
    public UnityEvent OnLifeReachedZero;

    private Animator anim;

    private void Start()
    {
        if (useScriptableHealth)
        {
            healthScriptable.value = maxHealth;
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
        if (useScriptableHealth)
        {
            healthScriptable.value -= damage;

            if (healthScriptable.value <= 0)
            {
                OnLifeReachedZero.Invoke();
                isDead = true;
            }

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

    public void PlayDeathAnim()
    {
        anim.SetTrigger(dieTrigger);
    }
}
