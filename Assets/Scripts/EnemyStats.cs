using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Enemies/EnemyStats", order = 1)]
public class EnemyStats : Stats
{
    [Range(0.1f, 8f)] [SerializeField] protected float bulletSpeed = 4;
    [Range(1, 10)] [SerializeField] protected int bulletAmount = 1;
    [Range(0, 180)] [SerializeField] protected float bulletSpread = 15;
    [Range(0.5f, 3f)] [SerializeField] protected float maxRof = 2f;
    [Range(0, 2f)] [SerializeField] protected float minRof = 1f;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] [Range(0, 8)] float minDistance = 0;
    [SerializeField] AudioClip gunSound;
    [SerializeField] [Range(1, 25)] float minDistanceToShoot;

    public float BulletSpeed
    {
        get => bulletSpeed;
    }

    public float MinDistanceToShoot
    {
        get => minDistanceToShoot;
    }

    public float BulletAmount
    {
        get => bulletAmount;
    }

    public float BulletSpread
    {
        get => bulletSpread;
    }

    public float MaxRof
    {
        get => maxRof;
    }

    public float MinRof
    {
        get => minRof;
    }

    public float MinDistance
    {
        get => minDistance;
    }

    public GameObject BulletPrefab
    {
        get => bulletPrefab;
    }

    public AudioClip GunSound
    {
        get => gunSound;
    }

    public EnemyStats CopyStats(EnemyStats copyFrom)
    {
        EnemyStats newStats = CreateInstance<EnemyStats>();
        newStats.health = copyFrom.health;
        newStats.gunSound = copyFrom.gunSound;
        newStats.bulletSpeed = copyFrom.bulletSpeed;
        newStats.bulletAmount = copyFrom.bulletAmount;
        newStats.bulletSpread = copyFrom.bulletSpread;
        newStats.maxRof = copyFrom.maxRof;
        newStats.minRof = copyFrom.minRof;
        newStats.bulletPrefab = copyFrom.bulletPrefab;
        newStats.minDistance = copyFrom.minDistance;
        newStats.movementSpeed = copyFrom.movementSpeed;
        newStats.allegiance = copyFrom.allegiance;
        newStats.IFrameOnHit = copyFrom.IFrameOnHit;
        newStats.currentIFrame = copyFrom.currentIFrame;
        newStats.IFrameBlinkTime = copyFrom.IFrameBlinkTime;
        newStats.onDamage = copyFrom.onDamage;
        newStats.minDistanceToShoot = copyFrom.minDistanceToShoot;
        newStats.onDeath = new UnityEvent();
        return newStats;
    }
}
