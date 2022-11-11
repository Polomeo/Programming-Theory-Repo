using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject currentSelectedTower { get; private set; }
    public bool IsGameActive; 

    [SerializeField] private float spawnTimer;
    private float timer = 0f;
    [SerializeField] List<GameObject> spawnPoints;
    [SerializeField] List<GameObject> enemies;

    private void Awake()
    {
        // SINGLETON

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    { 
        IsGameActive = true;
        InvokeRepeating("SpawnRandomEnemy", spawnTimer, spawnTimer);
    }
    private void Update()
    {

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

    // ---- GAME LOOP ----

    public void StartGame()
    {
        IsGameActive = true;
        Debug.Log("Game Started!");
    }
    public void GameOver()
    {
        IsGameActive = false;
        Debug.Log("Game Over!");
    }

    // ---- SPAWN SECTION ----
    private void SpawnRandomEnemy()
    {
        if (IsGameActive)
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

}
