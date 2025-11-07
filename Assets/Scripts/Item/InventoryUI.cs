using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("UI 연결")]
    public GameObject inventoryPanel;
    public Transform slotRoot;
    public Button slotButtonPrefab;

    private bool isOpen = false;

    private void Start()
    {
        inventoryPanel.SetActive(false);
    }

    public void ToggleInventory()
    {
        isOpen = !isOpen;
        inventoryPanel.SetActive(isOpen);

        if (isOpen)
            Redraw();
    }

    private void Redraw()
    {
        // 기존 슬롯 제거
        foreach (Transform child in slotRoot)
            Destroy(child.gameObject);

        // 인벤토리 아이템 그리기
        for (int i = 0; i < Inventory.Instance.items.Count; i++)
        {
            ItemData item = Inventory.Instance.items[i]; // ✅ Item → ItemData로 변경

            Button btn = Instantiate(slotButtonPrefab, slotRoot);
            btn.GetComponentInChildren<Text>().text = item.itemName;

            int index = i;
            btn.onClick.AddListener(() => Inventory.Instance.UseItem(index));
            btn.onClick.AddListener(Redraw);
        }
    }

    public bool IsOpen => isOpen;
    }
