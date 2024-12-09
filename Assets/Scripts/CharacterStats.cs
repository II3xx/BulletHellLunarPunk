using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "CharacterStats", menuName = "Player/CharacterStats", order = 1)]
public class CharacterStats : Stats
{
    [SerializeField] protected string CharacterName;
    [Range(0.25f, 3f)] [SerializeField] protected float dashCooldown;
    [Range(2f, 10f)] [SerializeField] protected float dashVelocity;
    [Range(0.1f, 1.5f)] [SerializeField] protected float dashTime;
    [SerializeField] private AudioClip dashSound;
    [SerializeField] AnimationCurve dashCurve;
    [SerializeField] private Color onDash = new Color(1, 1f, 1f, 0.5f);

    public float DashTime
    {
        get => dashTime;
    }

    public Color OnDash
    {
        get => onDash;
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

    public AnimationCurve DashCurve
    {
        get => dashCurve;
    }

    public CharacterStats CopyStats(CharacterStats copyFrom)
    {
        CharacterStats newStats = CreateInstance<CharacterStats>();
        newStats.health = copyFrom.health;
        newStats.dashSound = copyFrom.dashSound;
        newStats.movementSpeed = copyFrom.movementSpeed;
        newStats.allegiance = copyFrom.allegiance;
        newStats.IFrameOnHit = copyFrom.IFrameOnHit;
        newStats.currentIFrame = copyFrom.currentIFrame;
        newStats.IFrameBlinkTime = copyFrom.IFrameBlinkTime;
        newStats.CharacterName = copyFrom.CharacterName;
        newStats.dashCooldown = copyFrom.dashCooldown;
        newStats.dashVelocity = copyFrom.dashVelocity;
        newStats.dashTime = copyFrom.dashTime;
        newStats.onDeath = new UnityEvent();
        newStats.dashCurve = copyFrom.dashCurve;
        newStats.onDash = copyFrom.onDash;
        newStats.onDamage = copyFrom.onDamage;
        return newStats;
    }
}
