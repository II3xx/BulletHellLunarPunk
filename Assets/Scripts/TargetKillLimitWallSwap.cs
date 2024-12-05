using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetKillLimitWallSwap : MonoBehaviour
{
    [SerializeField] Collider2D ToggleCollider;
    [SerializeField] [Range(0,10)] int TotalKills = 0;

    private void Awake()
    {
        ToggleCollider = GetComponent<Collider2D>();
        ToggleCollider.isTrigger = false;
    }

    public void OnKillUpdate()
    {
        TotalKills--;
        if(TotalKills <= 0)
        {
            ToggleCollider.isTrigger = true;
        }
    }

}
