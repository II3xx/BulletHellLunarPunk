using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyBulletShoot : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private EnemyRangedStats enemyStats;
    private float currentRoF;
    [SerializeField] private AudioSource audioSource;
    

    void RandomizeRoF()
    {
        currentRoF = Random.Range(enemyStats.MinRof, enemyStats.MaxRof);
    }

    float AngleMath(Vector2 Dest)
    {
        return Mathf.Atan2(Dest.y - transform.position.y, Dest.x - transform.position.x) + Mathf.Deg2Rad * (Random.Range(0, enemyStats.BulletSpread) - enemyStats.BulletSpread * 0.5f);
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Shooter());
    }

    void OnPointShot()
    {
        float angle = AngleMath(player.GetComponent<Rigidbody2D>().position);
        Vector2 bulletVelocity = new(enemyStats.BulletSpeed * Mathf.Cos(angle), enemyStats.BulletSpeed * Mathf.Sin(angle));
        GameObject Bullet = Instantiate(enemyStats.BulletPrefab);
        Bullet.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle - 90));
        Bullet.GetComponent<Bullet>().SetBulletStats(bulletVelocity, Faction.enemy);
    }

    void OnPredictionShot()
    {
        Rigidbody2D playRB2d = player.GetComponent<Rigidbody2D>();
        float angle = AngleMath(playRB2d.position);
        Vector2 bulletVelocity = new(enemyStats.BulletSpeed * Mathf.Cos(angle), enemyStats.BulletSpeed * Mathf.Sin(angle));
        angle = AngleMath(playRB2d.position + playRB2d.velocity * (playRB2d.position - (Vector2)transform.position) / bulletVelocity);
        bulletVelocity = new(enemyStats.BulletSpeed * Mathf.Cos(angle), enemyStats.BulletSpeed * Mathf.Sin(angle));
        GameObject Bullet = Instantiate(enemyStats.BulletPrefab);
        Bullet.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle - 90));
        Bullet.GetComponent<Bullet>().SetBulletStats(bulletVelocity, Faction.enemy);
    }

    IEnumerator Shooter()
    {
        RandomizeRoF();
        for(float runtime = 0;runtime <= currentRoF; runtime+=Time.deltaTime)
        {
            yield return null;
        }
        while (Vector2.Distance(player.transform.position, transform.transform.position) > enemyStats.MinDistanceToShoot)
        {
            yield return null;
        }
        audioSource.clip = enemyStats.GunSound;
        audioSource.pitch = Random.Range(0.95f, 1.05f);
        audioSource.Play();
        for (int i = 0; i < enemyStats.BulletAmount; i++)
        {
            if (Random.Range(0, 100) < enemyStats.PredictionShot)
                OnPredictionShot();
            else
                OnPointShot();
        }
        StartCoroutine(Shooter());
        yield break;
    }
}
