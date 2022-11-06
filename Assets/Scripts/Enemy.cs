using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // INCAPSULATION: Health is public to see, but only the class members can set it
    public int Health { get; protected set; }
    public int Damage { get; protected set; }

    protected float AttackRate;

    [SerializeField] private float moveSpeed;
    private float timer;
    private Tower targetTower;

    

    private void Start()
    {
        // Standard enemy parameters
        Health = 30;
        Damage = 5;
        AttackRate = 2f;

        timer = 0.0f;
    }

    private void Update()
    {
        if (!targetTower)
        {
            Walk();
        }
    }

    // When collides with a Tower
    private void OnTriggerEnter(Collider other)
    {
        // Get the Tower component of the colider
        targetTower = other.gameObject.GetComponent<Tower>();

        if(targetTower != null)
        {
            Debug.Log("Target Tower set to: " + targetTower.gameObject.name);

        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        // If exists, apply damage
        if (targetTower != null)
        {
            Attack(targetTower);
        }
    }

    // When stops colliding
    private void OnTriggerExit(Collider other)
    {
        if (targetTower != null)
        {
            targetTower = null;
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

    protected virtual void Attack (Tower tower)
    {
        // Starts the timer
        timer += Time.deltaTime;

        // If the FireRate time has passed
        if (timer > AttackRate)
        {
            // Attacks and resets the timer
            tower.TakeDamage(Damage);
            timer = 0.0f;

            Debug.Log(gameObject.name + " attacked " + targetTower.name + " for " + Damage + " dmg.");

        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    IEnumerator EffectCooldownRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);
    }

    
}
