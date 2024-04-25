using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class Offensive : Structure
{
    private List<GameObject> enemiesInZone;
    private GameObject targetEnemy;
    [SerializeField] protected float cooldown = 1f;
    private float nextCooldown = 0f;
    private bool isAttacking = false;
    protected Offensive(string name, string description, int cost, int health, int progressLevel, int attackDamage)
        : base(name, description, "Offensive", cost, health, progressLevel, attackDamage)
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

        if (isAttacking && Time.time >= nextCooldown)
        {
            if (targetEnemy == null && enemiesInZone.Count > 0)
            {
                targetEnemy = enemiesInZone[0];
            }
            if (targetEnemy != null)
            {
                Attack(targetEnemy);
                PlayAudio();
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
