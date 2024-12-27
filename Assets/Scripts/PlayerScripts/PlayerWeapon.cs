using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{

    [SerializeField] private WeaponStats weaponStats;
    [SerializeField] private AudioSource audioSource;
    private Coroutine currentCoroutine;
    private bool notActived = true;
    private float runTime;

    private void Awake()
    {
        runTime = weaponStats.FireRate;
    }

    public float RateOfFire
    {
        get => weaponStats.FireRate;
    }

    private void Update()
    {
        runTime += Time.deltaTime;
        if(!notActived)
        {
            shootSystem();
        }
    }

    private void shootSystem()
    {
        if (runTime < RateOfFire)
        {
            return;
        }
        runTime = 0;
        audioSource.clip = weaponStats.GunSound;
        audioSource.pitch = Random.Range(0.95f, 1.05f);
        audioSource.Play();

        for (int i = 0; i < weaponStats.BulletAmount; i++)
        {
            float angle = Mathf.Deg2Rad * (transform.localRotation.eulerAngles.z + Random.Range(0, weaponStats.BulletSpread) - weaponStats.BulletSpread * 0.5f - 90);
            Vector2 bulletVelocity = new(weaponStats.BulletSpeed * Mathf.Cos(angle), weaponStats.BulletSpeed * Mathf.Sin(angle));
            GameObject Bullet = Instantiate(weaponStats.BulletPrefab);
            Bullet.transform.position = transform.position;
            Bullet.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle - 90);
            Bullet.GetComponent<Bullet>().SetBulletStats(bulletVelocity, Faction.player);
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            notActived = !notActived;
        }
        else if (context.canceled)
        {
            notActived = !notActived;
        }
        
    }
}
