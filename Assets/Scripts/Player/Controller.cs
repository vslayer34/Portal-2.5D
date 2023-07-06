using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Controller : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Rigidbody rb;

    /// <summary>
    /// player movement speed
    /// </summary>
    private float movementSpeed = 7.0f;
    /// <summary>
    /// Player jump force
    /// </summary>
    [SerializeField] private float jumpForce = 400.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputManager.OnJumpPresed = Jump;
    }


    private void FixedUpdate()
    {
        MovePosition();
    }


    /// <summary>
    /// change the postion of the rigid body to move the player
    /// </summary>
    private void MovePosition()
    {
        Vector3 moveDirection = new Vector3(0.0f, 0.0f, inputManager.InputValue) * Time.deltaTime * movementSpeed;
        rb.position += moveDirection;
    }

    /// <summary>
    /// Add force to the rigid body to jump
    /// </summary>
    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
