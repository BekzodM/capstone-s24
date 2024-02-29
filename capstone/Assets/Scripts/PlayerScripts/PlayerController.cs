using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Player player;         // The player object this controller is controlling
    //[SerializeField] float speed = 1.0f;    // Player speed

    private Rigidbody playerRigidbody;      // Part of the player object but for quick access
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
        //// Associate the callback function to Movement() for player movement
        //playerInputActions.Player.Movement.performed += Movement;
    }

    // FixedUpdate() shakes the player instead of moving for some reason
    // It is most likely due to friction, as increasing speed to 100 and it starts moving
    // Or it can be due to the collider being a box (larger surface area contact between
    // colliders)
    // Humans have 2 legs (2 points of contact), a sphere has 1 "leg" (1 point of contact)
    private void Update()
    {

    }

    // Fixed update
    // Relative to game world time
    // Unlike Update(), which is called every frame, FixedUpdate() is called at specific intervals
    // We want the game to continuously check for movement updates, so the movement must be handled
    // in Update() or FixedUpdate() and not a callback
    private void FixedUpdate()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        //Debug.Log(inputVector);
        playerRigidbody.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * player.MoveSpeed(), ForceMode.Force);

    }

    //private void Movement(InputAction.CallbackContext context)
    //{
    //    // Retrieve the movement input vector from the context
    //    Debug.Log("Movement()");
    //    Vector2 inputVector = context.ReadValue<Vector2>();
    //    playerRigidbody.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * player.MoveSpeed(), ForceMode.Force);
    //}

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            playerRigidbody.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);
        }
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        Debug.Log(inputVector);

        return inputVector;
    }
}
