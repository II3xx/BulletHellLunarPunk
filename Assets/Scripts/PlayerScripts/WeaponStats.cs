using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats", menuName = "Player/WeaponStats", order = 1)]
public class WeaponStats : ScriptableObject
{
    [Range(0.1f, 8f)] [SerializeField] private float bulletSpeed = 4;
    [Range(1, 10)] [SerializeField] private int bulletAmount = 1;
    [Range(0, 180)] [Tooltip("The amount of conal spread in degrees")] [SerializeField] private float bulletSpread = 5f;
    [Range(0.25f, 8)] [SerializeField] private float fireRate = 1f;
    [SerializeField] private GameObject bullet;
    [SerializeField] private AudioClip gunSound;
    [SerializeField] [Range(0, 360)] private float gunPointAngle;

    public float BulletSpeed
    {
        get => bulletSpeed;
    }

    public float WeaponAngle
    {
        get => gunPointAngle;
    }

    public float BulletAmount
    {
        get => bulletAmount;
    }

    public float BulletSpread
    {
        get => bulletSpread;
    }

    public float FireRate
    {
        get => fireRate;
    }

    public GameObject BulletPrefab
    {
        get => bullet;
    }

    public AudioClip GunSound
    {
        get => gunSound;
    }
}
