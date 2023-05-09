using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public delegate void OnPropertyChanged();
    public OnPropertyChanged onPropertyChangedCallback;

    public Weapon weapon = null;
    public Ability ability = null;
    public GameObject weaponObj = null;
    public GameObject abilityObj = null;
    public GameObject Hand = null;
    public Transform DroppedParent = null;

    public void Add(Weapon pickupWeapon, GameObject weaponObj)
    {
        this.weaponObj = weaponObj;
        weapon = pickupWeapon;
        var heldWeapon = weaponObj;

        heldWeapon.transform.position = Hand.transform.position;
        heldWeapon.transform.rotation = Hand.transform.rotation;

        heldWeapon.transform.parent = Hand.transform;

        Destroy(heldWeapon.GetComponent<Rigidbody2D>());
        heldWeapon.GetComponent<BoxCollider2D>().enabled = false;
        heldWeapon.GetComponent<ItemPickup>().enabled = false;

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
    public void Add(Ability pickupAbility, GameObject abilityObj)
    {
        this.abilityObj = abilityObj;
        ability = pickupAbility;
        var currentAbility = abilityObj;

        currentAbility.transform.rotation = Hand.transform.rotation;

        currentAbility.GetComponent<SpriteRenderer>().enabled = false;

        currentAbility.transform.parent = gameObject.transform;

        Destroy(currentAbility.GetComponent<Rigidbody2D>());
        currentAbility.GetComponent<BoxCollider2D>().enabled = false;
        currentAbility.GetComponent<ItemPickup>().enabled = false;

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
    public void Remove(GameObject Obj)
    {
        if (Obj.tag == "Weapon")
        {
            if (weapon != null)
            {
                Obj.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
                Obj.transform.parent = DroppedParent.transform;
                Obj.GetComponent<ItemPickup>().enabled = true;
                Obj.GetComponent<ItemPickup>().isPickedUp = false;
                Obj.AddComponent<Rigidbody2D>();
                Obj.GetComponent<BoxCollider2D>().enabled = true;
            }
            else
                Debug.Log("You don't have a weapon");

            weapon = null;
        }
        else if (Obj.tag == "Ability")
        {
            if (ability != null && abilityObj.GetComponent<IAbility>().uses <= 0)
                Destroy(Obj);
            else if (ability != null)
            {
                Obj.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
                Obj.transform.parent = DroppedParent.transform;
                Obj.GetComponent<SpriteRenderer>().enabled = true;
                Obj.GetComponent<ItemPickup>().enabled = true;
                Obj.GetComponent<ItemPickup>().isPickedUp = false;
                Obj.AddComponent<Rigidbody2D>();
                Obj.GetComponent<BoxCollider2D>().enabled = true;
            }
            else
                Debug.Log("You don't have an ability");

            ability = null;
        }
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
