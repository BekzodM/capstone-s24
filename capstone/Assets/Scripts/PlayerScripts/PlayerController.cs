using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Instance of the auto generated C# class from Input Actions
    // This class instance will retrieve the inputs the user makes to control the player object
    private PlayerInputActions playerInputActions;
    private InputAction movement;

    private Rigidbody rb;
    [SerializeField] private float movementForce = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float maxSpeed = 5f;
    private Vector3 forceDirection = Vector3.zero;

    // We want our motion to be relative to camera position
    [SerializeField] private Camera playerCamera;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerInputActions.Player.Jump.performed += Jump; // Subscribe to Jump() method
        movement = playerInputActions.Player.Movement;  // Get ref to "Movement" action from PlayerInputActions
        playerInputActions.Player.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Player.Jump.performed -= Jump;
        playerInputActions.Player.Disable();
    }

    private void FixedUpdate()
    {
        // Get normalized (horizontal plane) directional XZ values and multiply by the force of movement
        forceDirection += movement.ReadValue<Vector2>().x * GetCameraRght(playerCamera) * movementForce;
        forceDirection += movement.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * movementForce;

        // Update force input
        // y value comes from Jump() callback
        rb.AddForce(forceDirection, ForceMode.Impulse);
        // Helps player come to a full stop after releasing the key instead of continuing to accelerate
        forceDirection = Vector3.zero;

        // Remove falling "floaty-ness" during fall by increasing the acceleration
        // AKA make the character fall faster
        if (rb.velocity.y < 0f)
        {
            rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;
        }

        // Cap the horizontal speed
        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0.0f;
        // If exceed horizontal velocity, set velocity to our max speed
        // Squaring approximation to avoid square rooting (for performance)
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;
        }

        LookAt();
    }

    // Control direction the player object is looking (rotation)
    private void LookAt()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0f;
        // If there is player input and the player is moving, allow player object rotation;
        // else stop player rotation
        if (movement.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
            this.rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        else
            rb.angularVelocity = Vector3.zero;
    }

    /* NOTE
     * 
     * The camera can be tilted upwards/downwards & even side to side
     * So camera's transform.forward & transform.right are not in the horizontal plane
     * We want to be able to move our player in that horizontal plane
     * 
     * The 2 functions below will take the projection of the camera's forward & right
     * and project them onto the horizontal plane
     */

    // Project camera forward onto a horizontal plane
    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0f;
        return forward.normalized;
    }

    // Project camera right onto a horizontal plane
    private Vector3 GetCameraRght(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0f;
        return right.normalized;
    }

    // Jump callback function
    private void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            forceDirection += Vector3.up * jumpForce;
        }
    }

    // Determine if player is grounded
    private bool IsGrounded()
    {
        // Use ray casting to determine if grounded
        // Origin is slightly above whatever object your player is sitting on
        // Direction of ray cast is down
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.25f, Vector3.down);
        // If the ray hits something (the ground) return true; else false
        if (Physics.Raycast(ray, out RaycastHit hit, 1.27f))    // Tune this magic number for precision
            return true;
        else
            return false;
    }
}
