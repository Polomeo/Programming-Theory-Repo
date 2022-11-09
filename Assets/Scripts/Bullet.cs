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
        if (other.gameObject.CompareTag("Enemy"))
        {
            // If a special damage effect was applied
            if(DamageType == "" || DamageType == null)
            {
                other.GetComponent<Enemy>().ApplyDamage(Damage);
            }
            else
            {
                other.GetComponent<Enemy>().ApplyDamage(Damage, DamageType);
            }
            Destroy(gameObject);
        }
    }
}

