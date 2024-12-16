using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent( typeof(Rigidbody2D))]

public class EnemyBulletShoot : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private EnemyStats enemyStats;
    private Rigidbody2D rb2D;
    private float currentRoF;
    private float runTime;
    [SerializeField] private AudioSource audioSource;
    

    void RandomizeRoF()
    {
        currentRoF = Random.Range(enemyStats.MinRof, enemyStats.MaxRof);
    }

    float AngleMath(Vector2 Dest)
    {
        return Mathf.Atan2(Dest.y - rb2D.position.y, Dest.x - rb2D.position.x) + Mathf.Deg2Rad * (Random.Range(0, enemyStats.BulletSpread) - enemyStats.BulletSpread * 0.5f);
    }

    void Awake()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        RandomizeRoF();
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
        angle = AngleMath(playRB2d.position + playRB2d.velocity * (playRB2d.position - rb2D.position) / bulletVelocity);
        bulletVelocity = new(enemyStats.BulletSpeed * Mathf.Cos(angle), enemyStats.BulletSpeed * Mathf.Sin(angle));
        GameObject Bullet = Instantiate(enemyStats.BulletPrefab);
        Bullet.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle - 90));
        Bullet.GetComponent<Bullet>().SetBulletStats(bulletVelocity, Faction.enemy);
    }

    // Update is called once per frame
    void Update()
    {
        runTime += Time.deltaTime;
        if (Vector2.Distance(player.transform.position, rb2D.transform.position) > enemyStats.MinDistanceToShoot)
            return;
        if (runTime >= currentRoF)
        {
            RandomizeRoF();
            runTime = 0;
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
        }
    }
}
