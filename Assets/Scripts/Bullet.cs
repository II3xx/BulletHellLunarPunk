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
    [SerializeField] AudioClip audioClipWallHit, audioClipUnitHit;
    [SerializeField] private int damage;
    [SerializeField] [Range(0.2f,10)] private float knockBackForce;
    [SerializeField] [Range(0.2f, 2)] private float knockBackTime;
    private Rigidbody2D rb2D;
    private bool ready = false;
    private readonly float lifeTime = 15f;
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

    private float AngleMath(Vector2 Dest)
    {
        return Mathf.Atan2(Dest.y - rb2D.position.y, Dest.x - rb2D.position.x);
    }

    private bool IsWall(Collider2D collision)
    {
        if (collision is not BoxCollider2D && collision is not CircleCollider2D && collision is not CapsuleCollider2D)
            return true;
        return false;
    }

    private void CheckEnemyMovement(Collider2D collision)
    {
        EnemyMovement enemyMovement = collision.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            UnitAudioHit();
            enemyMovement.Damage = damage;
            Debug.Log(AngleMath(collision.transform.position));
            float angle = AngleMath(collision.transform.position);
            enemyMovement.Knockback(angle, knockBackForce, knockBackTime);
            Destroy(gameObject);
            return;
        }
    }

    private void CheckSpawner(Collider2D collision)
    {
        EnemySpawner enemySpawner = collision.GetComponent<EnemySpawner>();
        if (enemySpawner != null)
        {
            UnitAudioHit();
            enemySpawner.Damage = damage;
            Destroy(gameObject);
            return;
        }
    }

    private void checkAllEnemies(Collider2D collision)
    {
        CheckEnemyMovement(collision);
        CheckSpawner(collision);
    }

    private void CheckPlayer(Collider2D collision)
    {
        PlayerScript playerScript = collision.GetComponent<PlayerScript>();
        if (playerScript != null)
        {
            UnitAudioHit();
            playerScript.Damage = damage;
            Destroy(gameObject);
            return;
        }
    }

    private void CreateTempAudio(AudioClip audioClip)
    {
        var temp = GameObject.CreatePrimitive(PrimitiveType.Plane);
        temp.transform.position = rb2D.position;
        var tempaudio = temp.AddComponent<TemporaryAudioSource>();
        tempaudio.setClipAndPlay(audioClip);
        temp.GetComponent<MeshRenderer>().enabled = false;
    }

    private void UnitAudioHit()
    {
        if (audioClipUnitHit != null)
        {
            CreateTempAudio(audioClipUnitHit);
        }
    }

    private void WallAudioHit()
    {
        if (audioClipWallHit != null)
        {
            CreateTempAudio(audioClipWallHit);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!ready)
            return;

        if(allegiance == Faction.player)
        {
            checkAllEnemies(collision);
        }

        else if(allegiance == Faction.enemy)
        {
            CheckPlayer(collision);
        }
        
        if(IsWall(collision))
        {
            WallAudioHit();
            Destroy(gameObject);
        }
    }
}
