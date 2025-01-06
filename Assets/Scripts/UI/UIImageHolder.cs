using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIImageHolder : MonoBehaviour
{
    [SerializeField] Image image;

    public Image HeldImage
    {
        get => image;
    }
}
