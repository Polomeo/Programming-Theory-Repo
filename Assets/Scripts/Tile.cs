using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject tower;
    public Transform spawnTowerPoint;
    private GameManager gameManager;


    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void OnMouseDown()
    {
        Debug.Log("Tile Clicked!");
        // Spawn the selected tower and clean the tower
        if (gameManager.TowerSelected != null)
        {
            tower = gameManager.TowerSelected;
            Instantiate(tower, spawnTowerPoint.position, tower.transform.rotation, spawnTowerPoint);
            gameManager.ClearTower();
        }

        // De-activate
    }
}
