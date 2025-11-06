using UnityEngine;
[System.Serializable]

public class Item : MonoBehaviour
{
    public string itemName;
    public string description;
    public Sprite icon;

    public enum ItemType { HP_Potion, MP_Potion, Weapon }
    public ItemType type;

    // 공통 값
    public float value; // 포션이면 회복량, 무기면 공격력

    // 무기 전용 필드
    public GameObject weaponPrefab;  // 무기 모델 (씬에 장착할 오브젝트)
}
