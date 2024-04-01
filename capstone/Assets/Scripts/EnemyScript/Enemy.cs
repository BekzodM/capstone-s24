using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public HealthBar healthBar;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public string playerTag = "Player";
    float attackCooldown = 2f;
    float lastAttackTime = -9999f;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    void OnTriggerStay(Collider other) {
        if(other.CompareTag(playerTag)) {
            if(Time.time - lastAttackTime >= attackCooldown) {
                Vector3 closestPoint = other.ClosestPoint(attackPoint.position); 
                float distance = Vector3.Distance(attackPoint.position, closestPoint);
                if (distance <= attackRange) {
                    other.GetComponent<Player>().TakeDamage(10);
                    lastAttackTime = Time.time;
                }
            }
        }
    }

    /*
    Called by Player or Structure to deduct
    hp from current enemy's health.
    */
    public void TakeDamage(int damage) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if(currentHealth == 0) {
            Destroy(gameObject);
        }
    }

    //Take damage function for structures
    public void TakeDamage(int damage, GameObject attacker) { 
        currentHealth-= damage;
        healthBar.SetHealth(currentHealth);

        //remove killed enemy from enemiesInZone list
        attacker.GetComponent<Offensive>().RemoveEnemyFromZone(gameObject);
        
        if(currentHealth == 0)
        {
            Destroy(gameObject);
        }
    }

    // Dev Function - visualize attack range.
    void OnDrawGizmosSelected() {
        if(attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


}
