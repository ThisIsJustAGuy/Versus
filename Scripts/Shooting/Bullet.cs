using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 0;
    public float damage = 0;
    public int punchThrough = 0;
    public Rigidbody2D rb;
    public Weapon originWeapon;
    public bool acc;
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Bullets");
        speed = originWeapon.bulletSpeed;
        damage = originWeapon.bulletDamage;
        punchThrough = originWeapon.punchThrough;
        float rnd = Random.Range(-originWeapon.accuracyDegree / (acc ? 2 : 1), originWeapon.accuracyDegree / (acc ? 2 : 1));
        transform.rotation = Quaternion.Euler(0, 180 * transform.rotation.y, rnd);
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Player"))
            collision.GetComponent<PlayerCombat>().TakeDamage(damage, gameObject, originWeapon.bulletForce);
        else if (collision.GetComponent<IDestroyable>() != null)
            collision.GetComponent<IDestroyable>().TakeDamage(damage);

        if (((punchThrough <= 0 && !collision.name.Contains("Bullet")) || collision.name.Contains("Ground") || collision.name.Contains("Wall")) && !collision.name.Contains("GameBounds"))
            Destroy(gameObject);

        punchThrough--;
    }
}
