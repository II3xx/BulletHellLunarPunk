using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon", order = 1)]
public class Weapon : Item
{
    [SerializeField] private WeaponStats stats;

    private Weapon()
    {
        category = ItemCategory.Weapon;
    }

    public WeaponStats Stats
    {
        get => stats;
    }
}
