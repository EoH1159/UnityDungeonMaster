using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    public List<Item> items = new List<Item>();
    public int maxSlots = 8;

    [Header("Weapon Slot")]
    public Transform weaponSlot; // 플레이어 손 위치에 빈 오브젝트 만들어서 연결
    private GameObject currentWeapon;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Inventory.Instance.UseItem(0);  // 첫 번째 아이템 사용 (포션 or 무기)
    }
    public void AddItem(Item newItem)
    {
        if (items.Count < maxSlots)
        {
            items.Add(newItem);
            Debug.Log($"{newItem.itemName} 을(를) 인벤토리에 추가했습니다!");
        }
        else
        {
            Debug.Log("인벤토리가 가득 찼습니다!");
        }
    }

    public void UseItem(int index)
    {
        if (index < 0 || index >= items.Count) return;

        Item item = items[index];
        switch (item.type)
        {
            case Item.ItemType.HP_Potion:
                GameManager.Instance.ChangeHP(item.value);
                break;
            case Item.ItemType.MP_Potion:
                GameManager.Instance.ChangeMP(item.value);
                break;
            case Item.ItemType.Weapon:
                EquipWeapon(item);
                break;
        }

        Debug.Log($"{item.itemName} 을(를) 사용했습니다!");
    }

    void EquipWeapon(Item weaponItem)
    {
        // 기존 무기 제거
        if (currentWeapon != null)
            Destroy(currentWeapon);

        // 새 무기 장착
        currentWeapon = Instantiate(weaponItem.weaponPrefab, weaponSlot);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;

        Debug.Log($"🔪 {weaponItem.itemName} 장착 완료! 공격력: {weaponItem.value}");
    }
}
