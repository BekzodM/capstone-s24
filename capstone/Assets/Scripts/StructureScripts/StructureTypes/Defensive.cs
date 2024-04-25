using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Defensive : Structure
{
    protected List<GameObject> enemiesInZone;
    [SerializeField] protected float cooldown = 1f;
    private float nextCooldown = 0f;
    private bool isAttacking = false;

    protected Defensive(string name, string description, int cost, int health, int progressLevel, int attackDamage)
        : base(name, description, "Defensive", cost, health, progressLevel, attackDamage)
    {
    }

    protected override void Start()
    {
        base.Start();
        enemiesInZone= new List<GameObject>();
    }

    protected void Update()
    {
        if (isAttacking && Time.time >= nextCooldown) {
            if (enemiesInZone.Count > 0) {
                Attack();

                //clear dead enemies from the enemiesInZone
                int enemiesInZoneSize = enemiesInZone.Count;
                for (int i = 0; i < enemiesInZoneSize; i++)
                {
                    GameObject enemy = enemiesInZone[i];
                    if (enemy != null)
                    {
                        if (enemy.GetComponent<Enemy>().GetIsDead())
                        {
                            RemoveEnemyFromZone(enemy);
                        }
                    }
                }

                PlayAudio();
            }
            nextCooldown= Time.time + cooldown;
        }
    }

    public virtual void StartDefensiveAttack(GameObject other) {
        enemiesInZone.Add(other);
        isAttacking= true;
        nextCooldown= Time.time;
    }
    public virtual void EndDefensiveAttack(GameObject other) {
        enemiesInZone.Remove(other);
        if (enemiesInZone.Count == 0) {
            isAttacking= false;
        }
    }

    protected virtual void Attack() {
        foreach (GameObject enemy in enemiesInZone) {
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent != null) {
                enemyComponent.TakeDamage(attackDamage, gameObject);            
            }
        }
    }

    public void RemoveEnemyFromZone(GameObject enemy)
    {
        enemiesInZone.Remove(enemy);
        //Debug.Log(enemy.name + " has lefted the enemiesInZone");
    }


}
