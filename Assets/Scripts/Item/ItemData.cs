
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("기본 정보")]
    public string itemName;
    public ItemType itemType;
    public float value;
    public float duration = 5f;  // ▶︎ 버프 지속시간(초)

    [Header("시각 요소")]
    public GameObject prefab;        // 3D 모델
    

    public enum ItemType
    {
        HP_Potion,
        MP_Potion,
        Weapon,
        SpeedPotion
    }
}
