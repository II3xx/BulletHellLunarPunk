using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

abstract public class MovingEnemy : BaseEnemy
{
    protected NavMeshAgent navAgent;
    protected float moveRunTime = 0;

    override protected void OnStart()
    {
        base.OnStart();
        navAgent = gameObject.GetComponent<NavMeshAgent>();
        navAgent.speed = ((MovingEnemyStats)enemyStats).Speed;
        navAgent.updateRotation = false;
        navAgent.updateUpAxis = false;
    }

    IEnumerator OnKnockback(float maxTimer, Vector2 Velocity)
    {
        float normalAccel = navAgent.acceleration;
        navAgent.acceleration = 0;
        navAgent.velocity = Velocity;
        float knockBackResistance = 1 - ((MovingEnemyStats)enemyStats).KnockBackResistance * 0.01f;
        float timer = maxTimer * knockBackResistance;
        for (float i = 0; timer > i; i += Time.deltaTime)
        {
            navAgent.velocity = Vector2.Lerp(Velocity, new(0, 0), 0.1f);
            yield return null;
        }
        navAgent.acceleration = normalAccel;
    }

    public void Knockback(float knockbackAngle, float knockBackAmount, float knockBackTime)
    {
        Vector2 velocity = new(knockBackAmount * Mathf.Cos(knockbackAngle), knockBackAmount * Mathf.Sin(knockbackAngle));
        if(health > 0)
            StartCoroutine(OnKnockback(knockBackTime, velocity));
    }

    virtual protected void UpdatePosition() { }
}
