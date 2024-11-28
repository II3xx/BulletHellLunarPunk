using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : BulletShoot
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Rigidbody2D rb2D;
    private float runTime;

    private void Awake()
    {
        runTime = currentRoF;
    }

    public float RateOfFire
    {
        get => currentRoF;
    }

    private void Update()
    {
        runTime += Time.deltaTime;
    }

    public void OnShoot()
    {
        if (runTime < RateOfFire)
        {
            return;
        }
        runTime = 0;
        for (int i = 0; i < BulletAmount; i++)
        {
            float angle = Mathf.Deg2Rad * (transform.localRotation.eulerAngles.z + Random.Range(0, BulletSpread) - BulletSpread * 0.5f - 90);
            Vector2 bulletVelocity = new(bulletSpeed * Mathf.Cos(angle), bulletSpeed * Mathf.Sin(angle));
            GameObject Bullet = GameObject.Instantiate(bullet);
            Bullet.AddComponent(typeof(Bullet));
            Bullet.transform.position = transform.position;
            Bullet.transform.rotation = Quaternion.Euler(0,0,Mathf.Rad2Deg * angle - 90);
            Bullet.GetComponent<Bullet>().SetBulletStats(bulletVelocity, Faction.player);
        }
    }
}
