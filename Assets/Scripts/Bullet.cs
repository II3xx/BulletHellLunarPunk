using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Faction
{
    player,
    enemy,
    trap
}

[RequireComponent(typeof(Rigidbody2D))]

public class Bullet : MonoBehaviour
{
    private Faction allegiance;
    [SerializeField] private int damage;
    private Rigidbody2D rb2D;
    private bool ready = false;
    private float lifeTime = 15f;
    private float runTime = 0f;

    public void SetBulletStats(Vector2 velocity, Faction allegiance)
    {
        this.allegiance = allegiance;
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        rb2D.velocity = velocity;
        ready = true;
    }

    private void Update()
    {
        runTime += Time.deltaTime;
        if(lifeTime <= runTime)
        {
            Destroy(gameObject);
        }
    }

    private bool IsWall(Collider2D collision)
    {
        if (collision is not BoxCollider2D && collision is not CircleCollider2D && collision is not CapsuleCollider2D)
            return true;
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!ready)
            return;
        EnemyMovement enemyMovement = collision.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            if (enemyMovement.Allegiance != allegiance)
            {
                enemyMovement.Damage = damage;
                Destroy(gameObject);
                return;
            }
        }
        PlayerScript playerScript = collision.GetComponent<PlayerScript>();
        if(playerScript != null)
        {
            if (playerScript.Allegiance != allegiance)
            {
                playerScript.Damage = damage;
                Destroy(gameObject);
                return;
            }
        }
        if(IsWall(collision))
        {
            Destroy(gameObject);
        }
    }
}
