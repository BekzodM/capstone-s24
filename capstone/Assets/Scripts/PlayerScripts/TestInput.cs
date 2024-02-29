using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestInput : MonoBehaviour
{
    [SerializeField] float speed = 50.0f;    // Player speed

    private Rigidbody playerRigidbody;
    private PlayerInput playerInput;    // PlayerInput component tied to Player object
    // Instance of the auto generated C# class from Input Actions
    // This class instance will retrieve the inputs the user makes to control the player object
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        playerInputActions = new PlayerInputActions();
        // Enable the Player action map (only Player action map)
        playerInputActions.Player.Enable();
        // Associate the callback function to Jump() for player jump
        playerInputActions.Player.Jump.performed += Jump;
        // Associate the callback function to Movement() for player movement
        playerInputActions.Player.Movement.performed += Movement;
    }

    // FixedUpdate() shakes the player instead of moving for some reason
    private void Update()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        playerRigidbody.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);

    }

    // Fixed update
    // Relative to game world time
    // Unlike Update(), which is called every frame, FixedUpdate() is called at specific intervals
    //private void FixedUpdate()
    //{
    //    Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
    //    playerRigidbody.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);

    //}

    private void Movement(InputAction.CallbackContext context)
    {
        // Retrieve the movement input vector from the context
        Vector2 inputVector = context.ReadValue<Vector2>();
        playerRigidbody.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            playerRigidbody.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);
        }
    }
}
