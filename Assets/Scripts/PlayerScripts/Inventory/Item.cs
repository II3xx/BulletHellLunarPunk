using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemCategory
{
    Key,
    Weapon,
    Book
}

[CreateAssetMenu(fileName = "GenericItem", menuName = "Items/Item", order = 1)]
public class Item : ScriptableObject
{
    [TextArea] private readonly string itemName;
    [SerializeField] protected ItemCategory category;

    public string ItemName
    {
        get => itemName;
    }

    public ItemCategory Category
    {
        get => category;
    }
}
