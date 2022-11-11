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

    [SerializeField] private GameObject enemyGoal;
    [SerializeField] private bool isFrozzen;

    [SerializeField] private float moveSpeed;
    private float timer;
    private Tower targetTower;

    [SerializeField] private bool isWalking;
    [SerializeField] private Rigidbody rb;
    private Renderer rend;

    

    private void Start()
    {
        // Standard enemy parameters
        Health = 30;
        Damage = 5;
        AttackRate = 2f;
        enemyGoal = GameObject.FindWithTag("Goal");

        isFrozzen = false;

        timer = 0.0f;
        isWalking = true;

        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        // ABSTRACTION
        if (targetTower != null)
        {
            StopWalking();
            Attack(targetTower);
        }
        else //if (isWalking)
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

        }
        
    }

    // When stops colliding
    private void OnTriggerExit(Collider other)
    {
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

        if (type == "Cold" && !isFrozzen)
        {
            Debug.Log("Frozzen!");
            isFrozzen = true;

            // Store the color temporarely
            Color baseColor = rend.material.color;

            // Frozzen! move slower
            moveSpeed /= 2;

            // Set the color to blue
            // rend.material.SetColor("_Color", new Color(0, 0, 0.8f, 0.5f));

            // Wait 5 seconds and return to normal
            StartCoroutine(EffectCooldownRoutine(2f));
            moveSpeed *= 2;
            isFrozzen = false;

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
