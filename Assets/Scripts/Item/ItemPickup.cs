
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData itemData;   // 인벤토리에 추가할 아이템 데이터

    private void Start()
    {
        // 프리팹이 지정돼 있으면 시각적으로 표시
        if (itemData.prefab != null)
        {
            GameObject model = Instantiate(itemData.prefab, transform);
            model.transform.localPosition = Vector3.zero;
        }
    }

    public void PickUp()
    {
        Inventory.Instance.AddItem(itemData);
        Debug.Log($" {itemData.itemName} 획득!");
        Destroy(gameObject);
    }
}
