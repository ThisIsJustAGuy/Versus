using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    private Transform attackPoint;
    public LayerMask hitables;
    private Rigidbody2D RB;
    [SerializeField] private float attackRange = 0.3f;
    [SerializeField] private int damage = 5;
    public int maxHealth = 100;
    [SerializeField] private float attackRate = 0.5f;
    float nextAttackTime = 0;
    public float currentHealth;
    public bool isDead = false;
    public HealthBar healthbar;
    public Inventory inventory;
    public Transform[] spawnPoints;
    public bool acc = false;
    public int scoreTreshold = 1;

    void Start()
    {
        if (gameObject.name == "Player1")
        {
            animator = GetComponent<Animator>();
            attackPoint = GameObject.FindGameObjectWithTag("AttackPointP1").transform;
        }
        else
        {
            attackPoint = GameObject.FindGameObjectWithTag("AttackPointP2").transform;
        }
        RB = GetComponent<Rigidbody2D>();
        inventory = GetComponent<Inventory>();

        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);

        int rnd = UnityEngine.Random.Range(0, spawnPoints.Length);

        transform.position = spawnPoints[rnd].position;
        transform.rotation = spawnPoints[rnd].rotation;

    }
    void Update()
    {
        if (gameObject.name == "Player1" && !animator.GetBool("IsDead"))
        {
            if (Input.GetButton("P1_Attack"))
                Attack();
            if (Input.GetButtonDown("P1_Special"))
                Special();
        }
        else
        {
            if (Input.GetButton("P2_Attack"))
                Attack();
            if (Input.GetButtonDown("P2_Special"))
                Special();
        }
    }

    private void Special()
    {
        if (inventory.ability != null)
        {
            StartCoroutine(inventory.abilityObj.GetComponent<IAbility>().Use());
            if (inventory.onPropertyChangedCallback != null)
                inventory.onPropertyChangedCallback.Invoke();
        }
    }

    private void Attack()
    {
        if (inventory.weapon == null && Time.time >= nextAttackTime)
        {
            if (gameObject.name == "Player1")
            {
                animator.SetTrigger("TriggerAttack");
            }
            Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, hitables);
            foreach (var i in hit)
            {
                if (i.name.Contains("Player"))
                    i.GetComponent<PlayerCombat>().TakeDamage(damage, gameObject, 2f);
                else
                    i.GetComponent<IDestroyable>().TakeDamage(damage);
            }
            nextAttackTime = Time.time + attackRate;
        }
        else if (inventory.weapon != null && Time.time >= nextAttackTime && inventory.weaponObj.GetComponent<IWeapon>().ammo != 0)
        {
            inventory.weaponObj.GetComponent<IWeapon>().Attack();
            nextAttackTime = Time.time + inventory.weapon.attackRate;
            if (inventory.onPropertyChangedCallback != null)
                inventory.onPropertyChangedCallback.Invoke();
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void TakeDamage(float damage, GameObject damager, float force)
    {
        if (currentHealth > 0)
        {
            if (damager.transform.position.x > gameObject.transform.position.x)
                RB.AddForce(new Vector2(-force, force), ForceMode2D.Impulse);
            else
                RB.AddForce(new Vector2(force, force), ForceMode2D.Impulse);
            if (gameObject.name == "Player1")
                animator.SetTrigger("Hurt");
            currentHealth -= damage;
            healthbar.SetHealth(currentHealth);
            if (currentHealth <= 0)
                StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        if (gameObject.name == "Player1")
            animator.SetBool("IsDead", true);

        if (inventory.weaponObj != null)
            inventory.Remove(inventory.weaponObj);
        if (inventory.abilityObj != null)
            inventory.Remove(inventory.abilityObj);

        RB.freezeRotation = false;
        isDead = true;
        yield return new WaitForSeconds(3f);

        int rnd = UnityEngine.Random.Range(0, spawnPoints.Length);

        transform.position = spawnPoints[rnd].position;
        transform.rotation = spawnPoints[rnd].rotation;

        currentHealth = maxHealth;
        healthbar.SetHealth(currentHealth);

        if (gameObject.name == "Player1")
            animator.SetBool("IsDead", false);

        isDead = false;

        RB.freezeRotation = true;
    }
}
