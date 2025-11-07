
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("기본 정보")]
    public string itemName;
    public ItemType itemType;
    public float value;

    [Header("시각 요소")]
    public GameObject prefab;        // 3D 모델
    public GameObject iconPrefab;    // 아이콘 프리팹 (UI에 표시될 미니 프리팹)

    public enum ItemType
    {
        HP_Potion,
        MP_Potion,
        Weapon
    }
}
