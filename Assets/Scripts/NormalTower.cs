using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE: NormalTower inhereiths methods and attributes from Tower.
public class NormalTower : Tower
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject shootSpot;

    private float timer = 0.0f;
    private bool canShoot = true;

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

        if (canShoot)
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
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();

        if (enemy != null)
        {
            canShoot = false;
            Debug.Log(gameObject.name + " can't shoot because is beeing attacked!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canShoot = true;
    }

    // POLYMORPHISM: Method overriding
    public override void Shoot()
    {
        // Instantiate the bullet and apply force forward
        GameObject bullet = Instantiate(bulletPrefab, shootSpot.transform.position, bulletPrefab.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(shootSpot.transform.right * FireSpeed);

        // Tell the bullet the information about the shoot
        bullet.GetComponent<Bullet>().Damage = Damage;

        // Calls the base function to log
        base.Shoot();
    }
    

}
