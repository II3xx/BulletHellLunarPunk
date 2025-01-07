using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/DialogueText", order = 1)]
public class DialogueHolder : ScriptableObject
{
    [SerializeField] [TextArea] private List<string> DialogueText;
    [SerializeField] private bool isRunic;
    [SerializeField] [Range(12,36)] private float fontSize = 24;
    [SerializeField] private TMP_FontAsset defaultFont;
    [SerializeField] private bool resetable;

    public bool IsRunic
    {
        get => isRunic;
    }

    public TMP_FontAsset DefaultFont
    {
        get => defaultFont;
    }

    public float FontSize
    {
        get => fontSize;
    }

    public int DialogueSize
    {
        get => DialogueText.Count;
    }

    public bool Resetable
    {
        get => resetable;
    }

    public string GetStringIndex(int index)
    {
        if (index >= DialogueText.Count)
            return "";
        return DialogueText[index];
    }
}
