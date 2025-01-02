using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Key", menuName = "Items/Key", order = 1)]
public class Key : Item
{
    [SerializeField] private string openable;

    public string Openable
    {
        get => openable;
    }
}
