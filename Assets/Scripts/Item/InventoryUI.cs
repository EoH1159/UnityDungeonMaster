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
        {
            Inventory.Instance.SortItemsByType();
            Redraw();
        }
    }

    private void Redraw()
    {
        // 기존 슬롯 제거
        foreach (Transform child in slotRoot)
            Destroy(child.gameObject);

        // 아이템 리스트 순서대로 슬롯 생성
        for (int i = 0; i < Inventory.Instance.items.Count; i++)
        {
            ItemData item = Inventory.Instance.items[i];

            Button btn = Instantiate(slotButtonPrefab, slotRoot);

            // 슬롯 버튼 내부의 텍스트를 아이템 이름으로 변경
            Text label = btn.GetComponentInChildren<Text>();
            if(label != null)
{
                label.text = item.itemName;
                Debug.Log($"텍스트 설정됨: {label.text}");
            }
            else
            {
                Debug.LogWarning($"⚠️ {btn.name} 안에 Text 컴포넌트가 없습니다!");
            }

            int index = i;
            btn.onClick.AddListener(() => Inventory.Instance.UseItem(index));
            btn.onClick.AddListener(Redraw);
        }

    }

    public bool IsOpen => isOpen;
    }
