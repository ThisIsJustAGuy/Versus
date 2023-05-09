using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]
public class Weapon : ScriptableObject
{
    public string itemName = "New Weapon";
    public int ammo = 0;
    public float attackRate = 0f;
    public float bulletSpeed = 0f;
    public float bulletDamage = 0f;
    public int punchThrough = 0;
    public float accuracyDegree = 0;
    public int bulletsAtATime = 0;
    public float bulletForce = 0f;
    public Sprite sprite = null;
}
