using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    [SerializeField] private WeaponStats weaponStats;
    private float runTime;

    private void Awake()
    {
        runTime = weaponStats.fireRate;
    }

    public float RateOfFire
    {
        get => weaponStats.fireRate;
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
        for (int i = 0; i < weaponStats.BulletAmount; i++)
        {
            float angle = Mathf.Deg2Rad * (transform.localRotation.eulerAngles.z + Random.Range(0, weaponStats.BulletSpread) - weaponStats.BulletSpread * 0.5f - 90);
            Vector2 bulletVelocity = new(weaponStats.bulletSpeed * Mathf.Cos(angle), weaponStats.bulletSpeed * Mathf.Sin(angle));
            GameObject Bullet = Instantiate(weaponStats.bullet);
            Bullet.AddComponent(typeof(Bullet));
            Bullet.transform.position = transform.position;
            Bullet.transform.rotation = Quaternion.Euler(0,0,Mathf.Rad2Deg * angle - 90);
            Bullet.GetComponent<Bullet>().SetBulletStats(bulletVelocity, Faction.player);
        }
    }
}
