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
    private bool isWalking;
    private float timer;

    

    private void Start()
    {
        // Standard enemy parameters
        Health = 25;
        Damage = 5;
        AttackRate = 2f;

        isWalking = true;
        timer = 0.0f;
    }

    private void Update()
    {
        if (isWalking)
        {
            Walk();
        }
    }

    // When collides with a Tower
    private void OnTriggerEnter(Collider other)
    {
        // Get the Tower component of the colider
        Tower tower = other.gameObject.GetComponent<Tower>();
        
        // If exists, apply damage
        if (tower != null)
        {
            Attack(tower);
        }
    }

    // When stops colliding
    private void OnTriggerExit(Collider other)
    {
        // If the Tower destroys, keeps walking
        isWalking = true;
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
        // Stops walking
        isWalking = false;

        // Starts the timer
        timer += Time.deltaTime;

        // If the FireRate time has passed
        if (timer > AttackRate)
        {
            // Shoots and resets the timer
            tower.TakeDamage(Damage);
            timer = 0.0f;
        }
        // Attacks towers
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
