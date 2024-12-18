using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent navAgent;
    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private float moveRunTime = 0;
    [Tooltip("How long until it will retry to move")]
    [SerializeField] [Range(0.5f,8)] float timeBetweenMoves;
    [Tooltip("The chance upon an attempt to move to change desired location")]
    [SerializeField] [Range(1, 100)] float moveChance;
    [SerializeField] private UnityEvent onDeath;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navAgent = gameObject.GetComponent<NavMeshAgent>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyStats = enemyStats.CopyStats(enemyStats);
        enemyStats.onDeath.AddListener(OnDeath);
        navAgent.speed = enemyStats.Speed;
        navAgent.updateRotation = false;
        navAgent.updateUpAxis = false;
    }

    private void OnDeath()
    {
        onDeath.Invoke();
        Destroy(gameObject);
    }

    public Faction Allegiance
    {
        get => enemyStats.Allegiance;
    }

    public int Damage
    {
        set => enemyStats.Damage = value;
    }

    public void Knockback(float knockbackAngle, float knockBackAmount, float knockBackTime)
    {
        Vector2 velocity = new(knockBackAmount * Mathf.Cos(knockbackAngle), knockBackAmount * Mathf.Sin(knockbackAngle));
        StartCoroutine(OnKnockback(knockBackTime, velocity));
    }

    IEnumerator OnKnockback(float maxTimer, Vector2 Velocity)
    {
        float normalAccel = navAgent.acceleration;
        navAgent.acceleration = 0;
        navAgent.velocity = Velocity;
        for (float i = 0; maxTimer > i; i += Time.deltaTime)
        {
            navAgent.velocity = Vector2.Lerp(Velocity, new(0, 0), 0.1f);
            yield return null;
        }
        navAgent.acceleration = normalAccel;
    }

    private void UpdateBlinkDamage()
    {
        if (spriteRenderer.color.a == enemyStats.OnDamage.a)
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);
        }
        else
        {
            spriteRenderer.color = enemyStats.OnDamage;
        }
    }

    private void UpdateBlink()
    {
        if (enemyStats.IFrameActive())
        {
            if (enemyStats.UpdateIFrameBlink())
            {
                UpdateBlinkDamage();
            }
        }
        else if (spriteRenderer.color.a == enemyStats.OnDamage.a)
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);
        }
    }

    Vector2 NewTestPosition()
    {
        Vector3 Dest = player.GetComponent<Rigidbody2D>().position;
        float angle2 = Random.Range(0, 360) * Mathf.Deg2Rad;

        Vector2 maxPos = new(enemyStats.MaxDistance * Mathf.Cos(angle2), enemyStats.MaxDistance * Mathf.Sin(angle2));
        Vector2 minPos = new(enemyStats.MinDistance * Mathf.Cos(angle2), enemyStats.MinDistance * Mathf.Sin(angle2));

        Vector3 Destin = new(Dest.x + Random.Range(minPos.x, maxPos.x), Dest.y + Random.Range(minPos.y, maxPos.y), 0);

        return Destin;
    }

    void UpdatePosition()
    {
        moveRunTime += Time.deltaTime;
        if(moveRunTime < timeBetweenMoves)
        {
            moveRunTime += Time.deltaTime;
            return;
        }
        moveRunTime = 0;

        if(Random.Range(0,100) < moveChance)
        {
            return;
        }

        Vector2 Destin = NewTestPosition();

        if (navAgent.isOnNavMesh)
            navAgent.SetDestination(Destin);
    }

    // Update is called once per frame
    void Update()
    {
        enemyStats.UpdateIframe();
        UpdateBlink();
        UpdatePosition();
    }
}
