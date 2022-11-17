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

    [SerializeField] protected GameObject enemyGoal;
    [SerializeField] protected bool isWalking;

    [SerializeField] protected float moveSpeed;
    protected float timer;
    protected Tower targetTower;

    [SerializeField] protected Rigidbody rb;
    protected Renderer rend;

    

    private void Start()
    {
        // Standard enemy parameters
        Health = 30;
        Damage = 5;
        AttackRate = 2f;

        // Private variables
        enemyGoal = GameObject.FindWithTag("Goal");
        isWalking = true;

        timer = 0.0f;

        // Components
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        // ABSTRACTION
        if (targetTower != null)
        {
            if (isWalking)
            {
                StopWalking();
            }
            Attack(targetTower);
        }
        else
        {
            Walk();
        }
    }

    // When collides with a Tower
    private void OnTriggerEnter(Collider other)
    {
        // Case: collide with tower
        // Get the Tower component of the colider
        targetTower = other.gameObject.GetComponent<Tower>();

        if(targetTower != null)
        {
            Debug.Log("Target Tower set to: " + targetTower.gameObject.name);   
        }

        // Case: collide with goal
        if (other.CompareTag("Goal"))
        {
            // Game Over
            GameManager.Instance.GameOver();

            // Destroys the enemy
            // (Since the collider triggers are in the mesh, we destroy the parent of the gameobject wich is the enemy gameobject itself)
            Destroy(transform.gameObject);
        }

        // Case: collide with other enemy
        if (other.CompareTag("Enemy"))
        {
            StopWalking();
        }

    }

    // When stops colliding
    private void OnTriggerExit(Collider other)
    {
        // If target tower is null, continue walking
        Tower isTower = other.GetComponent<Tower>();

        if(isTower != null)
        {
            if (targetTower != null)
            {
                targetTower = null;
                isWalking = true;
            }
        }

    }

    // POLYMORPHISM: Method overloading
    // Overload in case the bullet has special damage
    public virtual void ApplyDamage(int damage, string type)
    {
        // Substract from current health
        Health -= damage;
        
        ApplyDamageType(type);
        
        Debug.Log(gameObject.name + " hit by " + damage +  type + " dmg! Health left: " + Health);

        // ABSTRACTION: Die() is a separate function
        if (Health <= 0)
        {
            Die();
        }
        
        
    }

    public virtual void ApplyDamage(int damage)
        {
            Health -= damage;
            Debug.Log(gameObject.name + " hit by " + damage + " dmg! Health left: " + Health);

            // ABSTRACTION: Die() is a separate function
            if (Health <= 0)
            {
                Die();
            }
    }

    protected virtual void ApplyDamageType(string type)
    {

        if (type == "Cold")
        {
            Debug.Log("Frozzen!");
        
            // Store the color temporarely
            Color baseColor = rend.material.color;

            // Frozzen! move slower
            moveSpeed /= 2;

            // Set the color to blue
            // rend.material.SetColor("_Color", new Color(0, 0, 0.8f, 0.5f));

            // Wait 5 seconds and return to normal
            StartCoroutine(EffectCooldownRoutine(2f));
            moveSpeed *= 2;

            // rend.material.SetColor("_Color",baseColor);
        }

    }

    protected virtual void Walk()
    {
        // rb.AddRelativeForce(Vector3.forward * moveSpeed);
        rb.AddForce((enemyGoal.transform.position - transform.position).normalized * moveSpeed);
    }

    protected virtual void StopWalking()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        isWalking = false;
    }

    protected virtual void Attack (Tower tower)
    {
        // Starts the timer
        timer += Time.deltaTime;

        // Keeps track of the tower Health
        int towerHealthLeft = tower.Health - Damage; // counts 1 hit 

        // If the FireRate time has passed
        if (timer > AttackRate)
        {
            // Attacks and resets the timer
            towerHealthLeft -= Damage;
            tower.TakeDamage(Damage);
            timer = 0.0f;

            Debug.Log(gameObject.name + " attacked " + targetTower.name + ". Tower Health left: " + towerHealthLeft);

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
