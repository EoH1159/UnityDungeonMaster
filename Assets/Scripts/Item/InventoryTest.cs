using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    [Header("테스트용 무기 프리팹들")]
    [SerializeField] private GameObject sword1;
    [SerializeField] private GameObject sword2;
    [SerializeField] private GameObject sword3;

    void Start()
    {
        // 무기 1 예시
        Item shortSword = new Item();
        shortSword.itemName = "한손검1";
        shortSword.type = Item.ItemType.Weapon;
        shortSword.weaponPrefab = sword1;
        shortSword.value = 10;

        // 무기 2 예시
        Item longSword = new Item();
        longSword.itemName = "한손검2";
        longSword.type = Item.ItemType.Weapon;
        longSword.weaponPrefab = sword2;
        longSword.value = 15;

        // 무기 3 예시
        Item heavySword = new Item();
        heavySword.itemName = "한손검3";
        heavySword.type = Item.ItemType.Weapon;
        heavySword.weaponPrefab = sword3;
        heavySword.value = 20;

        Inventory.Instance.AddItem(shortSword);
        Inventory.Instance.AddItem(longSword);
        Inventory.Instance.AddItem(heavySword);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) Inventory.Instance.UseItem(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) Inventory.Instance.UseItem(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) Inventory.Instance.UseItem(2);
    }
}
