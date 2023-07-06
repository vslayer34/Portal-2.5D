using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInputAction inputActions;

    /// <summary>
    /// fire an action when the jump is pressed
    /// </summary>
    public Action OnJumpPresed { get; set; }

    /// <summary>
    /// the value of the input the user entered
    /// </summary>
    public float InputValue { get; private set; }

    private void Awake()
    {
        // instantiate and enable the player controls schemezzzaaaaa
        inputActions = new PlayerInputAction();
        inputActions.Player.Enable();
        inputActions.Player.Jump.performed += Jump_performed;
    }

    private void Jump_performed(InputAction.CallbackContext obj)
    {
        OnJumpPresed();
    }

    private void Update()
    {
        // read the input every frame
        InputValue = inputActions.Player.Movement.ReadValue<float>();
    }
}
