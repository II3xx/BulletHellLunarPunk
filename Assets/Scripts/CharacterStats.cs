using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CharacterStats", menuName = "Player/CharacterStats", order = 1)]
public class CharacterStats : Stats
{
    [SerializeField] protected string CharacterName;
    [Range(0.25f, 3f)] [SerializeField] protected float dashCooldown;
    [Range(2f, 10f)] [SerializeField] protected float dashVelocity;
    [Range(0.1f, 1.5f)] [SerializeField] protected float dashTime;
    [SerializeField] private AudioClip dashSound;

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

    public CharacterStats CopyStats(CharacterStats copyFrom)
    {
        CharacterStats newStats = CreateInstance<CharacterStats>();
        newStats.Health = copyFrom.Health;
        newStats.dashSound = copyFrom.dashSound;
        newStats.Health = copyFrom.Health;
        newStats.movementSpeed = copyFrom.Health;
        newStats.allegiance = copyFrom.allegiance;
        newStats.IFrameOnHit = copyFrom.IFrameOnHit;
        newStats.currentIFrame = copyFrom.currentIFrame;
        newStats.IFrameBlinkTime = copyFrom.IFrameBlinkTime;
        newStats.CharacterName = copyFrom.CharacterName;
        newStats.dashCooldown = copyFrom.dashCooldown;
        newStats.dashVelocity = copyFrom.dashVelocity;
        newStats.dashTime = copyFrom.dashTime;
        return newStats;
    }
}
