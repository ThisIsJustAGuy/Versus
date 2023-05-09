using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour, IAbility
{
    Transform parent;
    public float uses { get; set; }
    private void Start()
    {
        uses = gameObject.GetComponent<ItemPickup>().ability.uses;
    }
    public IEnumerator Use()
    {
        parent = gameObject.transform.parent;

        if (uses > 0 && (parent.GetComponent<PlayerMovement>().moveSpeed == parent.GetComponent<PlayerMovement>().defSpeed))
        {
            parent.GetComponent<PlayerMovement>().moveSpeed *= 1.5f;
            uses--;
            yield return new WaitForSeconds(gameObject.GetComponent<ItemPickup>().ability.duration);

            parent.GetComponent<PlayerMovement>().moveSpeed /= 1.5f;
            if (uses <= 0)
                parent.GetComponent<Inventory>().Remove(parent.GetComponent<Inventory>().abilityObj);
        }
    }
}
