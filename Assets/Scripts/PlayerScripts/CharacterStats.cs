using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "CharacterStats", menuName = "Player/CharacterStats", order = 1)]
public class CharacterStats : ScriptableObject
{
    private readonly Faction allegiance = Faction.player;
    [SerializeField] [Range(0, 200)] protected int health;
    [SerializeField] [Range(0, 2)] protected float iFrameOnHit;
    [SerializeField] protected string CharacterName;
    [Range(0.25f, 3f)] [SerializeField] protected float dashCooldown;
    [Range(2f, 10f)] [SerializeField] protected float dashVelocity;
    [Range(0.1f, 1.5f)] [SerializeField] protected float dashTime;
    [SerializeField] private AudioClip dashSound;
    [SerializeField] [Range(0, 10)] protected float movementSpeed;
    [SerializeField] protected Color onDamage = new(1, 0.1f, 0.1f, 0.5f);
    protected float iFrameBlinkTime = 0.125f;

    public int MaxHealth
    {
        get => health;
    }

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

    public float DashTime
    {
        get => dashTime;
    }

    public AudioClip DashSound
    {
        get => dashSound;
    }

    public string Name
    {
        get => CharacterName;
    }

    public float DashVelocity
    {
        get => dashVelocity;
    }

    public float DashCooldown
    {
        get => dashCooldown;
    }

    public Faction Allegiance
    {
        get => allegiance;
    }

    public float Speed
    {
        get => movementSpeed;
    }
}
