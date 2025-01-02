using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "EnemyRangedStats", menuName = "Enemies/EnemyRangedStats", order = 1)]
sealed class EnemyRangedStats : MovingEnemyStats
{
    [SerializeField] [Range(0, 8)] float minDistance = 0;
    [SerializeField] [Range(0, 8)] float maxDistance = 8;

    [Header("Bullet Related Stats")]
    [Range(0.1f, 8f)] [SerializeField] private float bulletSpeed = 4;
    [Range(1, 10)] [SerializeField] private int bulletAmount = 1;
    [Range(0, 180)] [SerializeField] private float bulletSpread = 15;
    [SerializeField] private GameObject bulletPrefab;

    [Header("Shooting Related Stats")]
    [Range(0.5f, 3f)] [SerializeField] private float maxRof = 2f;
    [Range(0, 2f)] [SerializeField] private float minRof = 1f;
    [SerializeField] AudioClip gunSound;
    [SerializeField] [Range(1, 25)] float minDistanceToShoot;
    [SerializeField] [Range(0, 100)] float predictionShot;

    

    public float BulletSpeed
    {
        get => bulletSpeed;
    }

    public float PredictionShot
    {
        get => predictionShot;
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

    public float MaxDistance
    {
        get => maxDistance;
    }

    public GameObject BulletPrefab
    {
        get => bulletPrefab;
    }

    public AudioClip GunSound
    {
        get => gunSound;
    }
}
