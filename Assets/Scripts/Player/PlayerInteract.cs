
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public float interactRange = 7f;
    public LayerMask interactMask;   // 감지할 레이어 (예: Item)

    public Text interactText;        // UI 텍스트 (Screen에 띄울 것)

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        interactText.gameObject.SetActive(false);
    }

    void Update()
    {
        HandleInteractionRay();
    }

    void HandleInteractionRay()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange, interactMask))
        {
            //  아이템
            if (hit.collider.CompareTag("Item"))
            {
                interactText.text = "Press [E] to pick up";
                interactText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    ItemPickup pickup = hit.collider.GetComponent<ItemPickup>();
                    if (pickup != null)
                    {
                        pickup.PickUp();  // 아이템 습득
                    }
                }
            }
            //  플랫폼 (MovingPlatform_Interact)
            else if (hit.collider.GetComponent<MovingPlatform>() != null)
            {
                interactText.text = "Press [E] to move platform";
                interactText.gameObject.SetActive(true);
            }
            else
            {
                interactText.gameObject.SetActive(false);
            }
        }
        else
        {
            interactText.gameObject.SetActive(false);
        }
    }
}
