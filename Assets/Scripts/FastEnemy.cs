using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        // Fast enemy parameters
        Health = 10;
        Damage = 2;
        AttackRate = 1.5f;

        enemyGoal = GameObject.FindWithTag("Goal");
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
    }
}
