using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public float BulletSpeed
    {
        get => bulletSpeed;
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
}
