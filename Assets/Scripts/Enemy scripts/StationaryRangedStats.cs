using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StationaryRangedStats", menuName = "Enemies/StationaryRangedStats", order = 1)]
public class StationaryRangedStats : EnemyBaseStats
{
    [Range(0.1f, 8f)] [SerializeField] private float bulletSpeed = 4;
    [Range(1, 10)] [SerializeField] private int bulletAmount = 1;
    [Range(0, 180)] [SerializeField] private float bulletSpread = 15;
    [Range(0.5f, 3f)] [SerializeField] private float maxRof = 2f;
    [Range(0, 2f)] [SerializeField] private float minRof = 1f;
    [SerializeField] private GameObject bulletPrefab;
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

    public GameObject BulletPrefab
    {
        get => bulletPrefab;
    }

    public AudioClip GunSound
    {
        get => gunSound;
    }
}
