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

    [Header("Event health reaches 0")]
    public UnityEvent OnLifeReachedZero;

    public void TakeDamage(int damage)
    {
        if (useScriptableHealth)
        {
            healthScriptable.value -= damage;

            if (healthScriptable.value <= 0)
            {
                OnLifeReachedZero.Invoke();
            }

            return;
        }

        health -= damage;

        if (health <= 0)
        {
            OnLifeReachedZero.Invoke();
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
}
