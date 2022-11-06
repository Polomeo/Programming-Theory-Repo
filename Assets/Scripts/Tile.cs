using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject tower;
    public Transform spawnTowerPoint;

    private void OnMouseDown()
    {
        // Spawn the selected tower
        Instantiate(tower, spawnTowerPoint.position, tower.transform.rotation);

        // De-activate
    }
}
