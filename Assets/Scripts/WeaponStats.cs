using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats", menuName = "Player/WeaponStats", order = 1)]
public class WeaponStats : ScriptableObject
{
    [Range(0.1f, 8f)] public float bulletSpeed = 4;
    [Range(1, 10)] public int BulletAmount = 1;
    [Range(0, 180)] [Tooltip("The amount of conal spread in degrees")] public float BulletSpread = 5f;
    [Range(0.25f, 8)] public float fireRate = 1f;
    public GameObject bullet;
}
