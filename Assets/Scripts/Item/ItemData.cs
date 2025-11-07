
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public ItemType itemType;
    public float value; // 포션이면 회복량, 무기면 공격력 등
    public GameObject weaponPrefab; // 무기라면 프리팹 연결

    public enum ItemType
    {
        HP_Potion,
        MP_Potion,
        Weapon
    }
}
