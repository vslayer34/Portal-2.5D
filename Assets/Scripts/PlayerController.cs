using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    PlayerInputAction playerInput;
    Rigidbody rb;
    PlayerAnimations animationsScript;

    float movementinput;
    float speed = 10.0f;

    float jumpForce = 400.0f;

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
        playerInput.Player.Jump.performed += Jump;
    }

    // Update is called once per frame
    void Update()
    {
        movementinput = playerInput.Player.Movement.ReadValue<float>();
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
