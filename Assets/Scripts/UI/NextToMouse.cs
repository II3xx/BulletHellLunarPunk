using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;

public class NextToMouse : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bodyText, nameText, readText;

    public string BodyText
    {
        set => bodyText.text = value;
    }

    public string NameText
    {
        set => nameText.text = value;
    }

    public TextMeshProUGUI ReadText
    {
        get => readText;
    }
}
