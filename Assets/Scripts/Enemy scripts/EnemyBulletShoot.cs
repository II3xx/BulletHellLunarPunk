using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyBulletShoot : MonoBehaviour
{
    protected GameObject player;
    [SerializeField] protected EnemyRangedStats enemyStats;
    private float currentRoF;
    [SerializeField] protected AudioSource audioSource;
    

    void RandomizeRoF()
    {
        currentRoF = Random.Range(enemyStats.MinRof, enemyStats.MaxRof);
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Shooter());
    }

    protected void OnPointShot()
    {
        float angle = LunarMath.VectorAngle(player.transform.position, transform.position);
        Vector2 bulletVelocity = new(enemyStats.BulletSpeed * Mathf.Cos(angle), enemyStats.BulletSpeed * Mathf.Sin(angle));
        GameObject Bullet = Instantiate(enemyStats.BulletPrefab);
        Bullet.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle - 90));
        Bullet.GetComponent<Bullet>().SetBulletStats(bulletVelocity, Faction.enemy);
    }

    protected void OnPredictionShot()
    {
        Rigidbody2D playRB2d = player.GetComponent<Rigidbody2D>();
        float angle = LunarMath.VectorAngle(player.transform.position, transform.position);
        Vector2 bulletVelocity = new(enemyStats.BulletSpeed * Mathf.Cos(angle), enemyStats.BulletSpeed * Mathf.Sin(angle));
        angle = LunarMath.VectorAngle(playRB2d.position + playRB2d.velocity * (playRB2d.position - (Vector2)transform.position) / bulletVelocity, transform.position);
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
