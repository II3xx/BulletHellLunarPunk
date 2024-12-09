using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stats : ScriptableObject
{
    [SerializeField] [Range(0, 200)] protected int health;
    private int maxHealth;
    [SerializeField] [Range(0, 10)] protected float movementSpeed;
    [SerializeField] protected Faction allegiance;
    [SerializeField] [Range(0,2)] protected float IFrameOnHit;
    protected float currentIFrame;
    protected float IFrameBlinkTime = 0.125f;
    protected float IFrameBlinkRunTime = 0;
    private bool dashed = false;
    [HideInInspector] public UnityEvent onDeath;
    [SerializeField] protected Color onDamage = new Color(1,0.1f,0.1f,0.5f);

    private void Awake()
    {
        maxHealth = health;
    }

    public Color OnDamage
    {
        get => onDamage;
    }

    public bool Dashed
    {
        get => dashed;
    }

    public int Damage
    {
        set
        {
            if (IFrameActive())
                return;
            health -= value;
            setIFrameTime(IFrameOnHit, false);
            if (health <= 0)
            {
                onDeath.Invoke();
            }
        }
    }

    public int Health
    {
        get => health;
    }

    public bool UpdateIFrameBlink()
    {
        IFrameBlinkRunTime += Time.deltaTime;
        return IfFrameBlink();
    }

    public bool IfFrameBlink()
    {
        if(IFrameBlinkTime <= IFrameBlinkRunTime)
        {
            IFrameBlinkRunTime = 0;
            return true;
        }
        return false;
    }

    public void setIFrameTime(float toAdd, bool dash)
    {
        currentIFrame =+ toAdd;
        if(dash)
        {
            dashed = true;
        }
        else
        {
            dashed = false;
        }
    }

    public bool IFrameActive()
    {
        return currentIFrame > 0;
    }

    public void UpdateIframe()
    {
        currentIFrame = Mathf.Clamp((currentIFrame-Time.deltaTime), 0, 8);
    }

    public int Healing
    {
        set => health += value;
    }

    public float Speed
    {
        get => movementSpeed;
    }

    public Faction Allegiance
    {
        get => allegiance;
    }

}
