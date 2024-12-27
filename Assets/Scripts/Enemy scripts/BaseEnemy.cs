using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

abstract public class BaseEnemy : MonoBehaviour
{
    private readonly Faction allegiance = Faction.enemy;
    protected GameObject player;
    [SerializeField] protected EnemyBaseStats enemyStats;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] private UnityEvent onDeath;
    private int health;
    private bool iFramed = false;

    virtual protected void OnStart()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = enemyStats.MaxHealth;
    }

    private void OnDeath()
    {
        onDeath.Invoke();
        Destroy(gameObject);
    }

    public Faction Allegiance
    {
        get => allegiance;
    }

    public int Damage
    {
        set
        {
            Debug.Log(health);
            if (iFramed)
                return;
            health -= value;
            StartCoroutine(IFrameBlinker());
            if (health <= 0)
            {
                OnDeath();
            }
        }
    }

    // IFrame Stuff

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

    IEnumerator IFrameBlinker()
    {
        iFramed = true;
        for (float runTime = 0; runTime <= enemyStats.IFrameOnHit; runTime += enemyStats.IFrameBlinkTime)
        {
            UpdateBlinkDamage();
            yield return new WaitForSeconds(enemyStats.IFrameBlinkTime);
        }
        iFramed = false;
        spriteRenderer.color = new Color(1, 1, 1, 1);
        yield break;
    }
}
