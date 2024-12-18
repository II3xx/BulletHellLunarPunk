using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine;

public class DialogueReader : MonoBehaviour
{
    private readonly Dictionary<DialogueHolder, int> unResetableHolders = new();
    DialogueHolder currentText = null;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] Animator runicAnimator;
    private TMP_FontAsset defaultFont;
    private int currentIndex = 0;
    private int maxIndex = 0;


    private void Start()
    {
        defaultFont = textMesh.font; 
    }

    public void SetHolder(DialogueHolder holder)
    {
        currentText = holder;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().AddToInteract(OnDialogueEnter);
        maxIndex = currentText.DialogueSize;
        textMesh.fontSize = currentText.FontSize;
        if (currentText.DefaultFont != null)
            textMesh.font = currentText.DefaultFont;
        else
            textMesh.font = defaultFont;
    }

    public void OnExitHolder()
    {
        currentText = null;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().RemoveFromInteract(OnDialogueEnter);
    }

    public string NextString()
    {
        string dialogeuToReturn = currentText.GetStringIndex(currentIndex);
        currentIndex++;
        return dialogeuToReturn;
    }

    public void OnDialogueEnter()
    {
        if(currentText == null)
        {
            return;
        }
        unResetableHolders.TryGetValue(currentText, out currentIndex);
        if (currentIndex >= maxIndex)
        {
            return;
        }
        string textStart = NextString();
        if (textStart.Equals(""))
        {
            return;
        }
        if (currentText.IsRunic)
        {
            StartRunicText();
        }
        else
        {
            StartNormalText();
        }
        textMesh.text = textStart;


        playerInput.SwitchCurrentActionMap("UI");
    }

    public void OnNextDialogue(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            string text = NextString();
            textMesh.text = text;
            if (text.Equals(""))
            {
                EndDialogue();
            }
        }
    }

    private void StartRunicText()
    {
        runicAnimator.SetTrigger("Runic");
        runicAnimator.SetTrigger("Reading");
    }

    private void StartNormalText()
    {
        runicAnimator.SetTrigger("Runic");
        runicAnimator.SetTrigger("Reading");
    }

    private void EndRunicText()
    {
        runicAnimator.SetTrigger("Ending");
    }

    private void EndNormalText()
    {
        runicAnimator.SetTrigger("Ending");
    }

    private void EndDialogue()
    {
        if(currentText.IsRunic)
        {
            EndRunicText();
        }
        else
        {
            EndNormalText();
        }
        playerInput.SwitchCurrentActionMap("Player");
        if (!currentText.Resetable)
        {
            unResetableHolders.Add(currentText, currentIndex);
            currentText = null;
        }
        else
            currentIndex = 0;
    }
}
