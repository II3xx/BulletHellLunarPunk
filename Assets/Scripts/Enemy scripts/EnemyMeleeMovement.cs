using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class EnemyMeleeMovement : MovingEnemy
{
    MovingMeleeStats stats;

    private void Start()
    {
        stats = (MovingMeleeStats)enemyStats;
        OnStart();
    }


    Vector2 NewTestPosition(float minDistance)
    {
        Vector3 Dest = player.GetComponent<Rigidbody2D>().position;
        float angle2 = Random.Range(0, 360) * Mathf.Deg2Rad;

        Vector2 maxPos = new(minDistance * Mathf.Cos(angle2), minDistance * Mathf.Sin(angle2));

        Vector3 Destin = new(Dest.x + maxPos.x, Dest.y + maxPos.y, 0);

        return Destin;
    }

    Vector2 EngagePosition()
    {
        return NewTestPosition(0.5f);
    }

    Vector2 DisengagePosition()
    {
        return NewTestPosition(4f);
    }

    override protected void UpdatePosition()
    {
        Vector2 Destin = new();
        if (moveRunTime < stats.TimeBetweenMoves)
        {
            moveRunTime += Time.deltaTime;
            return;
        }
        moveRunTime = 0;

        if (Random.Range(0, 100) > stats.MoveChance)
        {
            return;
        }

        if (Random.Range(0, 100) > stats.EngagePercentage)
        {
            Destin = DisengagePosition();
        }
        else
        {
            Destin = EngagePosition();
        }

        if (navAgent.isOnNavMesh)
            navAgent.SetDestination(Destin);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }
}
