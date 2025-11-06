
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Moverment")]
    public float moveSpeed;
    public float jumpForce;
    private Vector2 curMovementInput;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float curXLookRotation;
    public float lookSensitivity;
    private Vector2 mouseDelta;

    private Rigidbody _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
       Cursor.lockState = CursorLockMode.Locked;
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
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
    }

    void CameraLook()
    {
        curXLookRotation += mouseDelta.y * lookSensitivity;
        curXLookRotation = Mathf.Clamp(curXLookRotation, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-curXLookRotation, 0f, 0f);

        transform.eulerAngles += new Vector3(0f, mouseDelta.x * lookSensitivity, 0f);
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
}
