using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage { get; set; }
    public string DamageType { get; private set; }

    [SerializeField] private float rightBound = 10f;

    public void SetDamageType(string type)
    {
        DamageType = type;  
    }

    private void FixedUpdate()
    {
        // ABSTRACTION
        DestroyOutOfBounds();
    }

    private void DestroyOutOfBounds()
    {
        if (gameObject.transform.position.x > rightBound)
        {
            Destroy(gameObject);
        }

    }
      


private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        
        if (enemy != null)
        {
            // If a special damage effect was applied
            if(DamageType == "" || DamageType == null)
            {
                enemy.ApplyDamage(Damage);
            }
            else
            {
                enemy.ApplyDamage(Damage, DamageType);
            }
            Destroy(gameObject);
        }
    }
}

