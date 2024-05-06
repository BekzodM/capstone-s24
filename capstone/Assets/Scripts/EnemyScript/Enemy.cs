using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public HealthBar healthBar;
    BattlePhaseController battlePhaseController;
    protected bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        battlePhaseController = FindFirstObjectByType<BattlePhaseController>();
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
            battlePhaseController.planningPhaseManager.AddMoney(50);
        }
    }

    //Take damage function for structures
    public void TakeDamage(int damage, GameObject attacker) { 
        currentHealth-= damage;
        healthBar.SetHealth(currentHealth);

        
        if(currentHealth <= 0 && isDead == false)
        {
            isDead= true;
            Destroy(gameObject);
            battlePhaseController.planningPhaseManager.AddMoney(50);
        }
    }

    public bool GetIsDead() { return isDead; }
}
