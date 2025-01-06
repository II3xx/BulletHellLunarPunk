using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Book", menuName = "Items/Book", order = 1)]
public class Book : Item
{
    [SerializeField] DialogueHolder bookText;

    private Book()
    {
        category = ItemCategory.Book;
    }

    public DialogueHolder BookText
    {
        get => bookText;
    }
}
