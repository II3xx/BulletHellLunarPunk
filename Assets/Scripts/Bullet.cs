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
    [Tooltip("The force amount that they're hit back by per bullet")]
    [SerializeField] [Range(0.2f,10)] private float knockBackForce;
    [Tooltip("The time that enemies hit are 'stunned' and being knocked back")]
    [SerializeField] [Range(0.2f, 2)] private float knockBackTime;
    private Rigidbody2D rb2D;
    private bool ready = false;
    private const float lifeTime = 10f;
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

    private void CheckAllEnemies(Collider2D collision)
    {
        BaseEnemy enemyBase = collision.GetComponent<BaseEnemy>();
        if (enemyBase != null)
        {
            UnitAudioHit();
            enemyBase.Damage = damage;
            float angle = LunarMath.VectorAngle(collision.transform.position, rb2D.position);
            Destroy(gameObject);
            if (enemyBase is MovingEnemy enemy)
            {
                enemy.Knockback(angle, knockBackForce, knockBackTime);
            }
            return;
        }
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
            CheckAllEnemies(collision);
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
