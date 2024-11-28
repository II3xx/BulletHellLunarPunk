using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShoot : MonoBehaviour
{

    [SerializeField] [Range(0.1f, 8f)] protected float bulletSpeed = 4;
    [SerializeField] [Range(1, 10)] protected int BulletAmount;
    [SerializeField] [Range(0, 180)] protected float BulletSpread;
    [SerializeField] [Range(0, 8)] protected float currentRoF;
}
