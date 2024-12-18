using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine;

public class DialogueReader : MonoBehaviour
{

    DialogueHolder currentText = null;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] Animator runicAnimator;
    
        
    public void SetHolder(DialogueHolder holder)
    {
        this.currentText = holder;
    }

    public void OnDialogueEnter()
    {
        if(currentText == null)
        {
            return;
        }
        if (currentText.IsRunic)
        {

        }
        else
        {

        }
        OnNextDialogue();
        playerInput.SwitchCurrentControlScheme("UI");
    }

    public void OnNextDialogue()
    {
        string text = currentText.nextString();
        if (text.Equals(""))
        {
            EndDialogue();
        }
        textMesh.text = text;
    }

    private void StartRunicText()
    {
        runicAnimator.SetBool("Runic", true);
        runicAnimator.SetBool("Reading", true);
    }

    private void StartNormalText()
    {
        runicAnimator.SetBool("Runic", false);
        runicAnimator.SetBool("Reading", true);
    }

    private void EndRunicText()
    {
        runicAnimator.SetBool("Ending", true);
    }

    private void EndNormalText()
    {
        runicAnimator.SetBool("Ending", true);
    }

    private void EndDialogue()
    {
        if(currentText.IsRunic)
        {

        }
        else
        {

        }
        playerInput.SwitchCurrentControlScheme("Player");
        currentText = null;
    }
}
