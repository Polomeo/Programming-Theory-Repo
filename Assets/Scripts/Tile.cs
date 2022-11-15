using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Transform spawnTowerPoint;

    private GameManager gameManager;
    private GameObject tower;
    private Tower placedTower;
    [SerializeField] private Transform tileParent;
    private bool isEmpty;
    private bool isShowing;


    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        isShowing = false;
        isEmpty = true;
        HideTile();
    }

    private void Update()
    {
        // If there is a selected tower and is not empty, show the tile
        if (gameManager.currentSelectedTower && isEmpty && !isShowing)
        {
            ShowTile();
            isShowing = true;
        }

        // If there is not a tower selected and is showing, hide it
        if(gameManager.currentSelectedTower == null && isShowing)
        {
            HideTile();
            isShowing = false;
        }

    }

    private void OnMouseDown()
    {
        // Spawn the selected tower and clean the tower
        if (gameManager.currentSelectedTower != null && isEmpty)
        {
            // Assign the tower selected from Game Manager
            tower = gameManager.currentSelectedTower;

            // Sets the local variable to placedTower to the instantiated tower
            GameObject tempTower = Instantiate(tower, spawnTowerPoint.position, tower.transform.rotation, tileParent);

            // Substract the cost
            gameManager.SubstractCurrency(tempTower.GetComponent<Tower>().GetTowerCost());
            

            // Clears the tower in the Game Manager
            gameManager.ClearTower();
            
            // Is empty is no longer false, so we hide the tile
            isEmpty = false;
            HideTile();
        }
        else if (!isEmpty)
        {
            Debug.Log("The tile is not empty!");
        }

    }

    private void HideTile()
    {
        if (isShowing)
        {
            gameObject.GetComponent<Renderer>().enabled = false;
        }

    }

    private void ShowTile()
    {
        if (!isShowing)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
        }
    }
}
