using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]

public class MovingEnemyRanged : MovingEnemy
{
    EnemyRangedStats rangedStats;

    private void Start()
    {
        rangedStats = (EnemyRangedStats)enemyStats;
        OnStart();
    }

    Vector2 NewTestPosition()
    {
        Vector3 Dest = player.GetComponent<Rigidbody2D>().position;
        float angle2 = Random.Range(0, 360) * Mathf.Deg2Rad;

        Vector2 maxPos = new(rangedStats.MaxDistance * Mathf.Cos(angle2), rangedStats.MaxDistance * Mathf.Sin(angle2));
        Vector2 minPos = new(rangedStats.MinDistance * Mathf.Cos(angle2), rangedStats.MinDistance * Mathf.Sin(angle2));

        Vector3 Destin = new(Dest.x + Random.Range(minPos.x, maxPos.x), Dest.y + Random.Range(minPos.y, maxPos.y), 0);

        return Destin;
    }

    override protected void UpdatePosition()
    {
        if(moveRunTime < rangedStats.TimeBetweenMoves)
        {
            moveRunTime += Time.deltaTime;
            return;
        }
        moveRunTime = 0;

        if(Random.Range(0,100) > rangedStats.MoveChance)
        {
            return;
        }

        Vector2 Destin = NewTestPosition();

        if (navAgent.isOnNavMesh)
            navAgent.SetDestination(Destin);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }
}
