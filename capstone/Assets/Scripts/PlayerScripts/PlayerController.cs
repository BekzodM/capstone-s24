//using System;
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using System.Runtime.Serialization;
using System;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    // Instance of the auto generated C# class from Input Actions
    // This class instance will retrieve the inputs the user makes to control the player object
    private PlayerInputActions playerInputActions;
    private InputAction movement;
    //private InputAction jump;
    ////private InputAction look;
    //private InputAction aim;

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
    [SerializeField] private Camera playerCamera;

    [SerializeField] private CinemachineVirtualCamera thirdPersonCamera;
    [SerializeField] private CinemachineVirtualCamera aimCamera;
    private bool aimCameraActive = false;

    //[SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject gun;

    private bool isShooting = false;        // Player bool input
    private bool isAiming = false;
    //private bool isAiming = false;            // To determine how accurate the spread is
    //private bool readyToShoot = true;
    //private bool reloading = false;
    //private bool allowButtonHold = true;    // Allow for player to hold down and continuously shoot
    //private bool shouldSpread = false;      // Indicates WHEN to spread
    //private bool allowSpread = false;       // Determines IF the weapon can spread

    //private int damage = 10;
    //private int magazineSize = 100;
    //private int bulletsLeft = 100;
    //private int bulletsPerTap = 10;  // How many bullets to shoot out
    //private int burstRounds = 3;    // How many shots to shoot consecutively after one click
    //private int bulletsShot = 0;    // The amount of bullets fired consecutively per click (counter)

    //private float timeBetweenShooting = 1.1f;   // Time between shots being reset & player input clicks (fire rate)
    //private float timeBetweenShots = .1f;       // Time between consecutive shots (per click)  // 0f for shotgun, > 0f for burst
    //private float reloadTime = 2f;
    //private float spread = .02f;
    //private float steadyAimTime = .5f;  // The time it takes after the first shot/click to steady the gun
    ///* gun type will determine allow to hold*/

    //// All instantiated bullets will belong to this parent to make it so the hierarchy doesn't get messy
    //[SerializeField] private Transform bulletParent;
    //[SerializeField] private float bulletMissDistance;

    private GunSystem gunSystem;

    //[SerializeField] public int test;
    private enum GunType
    {
        [EnumMember(Value = "SemiAuto")] SemiAuto,      // Pistols, Snipers, Certain rifles
        [EnumMember(Value = "Burst")] Burst,            // Certain assault rifles (e.g. 3 round burst each trigger press)
        [EnumMember(Value = "Auto")] Auto,              // Fully automatic assult rifles/Machine guns
        [EnumMember(Value = "Multishot")] Multishot,    // Shotguns
    }
    [SerializeField] private GunType gunType;
    /* semi auto, burst, auto, multishot */

    // Needs to be Awake() so values can be initialized before the OnEnable() & OnDisable() calls
    // (before accessing their methods)
    private void Awake()
    {
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
        // NOTE: For some reason, the right mouse button never triggers "started" or "canceled" events
        // But they are subscribed to their respective functions just in case
        // "performed" is currently triggered twice (on-click and on-release of right mouse button)
        //playerInputActions.Player.Aim.started += AimStart;
        playerInputActions.Player.Aim.performed += Aim;
        //playerInputActions.Player.Aim.canceled += AimCancel;
        playerInputActions.Player.Shoot.performed += _ => OnShoot();
        playerInputActions.Player.Reload.performed += _ => OnReload();
        playerInputActions.Player.Jump.performed += Jump; // Subscribe to Jump() method
        movement = playerInputActions.Player.Movement;  // Get ref to "Movement" action from PlayerInputActions
        playerInputActions.Player.Enable();
    }

    private void OnDisable()
    {
        //playerInputActions.Player.Aim.started -= AimStart;
        playerInputActions.Player.Aim.performed -= Aim;
        //playerInputActions.Player.Aim.canceled -= AimCancel;
        playerInputActions.Player.Shoot.performed += _ => OnShoot();
        playerInputActions.Player.Reload.performed += _ => OnReload();
        playerInputActions.Player.Jump.performed -= Jump;
        playerInputActions.Player.Disable();
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
        //Debug.Log(aim.ReadValue<float>());
        //if (aim.ReadValue<bool>())
        //    aimCamera.gameObject.SetActive(true);
        //else
        //    aimCamera.gameObject.SetActive(false);

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
    private Vector3 GetCameraRght(Camera playerCamera)
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

    //// Never used
    //private void AimStart(InputAction.CallbackContext context)
    //{
    //    Debug.Log("aim start");
    //    if (context.started)
    //    {
    //        aimCamera.gameObject.SetActive(true);
    //    }
    //}

    //// Never used
    //private void AimCancel(InputAction.CallbackContext context)
    //{
    //    Debug.Log("aim cancel");
    //    if (context.canceled)
    //    {
    //        aimCamera.gameObject.SetActive(false);
    //    }
    //}

    //private void Update()
    //{
    //    if (isShooting)
    //    {
    //        // rotate

    //        // shoot
    //        ShootMid();
    //    }
    //}

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

    // THE GUN CLASS METHOD STARTS HERE FOR SHOOT
    //private void ShootMid()
    //{
    //    bulletsShot = bulletsPerTap;
    //    Shoot();
    //    //Shoot();
    //}

    //private void Shoot()
    //{
    //    Debug.Log(bulletsLeft);

    //    //if (isShooting && readyToShoot && !reloading && bulletsLeft > 0)
    //    //{
    //    readyToShoot = false;
    //    bulletsLeft--;
    //    bulletsShot--;

    //    // Bullet instantiate

    //    InstantiateBullet();


    //    Invoke(nameof(ResetShot), timeBetweenShooting);

    //    //if (allowButtonHold)    // should switch to auto/semiauto enum
    //    //{
    //    /* MIGHT BE ABLE TO SWITCH BACK for fully auto*/
    //    if (bulletsShot > 0 && bulletsLeft > 0)
    //        Invoke(nameof(Shoot), timeBetweenShots);    // If held down shoot button

    //    if (allowSpread)
    //    {
    //        shouldSpread = true;

    //        Invoke(nameof(SteadyAim), steadyAimTime);
    //    }
    //    //}

    //    //shouldSpread = true;

    //    //if (bulletsShot > 0 && bulletsLeft > 0)
    //    //{
    //    //    shouldSpread = true;
    //    //    Debug.Log("test");
    //    //    float time = timeBetweenShots;
    //    //    for (int i = 1; i < bulletsPerTap; i++)
    //    //    {
    //    //        Invoke(nameof(InstantiateBullet), timeBetweenShots);
    //    //        time += timeBetweenShots;
    //    //    }
    //    //    Invoke(nameof(SteadyAim), steadyAimTime);
    //    //}

    //    //}
    //}





    //private void Shoot()
    //{
    //    Debug.Log(bulletsLeft);

    //    if (isShooting && readyToShoot && !reloading && bulletsLeft > 0)
    //    {
    //        readyToShoot = false;
    //        bulletsLeft--;

    //        // Bullet instantiate

    //        //if (gunType == GunType.SemiAuto || gunType == GunType.Auto)
    //        // auto vs semi is actually determined by allowButtonHold
    //        //{
    //        InstantiateBullet();
    //        //}
    //        // handling multiple bullets (SHOTGUNS & BURST RIFLES)
    //        if (gunType == GunType.Burst || gunType == GunType.Multishot)
    //        {
    //            //InstantiateBullet(); // Shoot first bullet
    //            /* 
    //             * NOTE: can divide timeBetweenShooting by bulletsPerTap instead to avoid wording similarity confusion (but uses division)
    //             */
    //            shouldSpread = true;
    //            float timeGap = timeBetweenShots;
    //            for (int i = 1; i < bulletsPerTap; i++)
    //            {
    //                ///* Spread */
    //                //Vector3 direction = playerCamera.transform.forward;
    //                //if (shouldSpread || gunType == GunType.Shotgun)  // if consecutive shot or a shotgun, spread
    //                //{
    //                //    float x = UnityEngine.Random.Range(-spread, spread);
    //                //    float y = UnityEngine.Random.Range(-spread, spread);
    //                //    direction = playerCamera.transform.forward + new Vector3(x, y, 0);
    //                //}

    //                ////  Actual bullet instantiate

    //                //RaycastHit hit;
    //                //// 2nd arg: position + forward for the position in front of the gun
    //                //// Bullet direction should face where you are aiming
    //                //GameObject bullet = GameObject.Instantiate(bulletPrefab, gun.transform.position + gun.transform.forward, Quaternion.identity, bulletParent);
    //                //BulletController bulletController = bullet.GetComponent<BulletController>();

    //                //// Can specify a 5th arg for a layermask (i.e. if ray hits an "enemy" layermask)
    //                //// Has to not hit player, so enemy layermask only? and if needed a layermask for terrain?
    //                //if (Physics.Raycast(playerCamera.transform.position, direction, out hit, Mathf.Infinity))
    //                //{
    //                //    bulletController.target = hit.point;
    //                //    bulletController.hit = true;
    //                //}
    //                //else
    //                //{
    //                //    // If no target, shoot direction is based off of the position of center of the screen
    //                //    //bulletController.target = playerCamera.transform.position + playerCamera.transform.forward * bulletMissDistance;
    //                //    bulletController.target = playerCamera.transform.position + direction * bulletMissDistance;
    //                //    bulletController.hit = false;

    //                //}



    //                Invoke(nameof(InstantiateBullet), timeGap);

    //                if (gunType == GunType.Burst)   // only shotguns multiple shots count as 1 shot used
    //                    bulletsLeft--;

    //                timeGap += timeBetweenShots;
    //                //Debug.Log("burst");
    //                //Invoke(nameof(SteadyAim), steadyAimTime);
    //            }

    //            Invoke(nameof(SteadyAim), steadyAimTime);
    //        }
    //        //if (gunType == GunType.Multishot)
    //        //{
    //        //    for (int i = 0; i < bulletsPerTap; i++)
    //        //        InstantiateBullet();
    //        //    Debug.Log("multi");
    //        //}


    //        Invoke(nameof(ResetShot), timeBetweenShooting);

    //        // AUTOMATIC RIFLE
    //        if (allowButtonHold)    // should switch to auto/semiauto enum
    //        {
    //            /* MIGHT BE ABLE TO SWITCH BACK for fully auto*/
    //            Invoke(nameof(Shoot), timeBetweenShooting);    // If held down shoot button

    //            shouldSpread = true;    // only spread if for held down weapons

    //            Invoke(nameof(SteadyAim), steadyAimTime);
    //        }
    //    }
    //}

    //private void ResetShot()
    //{
    //    readyToShoot = true;
    //}

    //// Player input callback
    private void OnReload()
    {
        //if (bulletsLeft < magazineSize && !reloading)
        gunSystem.OnReload();
    }

    //// Actual reload logic
    //private void Reload()
    //{

    //    reloading = true;
    //    Invoke(nameof(ReloadFinished), reloadTime);
    //}

    //private void ReloadFinished()
    //{
    //    bulletsLeft = magazineSize;
    //    reloading = false;
    //}

    //private void SteadyAim()
    //{
    //    //if (!isShooting)
    //    //{

    //    // steady aim time MUST BE LONGER than BURST timeBetweenShots * shotsPerTap
    //    shouldSpread = false;
    //    Debug.Log("steady");
    //    //}
    //}

    //private void InstantiateBullet()
    //{
    //    Vector3 direction = playerCamera.transform.forward;
    //    //if (shouldSpread || gunType == GunType.Multishot)  // if consecutive shot or a shotgun, spread
    //    if (shouldSpread)
    //    {
    //        float x = UnityEngine.Random.Range(-spread, spread);
    //        float y = UnityEngine.Random.Range(-spread, spread);
    //        direction = playerCamera.transform.forward + new Vector3(x, y, 0);
    //    }

    //    //  Actual bullet instantiate

    //    RaycastHit hit;
    //    // 2nd arg: position + forward for the position in front of the gun
    //    // Bullet direction should face where you are aiming
    //    GameObject bullet = GameObject.Instantiate(bulletPrefab, gun.transform.position + gun.transform.forward, Quaternion.identity, bulletParent);
    //    BulletController bulletController = bullet.GetComponent<BulletController>();

    //    // Can specify a 5th arg for a layermask (i.e. if ray hits an "enemy" layermask)
    //    // Has to not hit player, so enemy layermask only? and if needed a layermask for terrain?
    //    if (Physics.Raycast(playerCamera.transform.position, direction, out hit, Mathf.Infinity))
    //    {
    //        bulletController.target = hit.point;
    //        bulletController.hit = true;
    //    }
    //    else
    //    {
    //        // If no target, shoot direction is based off of the position of center of the screen
    //        //bulletController.target = playerCamera.transform.position + playerCamera.transform.forward * bulletMissDistance;
    //        bulletController.target = playerCamera.transform.position + direction * bulletMissDistance;
    //        bulletController.hit = false;

    //    }
    //}





    //private void InstantiateBullet()
    //{
    //    for (int i = 0; i < bulletsPerTap; i++)
    //    {
    //        /* Spread */
    //        Vector3 direction = playerCamera.transform.forward;
    //        if (shouldSpread || gunType == GunType.Shotgun)  // if consecutive shot or a shotgun, spread
    //        {
    //            float x = UnityEngine.Random.Range(-spread, spread);
    //            float y = UnityEngine.Random.Range(-spread, spread);
    //            direction = playerCamera.transform.forward + new Vector3(x, y, 0);
    //        }


    //        RaycastHit hit;
    //        // 2nd arg: position + forward for the position in front of the gun
    //        // Bullet direction should face where you are aiming
    //        GameObject bullet = GameObject.Instantiate(bulletPrefab, gun.transform.position + gun.transform.forward, Quaternion.identity, bulletParent);
    //        BulletController bulletController = bullet.GetComponent<BulletController>();

    //        // Can specify a 5th arg for a layermask (i.e. if ray hits an "enemy" layermask)
    //        // Has to not hit player, so enemy layermask only? and if needed a layermask for terrain?
    //        if (Physics.Raycast(playerCamera.transform.position, direction, out hit, Mathf.Infinity))
    //        {
    //            bulletController.target = hit.point;
    //            bulletController.hit = true;
    //        }
    //        else
    //        {
    //            // If no target, shoot direction is based off of the position of center of the screen
    //            //bulletController.target = playerCamera.transform.position + playerCamera.transform.forward * bulletMissDistance;
    //            bulletController.target = playerCamera.transform.position + direction * bulletMissDistance;
    //            bulletController.hit = false;

    //        }
    //        //if (gunType == GunType.Rifle)
    //    }

    //    if (burstRounds > 1)
    //    {
    //        float timeBetweenConsecutiveShots = timeBetweenShots / (float)burstRounds;
    //        float timeToShoot = timeBetweenConsecutiveShots;
    //        for (int i = 1; i < burstRounds; i++)
    //        {
    //            Invoke(nameof(InstantiateBullet), timeToShoot);
    //            timeToShoot += timeBetweenConsecutiveShots;

    //        }
    //    }
    //}
}
