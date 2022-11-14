using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunflowerTower : Tower
{
    private float timer = 0.0f;
    private float maxSunSpawnTimer = 3f;
    private float minSunSpawnTimer = 6f;
    [SerializeField] private GameObject sunPrefab;
    [SerializeField] private Transform shootPoint;

    private void Start()
    {
        Health = 10;
        FireSpeed = 2f;
        FireRate = Random.Range(minSunSpawnTimer, maxSunSpawnTimer);
    }

    private void FixedUpdate()
    {
        // This plant emits little Suns that function as player currency to buy more plants
        if (GameManager.Instance.IsGameActive)
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

    public override void Shoot()
    {
        // Emits the sun 
        GameObject sun = Instantiate(sunPrefab, shootPoint.position, sunPrefab.transform.rotation);
        sun.GetComponent<Rigidbody>().AddForce(Vector3.up * FireSpeed, ForceMode.Impulse);

        // Sets a new timer
        FireRate = Random.Range(minSunSpawnTimer, maxSunSpawnTimer);

        Debug.Log("Sun emmited!");
        Debug.Log("Next sun in: " + FireRate + " secs.");
    }

}
