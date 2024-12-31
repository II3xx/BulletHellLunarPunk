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


    Vector2 NewTestPosition()
    {
        Vector3 Dest = player.GetComponent<Rigidbody2D>().position;
        float angle2 = Random.Range(0, 360) * Mathf.Deg2Rad;

        Vector2 maxPos = new(0.5f * Mathf.Cos(angle2), 0.5f * Mathf.Sin(angle2));
        Vector2 minPos = new(0.5f * Mathf.Cos(angle2), 0.5f * Mathf.Sin(angle2));

        Vector3 Destin = new(Dest.x + Random.Range(minPos.x, maxPos.x), Dest.y + Random.Range(minPos.y, maxPos.y), 0);

        return Destin;
    }

    private void EngagePosition()
    {
        navAgent.speed = stats.Speed;
    }

    private void DisengagePosition()
    {
        navAgent.speed = -stats.Speed * 0.2f;
    }

    override protected void UpdatePosition()
    {
        if (moveRunTime < stats.TimeBetweenMoves)
        {
            moveRunTime += Time.deltaTime;
            return;
        }
        moveRunTime = 0;

        if (Random.Range(0, 100) < stats.MoveChance)
        {
            return;
        }

        if (Random.Range(0, 100) > stats.EngagePercentage)
        {
            DisengagePosition();
        }
        else
        {
            EngagePosition();
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
