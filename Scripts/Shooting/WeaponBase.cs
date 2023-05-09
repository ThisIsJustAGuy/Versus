using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour, IWeapon
{
    public Transform barrel;
    public GameObject bullet;
    public ItemPickup ip;
    public bool acc;
    public int ammo { get; set; }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!ip.isPickedUp && ammo == 0)
            StartCoroutine(DesTroy());
    }
    private void Start()
    {
        ammo = gameObject.GetComponent<ItemPickup>().weapon.ammo;
    }
    public void Attack()
    {
        barrel = gameObject.transform.GetChild(0).transform;
        for (int i = 0; i < ip.weapon.bulletsAtATime; i++)
        {
            var bulletShot = Instantiate(bullet, barrel.position, barrel.rotation);
            bulletShot.gameObject.GetComponent<Bullet>().acc = gameObject.transform.parent.transform.parent.GetComponent<PlayerCombat>().acc;
            bulletShot.gameObject.GetComponent<Bullet>().originWeapon = ip.weapon; /*így adjuk meg milyen fegyverből jön, ezt figyelembe véve változtatjuk a propertyket*/
            //bulletShot.gameObject.GetComponent<Bullet>().acc = false;
        }
        ammo--;
    }
    public IEnumerator DesTroy()
    {
        yield return new WaitForSeconds(10f);
        if (!ip.isPickedUp)
            Destroy(gameObject);
    }
}
