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

    public float DashTime
    {
        get => dashTime;
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

}
