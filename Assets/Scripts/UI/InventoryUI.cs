using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{

    [SerializeField] GameObject itemPanelPrefab;
    [SerializeField] GameObject weaponPanelPrefab;
    private Animator animator;
    private readonly List<GameObject> itemPanels = new();
    private readonly List<GameObject> weaponPanels = new();

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        for(int i = 0; i < 2; i++)
        {
            GameObject weaponPanel = Instantiate(weaponPanelPrefab);
            weaponPanel.transform.SetParent(transform);
            weaponPanel.transform.localScale = new Vector3(1, 1, 1);
            weaponPanel.transform.localPosition = new Vector3(-53.9f + (107.8f * i), 124.4f, 0);
            weaponPanels.Add(weaponPanel);
        }

        for (int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                GameObject itemPanel = Instantiate(itemPanelPrefab);
                itemPanel.transform.SetParent(transform);
                itemPanel.transform.localScale = new Vector3(1, 1, 1);
                itemPanel.transform.localPosition = new Vector3(-81.9f - -54.7f * j, 30.5f - 57.3f * i, 0);
                itemPanels.Add(itemPanel);
            }
        }
    }

    public void UpdatePanel(Sprite[] itemImages, Sprite[] weaponImages)
    {
        for (int i = 0; i < itemImages.Length; i++)
        {
            Image itemImage = itemPanels[i].GetComponent<UIImageHolder>().HeldImage;
            itemImage.sprite = itemImages[i];
            itemImage.color = new Color(1, 1, 1, 1);
        }

        for (int i = 0; i < weaponImages.Length; i++)
        {
            Image weaponImage = weaponPanels[i].GetComponent<UIImageHolder>().HeldImage;
            weaponImage.sprite = weaponImages[i];
            weaponImage.color = new Color(1, 1, 1, 1);
        }
    }

    public void TriggerInventory(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;
        animator.SetTrigger("InventoryTrigger");
    }
    
}
