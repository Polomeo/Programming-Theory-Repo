using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTower : Tower
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject shootSpot;

    float timer = 0.0f;

    // When is instantiated, set the variables
    void Start()
    {
        Health = 10;
        Damage = 5;
        FireRate = 1.5f;
        FireSpeed = 300f;
        CooldownTime = 5f;
    }

    // FixedUpdate is used to calculate more accurately the velocity of the bullet RigidBody
    void FixedUpdate()
    {
        // Starts the timer
        timer += Time.fixedDeltaTime;
        
        // If the FireRate time has passed
        if (timer > FireRate)
        {
            // Shoots and resets the timer
            Shoot();
            timer = 0.0f;
        }
    }

    public override void Shoot()
    {
        // Instantiate the bullet and apply force forward
        GameObject bullet = Instantiate(bulletPrefab, shootSpot.transform.position, bulletPrefab.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(shootSpot.transform.forward * FireSpeed);

        // Tell the bullet the information about the shoot
        bullet.GetComponent<Bullet>().Damage = Damage;

        // Calls the base function to log
        base.Shoot();
    }
    

}
