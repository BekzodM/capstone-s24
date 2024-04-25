using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public HealthBar healthBar;
    protected bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        /*
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        */
    }

    /*
    Called by Player or Structure to deduct
    hp from current enemy's health.
    */
    public void TakeDamage(int damage) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if(currentHealth <= 0) {
            Destroy(gameObject);
        }
    }

    //Take damage function for structures
    public void TakeDamage(int damage, GameObject attacker) { 
        currentHealth-= damage;
        healthBar.SetHealth(currentHealth);

        
        if(currentHealth <= 0 && isDead == false)
        {
            isDead= true;
            
            /*
            //remove killed enemy from enemiesInZone list
            string structType = attacker.GetComponent<Structure>().GetStructureType();

            if (structType == "Offensive") {
                attacker.GetComponent<Offensive>().RemoveEnemyFromZone(gameObject);
            }
            else if (structType == "Defensive") {
                attacker.GetComponent<Defensive>().RemoveEnemyFromZone(gameObject);
            }
            else if (structType == "Trap") {
                //attacker.GetComponent<Trap>().RemoveEnemyFromZone(gameObject);
            }
            */

            Destroy(gameObject);
        }
    }

    public bool GetIsDead() { return isDead; }
}
