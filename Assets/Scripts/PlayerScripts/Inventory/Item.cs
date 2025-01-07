using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemCategory
{
    Key,
    Weapon,
    Book
}

public abstract class Item : ScriptableObject
{
    [SerializeField] private string itemName;
    [TextArea] [SerializeField] private string itemDescription;
    protected ItemCategory category;
    [SerializeField] private Sprite itemSprite;

    public Sprite ItemSprite
    {
        get => itemSprite;
    }

    public string ItemName
    {
        get => itemName;
    }

    public ItemCategory Category
    {
        get => category;
    }
}
