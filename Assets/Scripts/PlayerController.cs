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
        Vector3 mouseScreenPosition = playerInput.Player.Look.ReadValue<Vector2>();
        mouseScreenPosition.z = transform.position.z;
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);

        crosshair.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, transform.position.z);
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void Jump(CallbackContext context)
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void MovePlayer()
    {
        Vector3 movementDirection = new Vector3(movementinput * speed, 0.0f, 0.0f) * Time.fixedDeltaTime;
        rb.position += movementDirection;
        animationsScript.RunForwardAnimations(Mathf.Abs(movementDirection.x));
    }

    void OnDisable()
    {
        playerInput?.Disable();
        playerInput.Player.Jump.performed -= Jump;
    }
}
