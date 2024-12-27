using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class EnemyBaseStats : ScriptableObject
{
    [SerializeField] [Range(0, 200)] protected int health;
    [SerializeField] [Range(0, 2)] protected float iFrameOnHit;
    protected float iFrameBlinkTime = 0.125f;
    [SerializeField] protected Color onDamage = new(1, 0.1f, 0.1f, 0.5f);

    public float IFrameOnHit
    {
        get => iFrameOnHit;
    }

    public Color OnDamage
    {
        get => onDamage;
    }
    public float IFrameBlinkTime
    {
        get => iFrameBlinkTime;
    }

    public int MaxHealth
    {
        get => health;
    }
}
