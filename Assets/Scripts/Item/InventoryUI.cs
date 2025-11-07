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
        foreach (Transform child in slotRoot)
            Destroy(child.gameObject);

        for (int i = 0; i < Inventory.Instance.items.Count; i++)
        {
            ItemData item = Inventory.Instance.items[i];

            Button btn = Instantiate(slotButtonPrefab, slotRoot);

            // 기존 텍스트 대신 아이콘 프리팹 표시
            if (item.iconPrefab != null)
            {
                GameObject icon = Instantiate(item.iconPrefab, btn.transform);
                icon.transform.localPosition = Vector3.zero;
            }
            else
            {
                // fallback: 이름 텍스트 표시
                btn.GetComponentInChildren<Text>().text = item.itemName;
            }

            int index = i;
            btn.onClick.AddListener(() => Inventory.Instance.UseItem(index));
            btn.onClick.AddListener(Redraw);
        }
    }

    public bool IsOpen => isOpen;
    }
