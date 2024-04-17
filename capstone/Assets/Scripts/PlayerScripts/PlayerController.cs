//using System;
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using System.Runtime.Serialization;
using System;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;

public class PlayerController : MonoBehaviour
{
    // Instance of the auto generated C# class from Input Actions
    // This class instance will retrieve the inputs the user makes to control the player object
    private PlayerInputActions playerInputActions;
    private InputAction movement;
    private InputAction mapViewMovement;

    //private PlayerInput playerInput;

    private Animator animator;


    private Rigidbody rb;
    [SerializeField] private float movementForce = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] float rotationSpeed = 1.0f;    /* 1.0f is instant rotation, also ensures bullet fires after rotation, else rigidbody of player blocks*/
    private Vector3 forceDirection = Vector3.zero;

    // Current active camera (main camera)
    // We want our motion to be relative to camera position
    private Camera playerCamera;

    [SerializeField] private CinemachineVirtualCamera thirdPersonCamera;
    [SerializeField] private CinemachineVirtualCamera aimCamera;
    //[SerializeField] private CinemachineVirtualCamera mapViewCamera;
    private bool aimCameraActive = false;

    //[SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject gun;

    private bool isShooting = false;        // Player bool input
    private bool isAiming = false;



    private GunSystem gunSystem;

    private enum ActiveActionMap
    {
        Player,
        MapView
    }
    private ActiveActionMap activeActionMap;    // Flag to signal which action maps to switch to


    // Needs to be Awake() so values can be initialized before the OnEnable() & OnDisable() calls
    // (before accessing their methods)
    private void Awake()
    {
        playerCamera = Camera.main;
        animator = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();
        //playerInput = GetComponent<PlayerInput>();
        //movement = playerInput.actions["Movement"];
        //jump = playerInput.actions["Jump"];
        ////look = playerInput.actions["Look"];
        //aim = playerInput.actions["Aim"];
        playerInputActions = new PlayerInputActions();

        gunSystem = gun.GetComponent<GunSystem>(); // Get Gun Script component from gun gameobject
    }

    private void Start()
    {
        //aimCamera.gameObject.SetActive(aimCameraActive);

        // Aim camera has higher priority, set active to false to use normal 3rd person camera as
        // default
        //aimCamera.gameObject.SetActive(false);
        aimCamera.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        //thirdPersonCamera.gameObject.transform.GetChild(0).gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        // Ensure when planning phase -> battle phase
        // Cursor is locked and invisible again
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        activeActionMap = ActiveActionMap.Player;

        // NOTE: For some reason, the right mouse button never triggers "started" or "canceled" events
        // But they are subscribed to their respective functions just in case
        // "performed" is currently triggered twice (on-click and on-release of right mouse button)
        playerInputActions.Player.Aim.performed += Aim;
        playerInputActions.Player.Shoot.performed += _ => OnShoot();
        playerInputActions.Player.Reload.performed += _ => OnReload();
        playerInputActions.Player.Jump.performed += Jump;

        //playerInputActions.Player.SwitchToMapActions.performed += SwitchToMapActions;

        movement = playerInputActions.Player.Movement;  // Get ref to player "Movement" action from PlayerInputActions
        mapViewMovement = playerInputActions.MapView.Direction; // Get ref to battlefield view movement from PlayerInputActions

        playerInputActions.Player.Enable();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        playerInputActions.Player.Aim.performed -= Aim;
        playerInputActions.Player.Shoot.performed -= _ => OnShoot();
        playerInputActions.Player.Reload.performed -= _ => OnReload();
        playerInputActions.Player.Jump.performed -= Jump;

        //playerInputActions.Player.SwitchToMapActions.performed -= SwitchToMapActions;

        playerInputActions.Player.Disable();
    }

    public void ToggleCursorUnlocked()
    {
        // Toggle cursor lock state between locked and confined to the window
        Cursor.lockState = CursorLockMode.None;

        // Toggle cursor visibility
        Cursor.visible = true;
    }

    private void FixedUpdate()
    {
        if (movement.ReadValue<Vector2>() != Vector2.zero)
        {
            animator.SetBool("Is_Walking", true);
        }
        else
        {
            animator.SetBool("Is_Walking", false);
        }
        // Player movement
        if (activeActionMap == ActiveActionMap.Player)
        {
            // Get normalized (horizontal plane) directional XZ values and multiply by the force of movement
            forceDirection += movement.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * movementForce;
            forceDirection += movement.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * movementForce;

            // Update force input
            // y value comes from Jump() callback
            rb.AddForce(forceDirection, ForceMode.Impulse);
            // Helps player come to a full stop after releasing the key instead of continuing to accelerate
            forceDirection = Vector3.zero;
        }
        // Top down camera movement
        else
        {
            Vector3 direction = mapViewMovement.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * movementForce;
            //topDownLookAt.transform.Translate(new Vector3(playerCamera.transform.position.x + direction.x, playerCamera.transform.position.y, playerCamera.transform.position.z + direction.z));
            //mapViewCamera.gameObject.transform.GetChild(0).gameObject.transform = new Vector3(playerCamera.transform.position.x + direction.x, playerCamera.transform.y, playerCamera.transform.position.z + direction.z);
        }

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

    //private void Update()
    //{
    //    //LookAt();
    //}

    // Control direction the player object is looking (rotation)
    private void LookAt()
    {
        // Look in the direction of the velocity
        // movement direction
        Vector3 moveDirection = rb.velocity;
        moveDirection.y = 0f;

        // aim rotation has priority over move rotation
        if (isAiming || isShooting)
        {
            // aim & shoot direction
            Quaternion targetRotation = Quaternion.Euler(0f, playerCamera.transform.eulerAngles.y, 0f);
            this.rb.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);
        }
        // If there is player input and the player is moving, allow player object rotation;
        // else stop player rotation
        else if (movement.ReadValue<Vector2>().sqrMagnitude > 0.1f && moveDirection.sqrMagnitude > 0.1f)
            this.rb.rotation = Quaternion.LookRotation(moveDirection, Vector3.up);
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
    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0f;
        return right.normalized;
    }

    // Jump callback function
    private void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump");
        Debug.Log(context.performed);
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

    // Triggered twice:
    //      on-click right mouse button
    //      on-release right mouse button
    //
    // Theories:
    //      The right mouse click is implemented in a way in which the on-click & on-release
    //      only triggers "performed" event
    //      Once on-click & once on-release of the button
    private void Aim(InputAction.CallbackContext context)
    {
        Debug.Log(context.performed);
        if (context.performed)
        {
            isAiming = !isAiming;
            aimCameraActive = !aimCameraActive;
            //aimCamera.gameObject.SetActive(aimCameraActive);

            //thirdPersonCamera.gameObject.SetActive(!aimCameraActive);

            if (aimCameraActive)
            {
                aimCamera.Priority += 10;
                aimCamera.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                // Uses index 1 instead of 0 for some reason even though it only has 1 child
                thirdPersonCamera.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                aimCamera.Priority -= 10;
                aimCamera.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                // Uses index 1 instead of 0 for some reason even though it only has 1 child
                thirdPersonCamera.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }



    private void OnShoot()
    {
        /* isShooting bool value accessed here, reloading bool value will be accessed through the gun gameobject */
        //isShooting = !isShooting;

        /*
         * NEED TO have an isShooting bool for gun, make a SETTER!
         */

        // redundant to if statement below in the Shoot() function
        //bulletsShot = bulletsPerTap;

        // rotate
        isShooting = !isShooting; // for rotation

        gunSystem.Shoot();

    }



    //// Player input callback
    private void OnReload()
    {
        //if (bulletsLeft < magazineSize && !reloading)
        gunSystem.OnReload();
    }

    // "m"
    //private void SwitchToMapActions(InputAction.CallbackContext context)
    //{
    //    Debug.Log(context.performed);
    //    // Switch to MapView actions
    //    if (activeActionMap == ActiveActionMap.Player)
    //    {
    //        Debug.Log("Switch to map view actions");
    //        playerInputActions.Player.Aim.performed -= Aim;
    //        playerInputActions.Player.Shoot.performed -= _ => OnShoot();
    //        playerInputActions.Player.Reload.performed -= _ => OnReload();
    //        playerInputActions.Player.Jump.performed -= Jump;
    //        //playerInputActions.Player.SwitchToMapActions.performed -= _ => SwitchToMapActions();
    //        playerInputActions.Player.SwitchToMapActions.performed -= SwitchToMapActions;

    //        playerInputActions.Player.Disable();

    //        playerInputActions.MapView.Zoom.performed += _ => Zoom();
    //        //playerInputActions.MapView.SwitchToPlayerActions.performed += _ => SwitchToMapActions();
    //        playerInputActions.MapView.SwitchToPlayerActions.performed += SwitchToMapActions;

    //        playerInputActions.MapView.Enable();

    //        Cursor.lockState = CursorLockMode.None;
    //        Cursor.visible = true;

    //        activeActionMap = ActiveActionMap.MapView;



    //        aimCamera.gameObject.SetActive(false);
    //        thirdPersonCamera.gameObject.SetActive(false);
    //    }
    //    // Switch to Player actions
    //    else
    //    {
    //        Debug.Log("Switch to player actions");
    //        playerInputActions.MapView.Zoom.performed -= _ => Zoom();
    //        //playerInputActions.MapView.SwitchToPlayerActions.performed -= _ => SwitchToMapActions();
    //        playerInputActions.MapView.SwitchToPlayerActions.performed -= SwitchToMapActions;

    //        playerInputActions.MapView.Disable();

    //        playerInputActions.Player.Aim.performed += Aim;
    //        playerInputActions.Player.Shoot.performed += _ => OnShoot();
    //        playerInputActions.Player.Reload.performed += _ => OnReload();
    //        playerInputActions.Player.Jump.performed += Jump;
    //        //playerInputActions.Player.SwitchToMapActions.performed += _ => SwitchToMapActions();
    //        playerInputActions.Player.SwitchToMapActions.performed += SwitchToMapActions;

    //        //movement = playerInputActions.Player.Movement;  // Get ref to "Movement" action from PlayerInputActions

    //        playerInputActions.Player.Enable();

    //        Cursor.lockState = CursorLockMode.Locked;
    //        Cursor.visible = false;

    //        activeActionMap = ActiveActionMap.Player;



    //        aimCamera.gameObject.SetActive(true);
    //        thirdPersonCamera.gameObject.SetActive(true);
    //    }
    //}

    private void Zoom()
    {
        //playerCamera.transform = Vector3(playerCamera.transform.x, playerCamera.transform + SOMETHING, playerCamera.transform.z);
    }
}
