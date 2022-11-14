using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // Tower Main Class
    public int Health { get; protected set; }
    public int Damage { get; protected set; }
    public string TowerName { get; protected set; }
    public int TowerCost { get; protected set; }

    public float CooldownTime { get; protected set; }
    public bool isAlive { get; protected set; }
    

    protected float FireRate;
    protected float FireSpeed;
    protected float destroyWaitTime = 5f;

    public virtual void Shoot() 
    {
        return;
    }

    public virtual void TakeDamage(int damage) 
    {
        Health -= damage;

        Debug.Log(gameObject.name + " has recibed " + damage + " dmg!");

        if (Health >= 0)
        {
            Die();
        }
    }

    public virtual void Die() 
    {
        Debug.Log(gameObject.name + " has died!");
        isAlive = false;
        gameObject.SetActive(false);
        Destroy(gameObject, destroyWaitTime); // Deactivates, and after 5 secons destroys itself so other scripts can know isAlive = false
    }
}
