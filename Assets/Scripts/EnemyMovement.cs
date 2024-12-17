using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D), typeof(NavMeshAgent))]

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent navAgent;
    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private float moveRunTime = 0;
    [SerializeField] [Range(0.5f,8)] float timeBetweenMoves;
    [SerializeField] [Range(1, 100)] float moveChance;
    Rigidbody2D rb2D;
    [SerializeField] private UnityEvent onDeath;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navAgent = gameObject.GetComponent<NavMeshAgent>();
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyStats = enemyStats.CopyStats(enemyStats);
        enemyStats.onDeath.AddListener(OnDeath);
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

    private float AngleMath(Vector2 Dest)
    {
        return Mathf.Atan2(rb2D.position.y - Dest.y, rb2D.position.x - Dest.x);
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
