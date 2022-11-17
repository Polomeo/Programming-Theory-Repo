using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PapirolaTower : Tower
{
    private void Start()
    {
        Health = 100;
        Damage = 0;
        TowerName = "Papirola";
        TowerCost = 150;
    }
}
