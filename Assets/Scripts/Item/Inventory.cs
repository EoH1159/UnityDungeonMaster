using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    [Header("아이템 리스트")]
    public List<Item> items = new List<Item>();

    [Header("무기를 장착할 위치")]
    public Transform weaponSlot;  // 손 위치

    private GameObject currentWeapon; // 현재 손에 들고 있는 무기

    void Awake()
    {
        // 이 스크립트가 게임 전체에서 딱 하나만 존재하게 함
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddItem(Item newItem)
    {
        items.Add(newItem);
        Debug.Log($"{newItem.itemName}을(를) 인벤토리에 추가했습니다!");
    }

    public void UseItem(int index)
    {
        if (index < 0 || index >= items.Count) return;

        Item item = items[index];

        // 아이템 종류에 따라 다르게 작동
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

        Debug.Log($"{item.itemName}을(를) 사용했습니다!");
    }

    void EquipWeapon(Item weaponItem)
    {
        // 이미 손에 무기가 있으면 없애기
        if (currentWeapon != null)
            Destroy(currentWeapon);

        // 새 무기를 손 위치에 붙이기
        currentWeapon = Instantiate(weaponItem.weaponPrefab, weaponSlot);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;

        Debug.Log($" {weaponItem.itemName} 장착 완료!");
    }
}
