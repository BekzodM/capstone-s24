using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Offensive : Structure
{
    private List<GameObject> enemiesInZone;
    private GameObject targetEnemy;
    [SerializeField] private float cooldown = 1f;
    private float nextCooldown = 0f;
    private bool isAttacking = false;
    protected Offensive(string name, string description, int cost, int health)
        : base(name, description, "Offensive", cost, health)
    {
    }

    protected override void Start()
    {
        base.Start();
        SetStructureType("Offensive");
        enemiesInZone = new List<GameObject>();
    }

    private void Update()
    {
        if (isAttacking && Time.time >= nextCooldown) {
            if (targetEnemy == null && enemiesInZone.Count > 0) {
                targetEnemy = enemiesInZone[0];
            }
            if (targetEnemy != null) {
                Attack(targetEnemy);
                nextCooldown = Time.time + cooldown;            
            }
        }
    }

    public void StartAttacking(GameObject target)
    {

        enemiesInZone.Add(target);
        if (targetEnemy == null)
        {
            targetEnemy = target;
        }
        isAttacking = true;
        nextCooldown = Time.time;
    }

    public void StopAttacking(GameObject target)
    {
        
        enemiesInZone.Remove(target);
        if (targetEnemy == target)
        {
            targetEnemy = (enemiesInZone.Count > 0) ? enemiesInZone[0] : null;
        }
        if (enemiesInZone.Count == 0) {
            isAttacking = false;
        }
    }

    protected abstract void Attack(GameObject target);

}
