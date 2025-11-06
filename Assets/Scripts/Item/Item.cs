using UnityEngine;
[System.Serializable]

public class Item
{
    public string itemName;         // 이름
    public ItemType type;           // 종류
    public float value;             // 회복량 or 공격력
    public GameObject weaponPrefab; // 무기일 경우 손에 붙일 프리팹

    public enum ItemType
    {
        HP_Potion,
        MP_Potion,
        Weapon
    }
}
