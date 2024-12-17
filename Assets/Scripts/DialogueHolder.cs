using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/DialogueText", order = 1)]
public class DialogueHolder : ScriptableObject
{
    [SerializeField] private List<string> DialogueText;
    [SerializeField] private bool isRunic;
    private int current;

    public bool IsRunic
    {
        get => IsRunic;
    }

    public string nextString()
    {
        string dialogeuToReturn = DialogueText[current];
        current++;
        return dialogeuToReturn;
    }
}
