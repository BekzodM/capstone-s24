using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
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
        
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }


}
