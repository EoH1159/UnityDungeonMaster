using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Moverment")]
    public float moveSpeed = 5f;
    public float jumpForce;
    private Vector2 curMovementInput;
    public LayerMask groundLayerMask;
    
    // 이동할 때는 항상 이 값을 사용하도록(예: moveSpeedCurrent로 사용)
    private float _speedMultiplier = 1f;
    public float CurrentMoveSpeed => moveSpeed * _speedMultiplier;


    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float curXLookRotation;
    public float lookSensitivity;
    private Vector2 mouseDelta;

    [Header("참조 연결")]
    [SerializeField] private InventoryUI inventoryUI; // 인벤토리 UI 연결용


    private Rigidbody _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

    }
    // Start is called before the first frame update
    void Start()
    {
       Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 마우스 입력 초기화 (시작 시 시점이 튀는 현상 방지)
        mouseDelta = Vector2.zero;
        curXLookRotation = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }
    private void LateUpdate()
    {
        CameraLook();
    }

    void Move()
    {
        //플레이어의 이동값을 받아와서 이동
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= CurrentMoveSpeed;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
    }

    void CameraLook()
    {
        // 마우스 입력은 속도, Rigidbody 등과 관계없이 항상 고정
        Vector2 rawMouse = new Vector2(
            Input.GetAxis("Mouse X"),
            Input.GetAxis("Mouse Y")
        );

        // 오직 lookSensitivity와 deltaTime만 사용
        curXLookRotation += rawMouse.y * lookSensitivity;
        curXLookRotation = Mathf.Clamp(curXLookRotation, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-curXLookRotation, 0f, 0f);

        // Y축(좌우) 회전
        transform.eulerAngles += new Vector3(0f, rawMouse.x * lookSensitivity, 0f);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
                       curMovementInput = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
            //점프 기능 구현
        }
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.5f) + (transform.up * -0.12f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.5f) + (transform.up * -0.12f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.5f) + (transform.up * -0.12f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.5f) + (transform.up * -0.12f), Vector3.down)
        };
        

        for (int i =0;i < rays.Length; i++)
        {
            Debug.DrawRay(rays[i].origin, rays[i].direction * 0.1f, Color.red, 0.1f);
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }

    // --- 기존 Move / Look / Jump 함수 아래에 추가 ---
    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // 인벤토리 UI가 존재할 경우 열기/닫기 전환
            if (inventoryUI == null)
            {
                Debug.LogError(" PlayerController: inventoryUI가 연결되지 않았습니다!");
                return;
            }
            inventoryUI.ToggleInventory();

            //  인벤토리 상태에 따라 커서/시점 제어
            bool isOpen = inventoryUI.IsOpen; // 인벤토리UI에 공개 변수 추가 (아래 참고)

            if (isOpen)
            {
                Cursor.lockState = CursorLockMode.None; // 마우스 잠금 해제
                Cursor.visible = true;                  // 커서 보이기
                lookSensitivity = 0f;                   // 시점 회전 잠금 (필요 시)
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked; // 마우스 잠금
                Cursor.visible = false;                   // 커서 숨기기
                lookSensitivity = 1f;                     // 시점 회전 복귀
            }
        }
    }

    Coroutine speedCo;

    public void ApplySpeedBoost(float multiplier, float duration)
    {
        if (speedCo != null) StopCoroutine(speedCo);
        speedCo = StartCoroutine(SpeedBoostRoutine(multiplier, duration));
    }

    IEnumerator SpeedBoostRoutine(float multiplier, float duration)
    {
        _speedMultiplier = multiplier;   // 예: 1.5배
        yield return new WaitForSeconds(duration);
        _speedMultiplier = 1f;           // 원상복구
        speedCo = null;
    }
}
