using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    public List<ItemData> items = new List<ItemData>();  // ✅ Item → ItemData
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
        }

        if (item.itemType != ItemData.ItemType.Weapon)
            items.RemoveAt(index);
    }

    void EquipWeapon(ItemData weaponItem)
    {
        if (currentWeapon != null)
            Destroy(currentWeapon);

        if (weaponItem.weaponPrefab != null && weaponSlot != null)
        {
            currentWeapon = Instantiate(weaponItem.weaponPrefab, weaponSlot);
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localRotation = Quaternion.identity;
        }

        Debug.Log($" {weaponItem.itemName} 장착 완료! (공격력 {weaponItem.value})");
    }
}
