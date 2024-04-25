using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trap : Structure
{   protected List<GameObject> enemiesInZone;
    [SerializeField] protected float cooldown = 1f;
    private float nextCooldown = 0f;
    private bool isAttacking = false;

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

    private void Update()
    {
        if (isAttacking && Time.time >= nextCooldown)
        {
            if (enemiesInZone.Count > 0) {

                /*
                foreach (GameObject enemy in enemiesInZone)
                {
                    if (enemy != null)
                    {
                        Attack(enemy);
                        PlayAudio();
                    }
                    //nextCooldown = Time.time + cooldown;
                }*/
                Attack();

                //clear dead enemies from the enemiesInZone
                int enemiesInZoneSize = enemiesInZone.Count;
                for (int i = 0; i < enemiesInZoneSize; i++) {
                    GameObject enemy = enemiesInZone[i];
                    if (enemy != null)
                    {
                        if (enemy.GetComponent<Enemy>().GetIsDead()) {
                            RemoveEnemyFromZone(enemy);
                        }
                    }
                }

                PlayAudio();
            }
            nextCooldown = Time.time + cooldown;
        }
    }

    public void StartAttacking(GameObject target)
    {
        AddEnemyToZone(target);
        isAttacking = true;
        nextCooldown = Time.time;
    }

    public void StopAttacking(GameObject target)
    {
        RemoveEnemyFromZone(target);
        if (enemiesInZone.Count == 0)
        {
            isAttacking = false;
        }

    }

    //protected abstract void Attack(GameObject target);
    protected virtual void Attack() {
        foreach (GameObject enemy in enemiesInZone) {
            if (enemy != null) {
                DealDamage(enemy);
            }
        }
    }

    protected abstract void DealDamage(GameObject enemy);

    public void AddEnemyToZone(GameObject enemy)
    {
        enemiesInZone.Add(enemy);
    }

    public void RemoveEnemyFromZone(GameObject enemy)
    {
        enemiesInZone.Remove(enemy);
    }

}
