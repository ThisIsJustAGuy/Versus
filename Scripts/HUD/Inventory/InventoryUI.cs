using UnityEngine;
using System.Linq;
using System;

public class InventoryUI : MonoBehaviour
{
    InventorySlot weaponSlot;
    InventorySlot powerupSlot;
    public Inventory inventory;
    public string player = "";
    void Start()
    {
        inventory = GameObject.Find(player).GetComponent<Inventory>();
        inventory.onItemChangedCallback += UpdateUI;
        inventory.onPropertyChangedCallback += UpdateAmmo;
        if (GetComponentsInChildren<InventorySlot>().Count() == 2)
        {
            weaponSlot = GetComponentsInChildren<InventorySlot>()[0];
            powerupSlot = GetComponentsInChildren<InventorySlot>()[1];
        }
    }

    void UpdateUI()
    {
        if (inventory.weapon != null && weaponSlot != null)
            weaponSlot.AddItem(inventory.weaponObj, "weapon");
        if (inventory.ability != null && powerupSlot != null)
            powerupSlot.AddItem(inventory.abilityObj, "ability");
        if (inventory.weapon == null && weaponSlot != null)
            weaponSlot.RemoveItem("weapon");
        if (inventory.ability == null && powerupSlot != null)
            powerupSlot.RemoveItem("ability");
    }
    void UpdateAmmo()
    {
        if (weaponSlot != null && inventory.weapon != null)
            weaponSlot.UpdateAmmo(inventory.weaponObj.GetComponent<IWeapon>().ammo);
        if (powerupSlot != null && inventory.ability != null)
            powerupSlot.UpdateAmmo(Convert.ToInt32(inventory.abilityObj.GetComponent<IAbility>().uses));
    }
}
