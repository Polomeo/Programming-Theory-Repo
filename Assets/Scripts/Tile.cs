using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject tower;
    private void OnMouseDown()
    {
        // Spawn the selected tower
        Instantiate(tower, transform.position, tower.transform.rotation);

        // De-activate
    }
}
