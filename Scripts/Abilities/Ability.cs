using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Inventory/Ability")]
public class Ability : ScriptableObject
{
    public string itemName = "New Ability";
    public float uses = 0f;
    public float duration = 0f;
    public Sprite sprite = null;
    //ability ideas: damage bonus, extra ammo, higher jump, rapid fire
}
