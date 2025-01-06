using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{

    [SerializeField] private Weapon startingWeapon;
    private Item toAdd;
    private readonly List<Weapon> weapons = new();
    private int currentWeaponIndex = 0; 
    private readonly List<Item> inventoryItems = new();

    // Start is called before the first frame update
    void Start()
    {
        weapons.Add(startingWeapon);
        gameObject.GetComponentInChildren<PlayerWeapon>().OnWeaponChange(startingWeapon);
    }

    // Inventory Add methods

    private void AddWeapon()
    {
        if(toAdd is Weapon weaponToAdd)
        {
            if (weapons.Count < 2)
            {
                weapons.Add(weaponToAdd);
                return;
            }
            weapons[currentWeaponIndex] = weaponToAdd;
            gameObject.GetComponentInChildren<PlayerWeapon>().OnWeaponChange(weaponToAdd);
        }
    }

    private void AddItem()
    {
        inventoryItems.Add(toAdd);
    }




    // Outbound Inventory Methods

    

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
