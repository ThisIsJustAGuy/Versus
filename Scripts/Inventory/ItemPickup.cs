using System;
using UnityEngine;

public class ItemPickup : Interactable
{
    public Weapon weapon;
    public Ability ability;
    public Inventory inventory;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        if (weapon != null)
            spriteRenderer.sprite = weapon.sprite;
        else if (ability != null)
            spriteRenderer.sprite = ability.sprite;
        Vector2 S = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
        gameObject.GetComponent<BoxCollider2D>().size = S;
    }

    public override void PickUp(string player)
    {
        inventory = (player == "Player1" ? GameObject.Find("Player1").GetComponent<Inventory>() : GameObject.Find("Player2").GetComponent<Inventory>());
        base.PickUp(player);
        PickItUp(player);
    }

    private void PickItUp(string player)
    {
        if (inventory.weapon == null && weapon != null)
            inventory.Add(weapon, gameObject);
        else if (inventory.weapon == null && weapon != null)
            Debug.Log(player + " already has a weapon");
        if (inventory.ability == null && ability != null)
            inventory.Add(ability, gameObject);
        else if (inventory.ability != null && ability != null)
            Debug.Log(player + " already has an ability");
    }
}
