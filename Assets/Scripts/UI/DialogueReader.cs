using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogueReader : MonoBehaviour
{
    private readonly Dictionary<DialogueHolder, int> unResetableHolders = new();
    DialogueHolder currentText = null;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] Animator runicAnimator;
    private readonly UnityEvent onRead = new();
    private bool onEndEmpty = false;
    private TMP_FontAsset defaultFont;
    private int currentIndex = 0;
    private int maxIndex = 0;
    private string currentLine;
    private Coroutine currentTextDelay;
    private bool hasRead = false;

    private void Start()
    {
        defaultFont = textMesh.font; 
    }

    private IEnumerator TextTimer()
    {
        hasRead = true;
        for (float i = 0; i <= 1; i++)
        {
            yield return new WaitForSeconds(0.5f);
        }
        hasRead = false;
        yield break;
    }

    private IEnumerator TextDelay()
    {
        textMesh.text = "";
        for(int i = 0; i < currentLine.Length; i++)
        {
            textMesh.text += currentLine[i];
            yield return new WaitForSeconds(0.05f);
        }
        currentTextDelay = null;
        yield break;
    }

    private void InternalSetHolder(DialogueHolder holder)
    {
        currentText = holder;
        maxIndex = currentText.DialogueSize;
        textMesh.fontSize = currentText.FontSize;
        if (currentText.DefaultFont != null)
            textMesh.font = currentText.DefaultFont;
        else
            textMesh.font = defaultFont;
    }

    public void InventorySetHolder(DialogueHolder holder, UnityAction action)
    {
        InternalSetHolder(holder);
        onRead.AddListener(action);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().StaggerAddToInteract(OnDialogueEnter);
    }

    public void SetHolder(DialogueHolder holder)
    {
        InternalSetHolder(holder);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().AddToInteract(OnDialogueEnter);
    }

    public void OnExitHolder()
    {
        currentText = null;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().RemoveFromInteract(OnDialogueEnter);
    }

    public void OnExitInventory()
    {
        onEndEmpty = true;
    }

    public void OnEscapeHolder(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            StopTextDelay();
            EndDialogue();
        }
    }

    public void NextString()
    {
        currentLine = currentText.GetStringIndex(currentIndex);
        currentIndex++;
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
        NextString();
        if (currentLine.Equals(""))
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
        currentTextDelay = StartCoroutine(TextDelay());

        playerInput.SwitchCurrentActionMap("UI");
    }

    private void StopTextDelay()
    {
        StopCoroutine(currentTextDelay);
        currentTextDelay = null;
    }

    public void OnNextDialogue(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;
        if(currentTextDelay != null)
        {
            StopTextDelay();
            textMesh.text = currentLine;
            return;
        }
        NextString();
        currentTextDelay = StartCoroutine(TextDelay());
        if (currentLine.Equals(""))
        {
            EndDialogue();
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
        textMesh.text = "";
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
        onRead.Invoke();
        onRead.RemoveAllListeners();
        if (onEndEmpty)
        {
            onEndEmpty = false;
            OnExitHolder();
        }
    }
}
