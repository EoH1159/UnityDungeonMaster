using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    [Header("인벤토리 리스트 (획득 순서 유지)")]
    public List<ItemData> items = new List<ItemData>();

    [Header("무기 장착 위치")]
    public Transform weaponSlot;
    private GameObject currentWeapon;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddItem(ItemData newItem)   //  Item → ItemData
    {
        items.Add(newItem);
        Debug.Log($"{newItem.itemName}을(를) 인벤토리에 추가했습니다!");
    }

    public void UseItem(int index)
    {
        if (index < 0 || index >= items.Count) return;

        ItemData item = items[index];   //  Item → ItemData

        switch (item.itemType)          //  itemType은 이제 ItemData 안에 정의되어 있음
        {
            case ItemData.ItemType.HP_Potion:
                GameManager.Instance.ChangeHP(item.value);
                break;

            case ItemData.ItemType.MP_Potion:
                GameManager.Instance.ChangeMP(item.value);
                break;

            case ItemData.ItemType.Weapon:
                EquipWeapon(item);
                break;
            case ItemData.ItemType.SpeedPotion:     // 추가
                var pc = FindObjectOfType<PlayerController>(); // 또는 캐시해둔 참조
                if (pc != null) pc.ApplySpeedBoost(item.value, item.duration);
                Debug.Log($"스피드 포션 사용: x{item.value} {item.duration}초");
                break;
        }

        if (item.itemType != ItemData.ItemType.Weapon)
            items.RemoveAt(index);
    }

    void EquipWeapon(ItemData weaponItem)
    {
        if (currentWeapon != null)
            Destroy(currentWeapon);

        // weaponPrefab 대신 prefab 사용
        if (weaponItem.prefab != null && weaponSlot != null)
        {
            currentWeapon = Instantiate(weaponItem.prefab, weaponSlot);
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localRotation = Quaternion.identity;
        }

        Debug.Log($" {weaponItem.itemName} 장착 완료! (공격력 {weaponItem.value})");
    }

    // 아이템 정렬 예시 (원하면 수동 호출)
    public void SortItemsByType()
    {
        items.Sort((a, b) => a.itemType.CompareTo(b.itemType));
        Debug.Log(" 아이템을 타입별로 정렬했습니다!");
    }
}
