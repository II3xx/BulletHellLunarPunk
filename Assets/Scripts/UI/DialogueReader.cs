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
    private bool hasRead = false;


    private void Start()
    {
        defaultFont = textMesh.font; 
    }

    private IEnumerator TextTimer()
    {
        hasRead = true;
        for (float i = 0; i <= 2; i += Time.deltaTime)
        {
            yield return new WaitForSeconds(0.2f);
        }
        hasRead = false;
        yield break;
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

    public void OnEscapeHolder(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            EndDialogue();
        }
    }

    public string NextString()
    {
        string dialogeuToReturn = currentText.GetStringIndex(currentIndex);
        currentIndex++;
        return dialogeuToReturn;
    }

    public void OnDialogueEnter()
    {
        if(hasRead)
        {
            return;
        }
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
        runicAnimator.SetBool("Runic", true);
        runicAnimator.SetTrigger("Reading");
    }

    private void StartNormalText()
    {
        runicAnimator.SetBool("Runic", false);
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
        StartCoroutine(TextTimer());
        if (currentText.IsRunic)
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
