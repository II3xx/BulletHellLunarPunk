using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAwakeShoot : EnemyBulletShoot
{

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < enemyStats.BulletAmount; i++)
        {
            if (Random.Range(0, 100) < enemyStats.PredictionShot)
                OnPredictionShot();
            else
                OnPointShot();
        }
    }

    private void Awake()
    {
        
    }
}
