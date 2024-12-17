using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : ScriptableObject
{
    [SerializeField] private List<string> DialogueText;
    [SerializeField] private bool isRunic;

    public bool IsRunic
    {
        get => IsRunic;
    }

    public string nextString(int current)
    {
        return DialogueText[current++];
    }
}
