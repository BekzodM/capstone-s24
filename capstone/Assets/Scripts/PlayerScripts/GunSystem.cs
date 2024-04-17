using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class GunSystem : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;

    [SerializeField] private GameObject bulletPrefab;
    //[SerializeField] private GameObject gun;

    private bool isShooting;        // Player bool input
    private bool isAiming = false;            // To determine how accurate the spread is
    private bool readyToShoot = true;
    private bool reloading = false;
    [SerializeField] private bool allowButtonHold = true;    // Allow for player to hold down and continuously shoot
    private bool shouldSpread = false;      // Indicates WHEN to spread
    private bool allowSpread = false;       // Determines IF the weapon can spread

    [SerializeField] private int damage = 10;
    [SerializeField] private int magazineSize = 100;
    private int bulletsLeft;
    [SerializeField] private int bulletsPerTap = 10;  // How many bullets to shoot out
    private int burstRounds = 3;    // How many shots to shoot consecutively after one click
    private int bulletsShot = 0;    // The amount of bullets fired consecutively per click (counter)

    [SerializeField] private float timeBetweenShooting = 1.1f;   // Time between shots being reset & player input clicks (fire rate)
    [SerializeField] private float timeBetweenShots = .0f;       // Time between consecutive shots (per click)  // 0f for shotgun, > 0f for burst
    [SerializeField] private float reloadTime = 2f;
    [SerializeField] private float spread = .02f;
    [SerializeField] private float steadyAimTime = .5f;  // The time it takes after the first shot/click to steady the gun
    /* gun type will determine allow to hold*/

    // All instantiated bullets will belong to this parent to make it so the hierarchy doesn't get messy
    [SerializeField] private Transform bulletParent;
    [SerializeField] private float bulletMissDistance;

    private enum GunType
    {
        [EnumMember(Value = "SemiAuto")] SemiAuto,      // Pistols, Snipers, Certain rifles
        [EnumMember(Value = "Burst")] Burst,            // Certain assault rifles (e.g. 3 round burst each trigger press)
        [EnumMember(Value = "Auto")] Auto,              // Fully automatic assult rifles/Machine guns
        [EnumMember(Value = "Multishot")] Multishot,    // Shotguns
    }
    [SerializeField] private GunType gunType;

    private void OnDisable()
    {
        /* Needed because if player holds on shoot button while battle phase -> planning phase,
         * on release in the player controller script is not called due to being disabled,
         * so the player controller does not tell the gun to stop shooting
         */
        readyToShoot = true;
        isShooting = false;
    }

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;

        isShooting = false;
    }

    private void Update()
    {

    }

    public void Shoot()
    {
        isShooting = !isShooting;
        ShootShot();
    }

    // can shoot a burst
    // can shoot a shot
    // can shoot a slug
    public void ShootShot()
    {

        if (isShooting && readyToShoot && !reloading && bulletsLeft > 0)
        {
            readyToShoot = false;
            bulletsLeft--;

            // Bullet instantiate

            //if (gunType == GunType.SemiAuto || gunType == GunType.Auto)
            // auto vs semi is actually determined by allowButtonHold
            //{
            InstantiateBullet();
            //}
            // handling multiple bullets (SHOTGUNS & BURST RIFLES)
            if (gunType == GunType.Burst || gunType == GunType.Multishot)
            {
                //InstantiateBullet(); // Shoot first bullet
                /* 
                 * NOTE: can divide timeBetweenShooting by bulletsPerTap instead to avoid wording similarity confusion (but uses division)
                 */
                shouldSpread = true;
                float timeGap = timeBetweenShots;
                for (int i = 1; i < bulletsPerTap; i++)
                {
                    ///* Spread */
                    //Vector3 direction = playerCamera.transform.forward;
                    //if (shouldSpread || gunType == GunType.Shotgun)  // if consecutive shot or a shotgun, spread
                    //{
                    //    float x = UnityEngine.Random.Range(-spread, spread);
                    //    float y = UnityEngine.Random.Range(-spread, spread);
                    //    direction = playerCamera.transform.forward + new Vector3(x, y, 0);
                    //}

                    ////  Actual bullet instantiate

                    //RaycastHit hit;
                    //// 2nd arg: position + forward for the position in front of the gun
                    //// Bullet direction should face where you are aiming
                    //GameObject bullet = GameObject.Instantiate(bulletPrefab, gun.transform.position + gun.transform.forward, Quaternion.identity, bulletParent);
                    //BulletController bulletController = bullet.GetComponent<BulletController>();

                    //// Can specify a 5th arg for a layermask (i.e. if ray hits an "enemy" layermask)
                    //// Has to not hit player, so enemy layermask only? and if needed a layermask for terrain?
                    //if (Physics.Raycast(playerCamera.transform.position, direction, out hit, Mathf.Infinity))
                    //{
                    //    bulletController.target = hit.point;
                    //    bulletController.hit = true;
                    //}
                    //else
                    //{
                    //    // If no target, shoot direction is based off of the position of center of the screen
                    //    //bulletController.target = playerCamera.transform.position + playerCamera.transform.forward * bulletMissDistance;
                    //    bulletController.target = playerCamera.transform.position + direction * bulletMissDistance;
                    //    bulletController.hit = false;

                    //}



                    Invoke(nameof(InstantiateBullet), timeGap);

                    if (gunType == GunType.Burst)   // only shotguns multiple shots count as 1 shot used
                        bulletsLeft--;

                    timeGap += timeBetweenShots;
                    //Debug.Log("burst");
                    //Invoke(nameof(SteadyAim), steadyAimTime);
                }

                Invoke(nameof(SteadyAim), steadyAimTime);
            }
            //if (gunType == GunType.Multishot)
            //{
            //    for (int i = 0; i < bulletsPerTap; i++)
            //        InstantiateBullet();
            //    Debug.Log("multi");
            //}


            Invoke(nameof(ResetShot), timeBetweenShooting);

            // AUTOMATIC RIFLE
            if (allowButtonHold)    // should switch to auto/semiauto enum
            {
                /* MIGHT BE ABLE TO SWITCH BACK for fully auto*/
                Invoke(nameof(ShootShot), timeBetweenShooting);    // If held down shoot button

                shouldSpread = true;    // only spread if for held down weapons

                Invoke(nameof(SteadyAim), steadyAimTime);
            }
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    // Player input callback
    public void OnReload()
    {
        if (bulletsLeft < magazineSize && !reloading)
            Reload();
    }

    // Actual reload logic
    private void Reload()
    {

        reloading = true;
        Invoke(nameof(ReloadFinished), reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

    private void SteadyAim()
    {
        //if (!isShooting)
        //{

        // steady aim time MUST BE LONGER than BURST timeBetweenShots * shotsPerTap
        shouldSpread = false;
        //}
    }

    private void InstantiateBullet()
    {
        Vector3 direction = playerCamera.transform.forward;
        //if (shouldSpread || gunType == GunType.Multishot)  // if consecutive shot or a shotgun, spread
        if (shouldSpread)
        {
            float x = UnityEngine.Random.Range(-spread, spread);
            float y = UnityEngine.Random.Range(-spread, spread);
            direction = playerCamera.transform.forward + new Vector3(x, y, 0);
        }

        //  Actual bullet instantiate

        RaycastHit hit;
        // 2nd arg: position + forward for the position in front of the gun
        // Bullet direction should face where you are aiming
        GameObject bullet = GameObject.Instantiate(bulletPrefab, this.transform.position + this.transform.forward, Quaternion.identity, bulletParent);
        BulletController bulletController = bullet.GetComponent<BulletController>();
        bulletController.damage = damage;   // Set the damage amount for the bullet

        // Can specify a 5th arg for a layermask (i.e. if ray hits an "enemy" layermask)
        // Has to not hit player, so enemy layermask only? and if needed a layermask for terrain?
        if (Physics.Raycast(playerCamera.transform.position, direction, out hit, Mathf.Infinity))
        {
            // direction * 1 for raycast contact point to penetrate enemy mesh and reach collider
            // This is also because the target is moving
            // This is because the raycast contact point is the final destination for the bullet to travel
            bulletController.target = hit.point + direction * 1;
            bulletController.hit = true;
        }
        else
        {
            // If no target, shoot direction is based off of the position of center of the screen
            //bulletController.target = playerCamera.transform.position + playerCamera.transform.forward * bulletMissDistance;
            bulletController.target = playerCamera.transform.position + direction * bulletMissDistance;
            bulletController.hit = false;

        }
    }
}
