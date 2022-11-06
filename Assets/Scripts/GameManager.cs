using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject TowerSelected { get; private set; }

    public void SelectTower(GameObject tower)
    {
        TowerSelected = tower;
    }

    public void ClearTower()
    {
        TowerSelected = null;
    }
}
