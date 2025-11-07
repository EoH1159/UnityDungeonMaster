
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public float detectRange = 7f;
    public LayerMask itemMask;   // 감지할 레이어 (예: Item)

    [Header("UI 표시")]
    public Text pickupText;

    private ItemPickup targetItem;  // 감지된 아이템

    // Update is called once per frame
    void Update()
    {
        DetectItem();
    }

    void DetectItem()
    {
        Camera cam = Camera.main;
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, detectRange, itemMask))
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green);
            targetItem = hit.collider.GetComponent<ItemPickup>();

            if (targetItem != null)
            {
                pickupText.text = $"[E] {targetItem.itemData.itemName} 줍기";
                pickupText.enabled = true;
            }
            else
            {
                pickupText.enabled = false;
            }
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * detectRange, Color.red);
            targetItem = null;
            pickupText.enabled = false;
        }
    }   
    

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && targetItem != null)
        {
            
            Debug.Log($" {targetItem.itemData.itemName} 획득!");
            targetItem.PickUp();
            pickupText.enabled = false;
            targetItem = null;
        }
    }
}
