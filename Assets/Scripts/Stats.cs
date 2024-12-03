using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stats : ScriptableObject
{
    [SerializeField] [Range(0, 200)] protected int Health;
    [SerializeField] [Range(0, 10)] protected float movementSpeed;
    [SerializeField] protected Faction allegiance;
    [HideInInspector] public UnityEvent onDeath;

    public int Damage
    {
        set
        {
            Health -= value;
            if (Health <= 0)
            {
                onDeath.Invoke();
            }
        }
    }

    public int Healing
    {
        set => Health += value;
    }

    public float Speed
    {
        get => movementSpeed;
    }

    public Faction Allegiance
    {
        get => allegiance;
    }

}
