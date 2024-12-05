using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stats : ScriptableObject
{
    [SerializeField] [Range(0, 200)] protected int Health;
    [SerializeField] [Range(0, 10)] protected float movementSpeed;
    [SerializeField] protected Faction allegiance;
    [SerializeField] [Range(0,2)] protected float IFrameOnHit;
    protected float currentIFrame;
    protected float IFrameBlinkTime = 0.125f;
    protected float IFrameBlinkRunTime = 0;
    [HideInInspector] public UnityEvent onDeath;

    public int Damage
    {
        set
        {
            if (IFrameActive())
                return;
            Health -= value;
            setIFrameTime(IFrameOnHit);
            if (Health <= 0)
            {
                onDeath.Invoke();
            }
        }
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
            return true;
        }
        return false;
    }

    public void setIFrameTime(float toAdd)
    {
        currentIFrame =+ toAdd;
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
        set => Health += value;
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
