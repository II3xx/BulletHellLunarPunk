using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class MovingEnemyStats : EnemyBaseStats
{
    [Header("Movement Related Stats")]
    [Tooltip("How long until it will retry to move")]
    [SerializeField] [Range(0.5f, 8)] float timeBetweenMoves;
    [Tooltip("The chance upon an attempt to move to change desired location")]
    [SerializeField] [Range(1, 100)] float moveChance;
    [SerializeField] [Range(0, 10)] protected float movementSpeed;

    public float MoveChance
    {
        get => moveChance;
    }

    public float TimeBetweenMoves
    {
        get => timeBetweenMoves;
    }

    public float Speed
    {
        get => movementSpeed;
    }
}
