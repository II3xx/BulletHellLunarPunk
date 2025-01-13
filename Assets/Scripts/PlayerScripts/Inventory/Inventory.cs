using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Inventory : MonoBehaviour
{

    [SerializeField] private Weapon startingWeapon;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private GameObject floatingText;
    private Item toAdd;
    private readonly List<Weapon> weapons = new();
    private int currentWeaponIndex = 0; 
    private readonly List<Item> inventoryItems = new();
    private GameObject toRemoveOnAdd;

    // Start is called before the first frame update
    void Start()
    {
        weapons.Add(startingWeapon);
        gameObject.GetComponentInChildren<PlayerWeapon>().OnWeaponChange(startingWeapon);
        InventoryUI();
    }

    // Inventory Add methods

    private void OnTextMake()
    {
        GameObject tempObject = Instantiate(floatingText);
        tempObject.GetComponentInChildren<TemporaryText>().OnStart("You have picked up " + toAdd.ItemName);
    }

    private void AddWeapon()
    {
        
        if(toAdd is Weapon weaponToAdd)
        {
            if (!NotHasWeapon(weaponToAdd))
            {
                return;
            }
            if (weapons.Count < 2)
            {
                weapons.Add(weaponToAdd);
                return;
            }
            OnTextMake();
            weapons[currentWeaponIndex] = weaponToAdd;
            gameObject.GetComponentInChildren<PlayerWeapon>().OnWeaponChange(weaponToAdd);
            Destroy(toRemoveOnAdd);
            InventoryUI();
        }
    }

    private void AddItem()
    {
        if (NotHasItem(toAdd))
        {
            OnTextMake();
            inventoryItems.Add(toAdd);
            InventoryUI();
            Destroy(toRemoveOnAdd);
        }
    }


    // Internal Inventory Check

    private bool NotHasWeapon(Weapon toAdd)
    {
        foreach (Weapon weapon in weapons)
        {
            if (weapon.ItemName.Equals(toAdd.ItemName))
            {
                return false;
            }
        }
        return true;
    }

    private bool NotHasItem(Item toAdd)
    {
        foreach(Item item in inventoryItems)
        {
            if(item.ItemName.Equals(toAdd.ItemName))
            {
                return false;
            }
        }
        return true;
    }


    // Outbound Inventory Methods

    public void InventoryUI()
    {
        List<Item> allItems = new();

        foreach(Item item in inventoryItems)
        {
            allItems.Add(item);
        }

        foreach(Weapon weapon in weapons)
        {
            allItems.Add(weapon);
        }

        inventoryUI.UpdatePanel(allItems);
    }

    // Outbound GameObject Removal Method

    public void SetGameObjectToRemoveOnItemAdd(GameObject toRemove)
    {
        toRemoveOnAdd = toRemove;
    }

    // Outbound Item Methods

    public void ItemToAdd(Item toAdd)
    {
        this.toAdd = toAdd;
        gameObject.GetComponent<PlayerScript>().AddToInteract(AddItem);
    }

    // Outbound weapon methods

    public void SetWeaponToAdd(Weapon toAdd)
    {
        this.toAdd = toAdd;
        gameObject.GetComponent<PlayerScript>().AddToInteract(AddWeapon);
    }

    public void OnScrollWheelWeaponSwap(InputAction.CallbackContext context)
    {
        if(!context.started)
        {
            return;
        }
        if (currentWeaponIndex == 0)
        {
            currentWeaponIndex++;
            gameObject.GetComponentInChildren<PlayerWeapon>().OnWeaponChange(weapons[currentWeaponIndex]);
        }
        else
        {
            currentWeaponIndex = 0;
            gameObject.GetComponentInChildren<PlayerWeapon>().OnWeaponChange(weapons[currentWeaponIndex]);
        }
    }

    public void OnNumberWeaponSwap(InputAction.CallbackContext context)
    {
        if(!context.started)
        {
            return;
        }
        int contextValue = Mathf.Clamp(context.ReadValue<int>(), 0, weapons.Count - 1);
        gameObject.GetComponentInChildren<PlayerWeapon>().OnWeaponChange(weapons[contextValue]);
    }
}
