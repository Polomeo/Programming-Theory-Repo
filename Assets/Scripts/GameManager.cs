using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject currentSelectedTower { get; private set; }

    [SerializeField] private float spawnTimer;
    private float timer = 0f;
    [SerializeField] List<GameObject> spawnPoints;
    [SerializeField] List<GameObject> enemies;

    private void Start()
    {
        InvokeRepeating("SpawnRandomEnemy", spawnTimer, spawnTimer);
    }
    private void Update()
    {
        // SpawnRandomEnemy();
    }

    public void SelectTower(GameObject tower)
    {
        currentSelectedTower = tower;
        Debug.Log(currentSelectedTower.name + " has been selected");
    }

    public void ClearTower()
    {
        currentSelectedTower = null;
        Debug.Log("Tower Selected = NULL");
    }

    // ---- SPAWN SECTION ----
    private void SpawnRandomEnemy()
    {
        timer += Time.time;

        if(timer > spawnTimer)
        {
            Vector3 randomSpawnPos = spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position;
            GameObject randomEnemy = enemies[Random.Range(0, enemies.Count)];

            Instantiate(randomEnemy, randomSpawnPos, randomEnemy.transform.rotation);

            timer = 0f;
        }
        
    }

}
