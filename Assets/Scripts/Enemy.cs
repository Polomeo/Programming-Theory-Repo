using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Health { get; protected set; }

    protected float MoveSpeed;

    private void Start()
    {
        Health = 10;
    }

    // Overload in case the bullet has special damage
    public virtual void ApplyDamage(int damage, string type)
    {
        // Substract from current health
        Health -= damage;

        ApplyDamageType(type);
        
    }
    public virtual void ApplyDamage(int damage)
        {
            Health -= damage;
            Debug.Log("Hit by " + damage + " dmg! Health left: " + Health);
        }

    protected virtual void ApplyDamageType(string type)
    {
        bool isFrozzen = false;

        if (type == "Cold" && !isFrozzen)
        {
            // Frozzen! move slower
            isFrozzen = true;
            MoveSpeed /= 2;

            // Set the material blue

            // Wait 5 seconds and return to normal
            StartCoroutine(EffectCooldownRoutine(5f));
            MoveSpeed *= 2;
            isFrozzen = false;
        }

    }

    IEnumerator EffectCooldownRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);
    }

    
}
