using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // Tower Main Class
    public int Health { get; protected set; }
    public int Damage { get; protected set; }
    public float CooldownTime { get; protected set; }
    

    protected float FireRate;
    protected float FireSpeed;

    public virtual void Shoot() 
    {
        Debug.Log(gameObject.name + " shot a bullet");
    }

    public virtual void TakeDamage(int damage) 
    {
        Health -= damage;

        if (Health >= 0)
        {
            Die();
        }
    }

    public virtual void Die() 
    {
        Debug.Log(gameObject.name + " has reached 0 health!");
        Destroy(gameObject);
    }
}
