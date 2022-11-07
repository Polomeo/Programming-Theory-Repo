using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Transform spawnTowerPoint;

    private GameManager gameManager;
    private GameObject tower;
    [SerializeField] private Transform tileParent;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void OnMouseDown()
    {
        Debug.Log("Tile Clicked!");
        // Spawn the selected tower and clean the tower
        if (gameManager.currentSelectedTower != null)
        {
            tower = gameManager.currentSelectedTower;

            Instantiate(tower, spawnTowerPoint.position, tower.transform.rotation, tileParent);

            gameManager.ClearTower();
        }

        // De-activate
    }
}
