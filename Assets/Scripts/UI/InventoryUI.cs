using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class InventoryUI : MonoBehaviour
{

    [SerializeField] GameObject itemPanelPrefab;
    [SerializeField] GameObject weaponPanelPrefab;
    [SerializeField] DialogueReader dialogueReader;
    [SerializeField] GameObject toolTipObjectPrefab;
    [SerializeField] private Animator animator;
    private readonly List<GameObject> itemPanels = new();
    private readonly List<GameObject> weaponPanels = new();
    private readonly Dictionary<GameObject, Item> itemDict = new();
    private GameObject toolTipObject;
    private GameObject currentObjectSelected;
    private bool isTooltiping = false;
    private bool initialized = false;
    private bool turnOff = false;

    private void Initialize()
    {
        animator = gameObject.GetComponent<Animator>();
        for(int i = 0; i < 2; i++)
        {
            CreateWeaponPanel(i);
        }

        for (int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                CreateItemPanel(i, j);
            }
        }
        initialized = true;
    }

    private void CreateWeaponPanel(int i)
    {
        GameObject weaponPanel = Instantiate(weaponPanelPrefab);
        weaponPanel.transform.SetParent(transform);
        weaponPanel.transform.localScale = new Vector3(1, 1, 1);
        weaponPanel.transform.localPosition = new Vector3(-53.9f + (107.8f * i), 124.4f, 0);
        weaponPanels.Add(weaponPanel);
    }

    private void CreateItemPanel(int i, int j)
    {
        GameObject itemPanel = Instantiate(itemPanelPrefab);
        itemPanel.transform.SetParent(transform);
        itemPanel.transform.localScale = new Vector3(1, 1, 1);
        itemPanel.transform.localPosition = new Vector3(-81.9f - -54.7f * j, 30.5f - 57.3f * i, 0);
        itemPanels.Add(itemPanel);
    }

    public void UpdatePanel(List<Item> items)
    {
        if(!initialized)
        {
            Initialize();
        }
        int itemPanelUpdated = 0;
        int weaponPanelUpdated = 0;
        itemDict.Clear();
        foreach (Item item in items)
        {
            if(item.Category == ItemCategory.Weapon)
            {
                itemDict.Add(weaponPanels[weaponPanelUpdated], item);
                weaponPanels[weaponPanelUpdated].GetComponent<UIImageHolder>().HeldImage.sprite = item.ItemSprite;
                weaponPanels[weaponPanelUpdated].GetComponent<UIImageHolder>().HeldImage.color = new Color(1, 1, 1, 1);
                weaponPanelUpdated++;
            }
            else
            {
                itemDict.Add(itemPanels[itemPanelUpdated], item);
                itemPanels[itemPanelUpdated].GetComponent<UIImageHolder>().HeldImage.sprite = item.ItemSprite;
                itemPanels[itemPanelUpdated].GetComponent<UIImageHolder>().HeldImage.color = new Color(1, 1, 1, 1);
                itemPanelUpdated++;
            }
        }
    }

    public void TriggerInventory(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;
        if (dialogueReader.CurrentlyReading())
            return;
        turnOff = false;
        animator.SetTrigger("InventoryTrigger");
    }

    private void ShowTooltip(GameObject gameObject)
    {
        if (isTooltiping)
            return;
        isTooltiping = true;
        currentObjectSelected = gameObject;
        if (itemDict.TryGetValue(gameObject, out Item itemDisc))
        {
            toolTipObject = Instantiate(toolTipObjectPrefab);
            toolTipObject.transform.SetParent(transform);
            toolTipObject.transform.localScale = new(1, 1, 1);
            NextToMouse next = toolTipObject.GetComponent<NextToMouse>();
            next.BodyText = itemDisc.ItemDescription;
            next.NameText = itemDisc.ItemName;
            next.transform.position = gameObject.transform.position - new Vector3(2, -0.35f, 0);

            if(itemDisc is Book book)
            {
                next.ReadText.alpha = 1f;
                SetDialogueHolder(book);
            }
            else
            {
                next.ReadText.alpha = 0f;
            }
        }
    }

    private void SetDialogueHolder(Book book)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().StaggerAddToInteract(EventTrigger);
        dialogueReader.InventorySetHolder(book.BookText, EventTrigger);
    }

    private void RemoveDialogueHolder()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().RemoveAllInteracts();
        dialogueReader.OnExitInventory();
    }

    private void HideToolTip()
    {
        Destroy(toolTipObject);
    }

    private void EventTrigger()
    {
        if (turnOff)
        {
            turnOff = false;
        }
        else
        {
            turnOff = true;
        }
        isTooltiping = false;
        HideToolTip();
        animator.SetTrigger("InventoryTrigger");
    }

    private void CheckToolTip()
    {
        var cameraData = Camera.main.GetUniversalAdditionalCameraData();
        Vector3 Mousepos = cameraData.cameraStack[0].ScreenToWorldPoint(Input.mousePosition);
        if(itemPanels.Contains(currentObjectSelected))
        {
            if (Vector2.Distance(Mousepos, currentObjectSelected.transform.position) < 0.4f)
            {
                return;
            }
        }
        else
        {
            if (Vector2.Distance(Mousepos, currentObjectSelected.transform.position) < 1)
            {
                return;
            }
        }

        RemoveDialogueHolder();
        isTooltiping = false;
        HideToolTip();
    }

    private void Update()
    {
        if (turnOff)
        {
            return;
        }
            
        if (isTooltiping)
        {
            CheckToolTip();
            return;
        }
        var cameraData = Camera.main.GetUniversalAdditionalCameraData();
        Vector3 Mousepos = cameraData.cameraStack[0].ScreenToWorldPoint(Input.mousePosition);

        
        
        foreach (GameObject gameObject in itemPanels)
        {
            if (Vector2.Distance(Mousepos,gameObject.transform.position) < 0.4f)
            {
                ShowTooltip(gameObject);
                return;
            }
        }

        foreach (GameObject gameObject in weaponPanels)
        {
            if (Vector2.Distance(Mousepos, gameObject.transform.position) < 1)
            {
                ShowTooltip(gameObject);
                return;
            }
        }
    }

}
