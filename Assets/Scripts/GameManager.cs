using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject currentSelectedTower { get; private set; }
    public bool IsGameActive { get; private set; }

    [SerializeField] private GameUIHandler gameUIHandler;

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
        // DontDestroyOnLoad(gameObject);

        // Sets the game active first so other classes can know
        IsGameActive = true;
    }

    private void Start()
    {
        GetSpawnPoints();
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
        if (IsGameActive)
        {
            IsGameActive = false;

            gameUIHandler.ShowGameOverUI();

            Debug.Log("Game Over!");

        }
    }

    // ---- SPAWN SECTION ----

    private void GetSpawnPoints()
    {
        spawnPoints = new List<GameObject>();

        GameObject[] spawnPointsInScene = GameObject.FindGameObjectsWithTag("SpawnPoint");

        foreach(GameObject sp in spawnPointsInScene)
        {
            spawnPoints.Add(sp);
        }
    }
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
