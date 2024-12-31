using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovingMeleeStats", menuName = "Enemies/MovingMeleeStats", order = 1)]
public class MovingMeleeStats : MovingEnemyStats
{
    [Header("Melee Related Stats")]
    [SerializeField] [Range(0f, 180f)] protected float swingArc = 0;
    [Tooltip("Where to start the swing. Swing will go from where it starts + swingArc. In front of enemy = 90")]
    [SerializeField] [Range(0f, 180f)] protected float swingStart = 90;
    [Tooltip("How fast it'll swing from start to finish.")]
    [SerializeField] [Range(1f, 30f)] protected float swingSpeed;
    [SerializeField] [Range(0.25f, 2f)] protected float swingCooldown;
    [Tooltip("Whether to retreat or advance position to enemy")]
    [SerializeField] [Range(25, 100)] protected float engagePercentage;

    public float SwingArc
    {
        get => swingArc;
    }
    public float SwingStart
    {
        get => swingStart;
    }
    public float SwingSpeed
    {
        get => swingSpeed;
    }
    public float SwingCooldown
    {
        get => swingCooldown;
    }
    public float EngagePercentage
    {
        get => engagePercentage;
    }
}
