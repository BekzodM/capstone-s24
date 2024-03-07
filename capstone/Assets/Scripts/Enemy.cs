using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public HealthBar healthBar;

    public Transform attackPoint;
    public float attackRange = 2f;
    public string playerTag = "Player";
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag(playerTag)) {
            float distance = Vector3.Distance(transform.position, other.transform.position);
            if (distance <= attackRange) {
                other.GetComponent<Player>().TakeDamage(10);
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


}
