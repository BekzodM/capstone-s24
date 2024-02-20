using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Offensive : Structure
{
    [SerializeField] private GameObject attackPrefab;
    private List<GameObject> enemiesInZone;
    private GameObject targetEnemy;
    [SerializeField] private float cooldown = 1f;
    private float nextCooldown = 0f;
    private bool isShooting = false;
    [SerializeField] private float projectileDistanceFromHead = 1.5f;
    [SerializeField] private float projectileSpeed = 20f;
    protected Offensive(string name, string description, int cost, int health, float areaEffectRadius)
        : base(name, description, "Offensive", cost, health, areaEffectRadius)
    {
    }

    private void Start()
    {
        enemiesInZone = new List<GameObject>();
    }

    private void Update()
    {
        if (isShooting && Time.time >= nextCooldown) {
            if (targetEnemy == null && enemiesInZone.Count > 0) {
                targetEnemy = enemiesInZone[0];
            }
            if (targetEnemy != null) {
                Shoot(targetEnemy);
                nextCooldown = Time.time + cooldown;            
            }
        }
    }

    public void StartShooting(GameObject target)
    {

        enemiesInZone.Add(target);
        if (targetEnemy == null)
        {
            targetEnemy = target;
        }
        isShooting = true;
        nextCooldown = Time.time;
    }

    public void StopShooting(GameObject target)
    {
        
        enemiesInZone.Remove(target);
        if (targetEnemy == target)
        {
            targetEnemy = (enemiesInZone.Count > 0) ? enemiesInZone[0] : null;
        }
        if (enemiesInZone.Count == 0) {
            isShooting = false;
        }
    }
    private void Shoot(GameObject target) {
        if (target == null)
        {
            Debug.LogWarning("Target is null. Cannot shoot.");
            return;
        }

        GameObject model = transform.GetChild(1).gameObject;
        Transform modelHead = model.transform.GetChild(0);
        Vector3 direction = target.transform.position - modelHead.transform.position;
        
        if (direction != Vector3.zero)
        {
            //rotate head
            modelHead.rotation = Quaternion.LookRotation(direction);

            //instantiate projectile
            Vector3 spawnPos = modelHead.position + modelHead.forward * projectileDistanceFromHead;
            if (attackPrefab != null) {
                GameObject projectileInstance = Instantiate(attackPrefab, spawnPos, Quaternion.identity);

                //launching projectile
                Rigidbody attackRB = projectileInstance.GetComponent<Rigidbody>();
                if (attackRB != null)
                {
                    attackRB.velocity = direction.normalized * projectileSpeed;
                }
            }
        }
    }
}
