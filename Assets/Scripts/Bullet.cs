using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage { get; set; }
    public string DamageType { get; private set; }

    public void SetDamageType(string type)
    {
        DamageType = type;  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().ApplyDamage(Damage);
            Destroy(gameObject);
        }
    }
}
