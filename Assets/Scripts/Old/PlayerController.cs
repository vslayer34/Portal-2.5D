using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    PlayerInputAction playerInput;
    Rigidbody rb;
    PlayerAnimations animationsScript;

    // movement
    float movementinput;
    float speed = 10.0f;
    float jumpForce = 400.0f;

    // mouse
    //Vector3 mouseScreenPosition;
    [SerializeField] Transform crosshair;
    Camera mainCamera;

    void Awake()
    {
        playerInput = new PlayerInputAction();
    }

    void OnEnable()
    {
        playerInput.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        animationsScript = GetComponentInChildren<PlayerAnimations>();
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        playerInput.Player.Jump.performed += Jump;
    }

    // Update is called once per frame
    void Update()
    {
        movementinput = playerInput.Player.Movement.ReadValue<float>();
        MoveMouseCursor();
    }

    void FixedUpdate()
    {
        MovePlayer();
        SetPlayerFacing();
    }

    void Jump(CallbackContext context)
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void SetPlayerFacing()
    {
        //float angle = Mathf.Atan2(transform.position.y - crosshair.position.y, transform.position.x - crosshair.position.x) * Mathf.Rad2Deg;
        float angle = Mathf.Atan2(2, 1) * Mathf.Rad2Deg;

        Debug.Log(angle);
    }

    void MovePlayer()
    {
        Vector3 movementDirection = new Vector3(movementinput * speed, 0.0f, 0.0f) * Time.fixedDeltaTime;
        rb.position += movementDirection;
        animationsScript.RunForwardAnimations(Mathf.Abs(movementDirection.x));
    }

    void MoveMouseCursor()
    {
        Vector3 mouseScreenPosition = playerInput.Player.Look.ReadValue<Vector2>();
        mouseScreenPosition.z = mainCamera.nearClipPlane + 12.0f;
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);

        crosshair.position = mouseWorldPosition;
    }

    void OnDisable()
    {
        playerInput?.Disable();
        playerInput.Player.Jump.performed -= Jump;
    }
}
