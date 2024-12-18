using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/DialogueText", order = 1)]
public class DialogueHolder : ScriptableObject
{
    [SerializeField] private List<string> DialogueText;
    [SerializeField] private bool isRunic;
    [SerializeField] [Range(12,36)] private float fontSize = 24;
    [SerializeField] private TMP_FontAsset defaultFont;
    private int current;

    public bool IsRunic
    {
        get => IsRunic;
    }

    public string NextString()
    {
        if (current == DialogueText.Count)
            return "";
        string dialogeuToReturn = DialogueText[current];
        current++;
        return dialogeuToReturn;
    }

    public TMP_FontAsset DefaultFont
    {
        get => defaultFont;
    }

    public float FontSize
    {
        get => fontSize;
    }
}
