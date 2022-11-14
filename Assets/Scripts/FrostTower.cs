using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostTower : NormalTower
{
    private void Start()
    {
        TowerName = "Frost Shooter";
        TowerCost = 150;
        Health = 10;
        Damage = 3;
        FireRate = 1.5f;
        FireSpeed = 300f;
        CooldownTime = 5f;
    }
    public override void Shoot()
    {
        // Instantiate the bullet and apply force forward
        GameObject bullet = Instantiate(bulletPrefab, shootSpot.transform.position, bulletPrefab.transform.rotation);
        
        // Tell the bullet the information about the shoot
        bullet.GetComponent<Bullet>().Damage = Damage;
        bullet.GetComponent<Bullet>().SetDamageType("Cold");

        // Shoots
        bullet.GetComponent<Rigidbody>().AddForce(shootSpot.transform.right * FireSpeed);

    }
}
