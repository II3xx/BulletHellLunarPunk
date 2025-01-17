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
    protected SpriteRenderer spriteRenderer;
    [SerializeField] private UnityEvent onDeath;
    
    protected int health;
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

    private void CreateTempAudio(AudioClip audioClip)
    {
        var temp = GameObject.CreatePrimitive(PrimitiveType.Plane);
        temp.transform.position = transform.position;
        var tempaudio = temp.AddComponent<TemporaryAudioSource>();
        tempaudio.setClipAndPlay(audioClip);
        temp.GetComponent<MeshRenderer>().enabled = false;
    }

    public int Damage
    {
        set
        {
            if (iFramed)
                return;
            health -= value;
            StartCoroutine(IFrameBlinker());
            if (health <= 0)
            {
                OnDeath();
            }
            else
            {
                CreateTempAudio(enemyStats.OnHitSound);
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
