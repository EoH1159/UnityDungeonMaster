
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData itemData;   // 인벤토리에 추가할 아이템 데이터

    public void PickUp()
    {
        Inventory.Instance.AddItem(itemData);
        Debug.Log($" {itemData.itemName} 획득!");
        Destroy(gameObject);
    }
}
