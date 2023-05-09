using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyUp : MonoBehaviour, IAbility
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

        if (uses > 0 && (parent.GetComponent<PlayerCombat>().acc == false))
        {
            parent.GetComponent<PlayerCombat>().acc = true;
            uses--;
            yield return new WaitForSeconds(gameObject.GetComponent<ItemPickup>().ability.duration);
            parent.GetComponent<PlayerCombat>().acc = false;
            if (uses <= 0)
                parent.GetComponent<Inventory>().Remove(parent.GetComponent<Inventory>().abilityObj);
        }
    }
}
