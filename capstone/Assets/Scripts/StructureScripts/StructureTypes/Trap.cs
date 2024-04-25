using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trap : Structure
{
    protected Trap(string name, string description, int cost, int health, int progressLevel, int attackDamage)
        : base(name, description, "Trap", cost, health, progressLevel, attackDamage)
    {
    }

    protected override void Start()
    {
        base.Start();
        SetStructureType("Trap");
        enemiesInZone = new List<GameObject>();
    }

    private List<GameObject> enemiesInZone;
    private GameObject targetEnemy;
    [SerializeField] protected float cooldown = 1f;
    private float nextCooldown = 0f;
    private bool isAttacking = false;



    private void Update()
    {
        if (isAttacking && Time.time >= nextCooldown)
        {
            foreach (var enemy in enemiesInZone)
            {
                if (enemy != null)
                {
                    Attack(enemy);
                    PlayAudio();
                }
                nextCooldown = Time.time + cooldown;
            }
        }
    }

    public void StartAttacking(GameObject target)
    {
        AddEnemyToZone(target);
        if (targetEnemy == null)
        {
            targetEnemy = target;
        }
        isAttacking = true;
        nextCooldown = Time.time;
    }

    public void StopAttacking(GameObject target)
    {
        RemoveEnemyFromZone(target);
        if (targetEnemy == target)
        {
            targetEnemy = (enemiesInZone.Count > 0) ? enemiesInZone[0] : null;
        }
        if (enemiesInZone.Count == 0)
        {
            isAttacking = false;
        }
    }

    protected abstract void Attack(GameObject target);

    protected abstract void DealDamage(GameObject enemy);

    public void AddEnemyToZone(GameObject enemy)
    {
        enemiesInZone.Add(enemy);
        //Debug.Log(enemy.name + " has entered the enemiesInZone");
    }

    public void RemoveEnemyFromZone(GameObject enemy)
    {
        enemiesInZone.Remove(enemy);
        //Debug.Log(enemy.name + " has lefted the enemiesInZone");
    }

}
