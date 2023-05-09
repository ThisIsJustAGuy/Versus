using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploding : MonoBehaviour, IDestroyable
{
    public float health = 40;
    public int damageDeal = 45;
    public int attackRange = 1;
    private LayerMask hitables;
    private void Start()
    {
        hitables = LayerMask.GetMask("Player1", "Player2");
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
            Explode();
    }
    void Explode()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, attackRange, hitables);
        foreach (var i in hit)
        {
            if (i.name.Contains("Player"))
                i.GetComponent<PlayerCombat>().TakeDamage(damageDeal, gameObject, 5f);
            else if (i.GetComponent<IDestroyable>() != null)
                i.GetComponent<IDestroyable>().TakeDamage(damageDeal);
        }
        Destroy(gameObject);
    }
}
