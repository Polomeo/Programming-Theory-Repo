using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject currentSelectedTower { get; private set; }

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
}
