using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // INCAPSULATION: Health is public to see, but only the class members can set it
    public int Health { get; protected set; }

    [SerializeField] private float moveSpeed;
    [SerializeField] private bool isAlive = true;

    private void Start()
    {
        Health = 40;
    }

    private void Update()
    {
        while (isAlive)
        {
            Walk();
        }
    }

    // POLYMORPHISM: Method overloading
    // Overload in case the bullet has special damage
    public virtual void ApplyDamage(int damage, string type)
    {
        // Substract from current health
        Health -= damage;
        
        ApplyDamageType(type);
        
        Debug.Log("Hit by " + damage +  type + " dmg! Health left: " + Health);

        // ABSTRACTION: Die() is a separate function
        if (Health <= 0)
        {
            Die();
        }
        
        
    }

    public virtual void ApplyDamage(int damage)
        {
            Health -= damage;
            Debug.Log("Hit by " + damage + " dmg! Health left: " + Health);

            // ABSTRACTION: Die() is a separate function
            if (Health <= 0)
            {
                Die();
            }
    }

    protected virtual void ApplyDamageType(string type)
    {
        bool isFrozzen = false;

        if (type == "Cold" && !isFrozzen)
        {
            // Frozzen! move slower
            isFrozzen = true;
            moveSpeed /= 2;

            // Set the material blue

            // Wait 5 seconds and return to normal
            StartCoroutine(EffectCooldownRoutine(5f));
            moveSpeed *= 2;
            isFrozzen = false;
        }

    }

    protected virtual void Walk()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    protected virtual void Die()
    {
        isAlive = false;
        Destroy(gameObject);
    }

    IEnumerator EffectCooldownRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);
    }

    
}
