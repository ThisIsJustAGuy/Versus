using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InventorySlot : InventoryUI
{
    Weapon weapon;
    Ability ability;
    public Image sprite;
    public GameObject ammoUI;
    public GameObject itemName;
    public void AddItem(GameObject obj, string item)
    {
        if (item == "weapon")
        {
            weapon = obj.GetComponent<ItemPickup>().weapon;

            if (weapon.sprite.rect.width > weapon.sprite.rect.height) //a hosszab oldalhoz viszonyítva nagyítunk, hogy ne torzuljon
                sprite.rectTransform.sizeDelta = new Vector2(weapon.sprite.rect.width * (30 / weapon.sprite.rect.width), weapon.sprite.rect.height * (30 / weapon.sprite.rect.width));
            else
                sprite.rectTransform.sizeDelta = new Vector2(weapon.sprite.rect.width * (30 / weapon.sprite.rect.height), weapon.sprite.rect.height * (30 / weapon.sprite.rect.height));

            sprite.sprite = weapon.sprite;
            UpdateAmmo(inventory.weaponObj.GetComponent<IWeapon>().ammo);
            itemName.GetComponent<TextMeshProUGUI>().text = weapon.name;
        }
        else if (item == "ability")
        {
            ability = obj.GetComponent<ItemPickup>().ability;

            if (ability.sprite.rect.width > ability.sprite.rect.height) //a hosszab oldalhoz viszonyítva nagyítunk, hogy ne torzuljon
                sprite.rectTransform.sizeDelta = new Vector2(ability.sprite.rect.width * (30 / ability.sprite.rect.width), ability.sprite.rect.height * (30 / ability.sprite.rect.width));
            else
                sprite.rectTransform.sizeDelta = new Vector2(ability.sprite.rect.width * (30 / ability.sprite.rect.height), ability.sprite.rect.height * (30 / ability.sprite.rect.height));

            sprite.sprite = ability.sprite;
            UpdateAmmo(Convert.ToInt32(inventory.abilityObj.GetComponent<IAbility>().uses));
            itemName.GetComponent<TextMeshProUGUI>().text = ability.name;
        }

        sprite.enabled = true;
    }

    public void RemoveItem(string remov)
    {
        if (remov == "weapon")
        {
            weapon = null;
            UpdateAmmo(-1);
        }
        else if (remov == "ability")
        {
            ability = null;
            UpdateAmmo(-1);
        }
        sprite.sprite = null;
        sprite.enabled = false;
        itemName.GetComponent<TextMeshProUGUI>().text = "--";
    }

    public void UpdateAmmo(int ammo)
    {
        if (ammo != -1)
            ammoUI.GetComponent<TextMeshProUGUI>().text = ammo.ToString();
        else
            ammoUI.GetComponent<TextMeshProUGUI>().text = "--";
    }

    private void Update()
    {
        if (gameObject.name == "P1Weapon" && weapon != null)
        {
            if (Input.GetButtonDown("P1_Drop") && player == "Player1")
                OnDropPressed();
        }
        else if (gameObject.name == "P2Weapon" && weapon != null)
        {
            if (Input.GetButtonDown("P2_Drop") && player == "Player2")
                OnDropPressed();
        }
    }

    public void OnDropPressed()
    {
        inventory.Remove(inventory.weaponObj);
    }
}
