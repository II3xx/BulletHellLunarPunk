using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] [Range(0, 200)] private int Health;
    [SerializeField] [Range(0, 10)] private float movementSpeed;
    [SerializeField] private Faction allegiance;

    public int Damage
    {
        set => Health -= value;
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
