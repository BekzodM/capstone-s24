using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDamage : MonoBehaviour
{
    public float radius = 0.2f;
    
    public Transform basePoint;
    public GameObject[] allEnemies;
    public GameObject nearestEnemy;
    float distance;
    float nearestDistance = 100000;

    public int maxHealth = 100;
    int currentHealth;

    //public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        nearestEnemy = null;

    }

    private void Update()
    {
        nearestEnemyFunction();
        baseEnemyAttack();
    }

    private void baseEnemyAttack()
    {
            if (nearestEnemy!= null && Vector3.Distance(basePoint.position, nearestEnemy.transform.position) < radius)
            {
                Destroy(nearestEnemy);
                nearestEnemy = null;
                currentHealth -= 5;
            }
            if (currentHealth <= 0)
            {
                //GAME OVER IMPLEMENT
            }

    }
    private void nearestEnemyFunction()
    {
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (allEnemies.Length > 0)
        {
            for (int i = 0; i < allEnemies.Length; i++)
            {
                distance = Vector3.Distance(basePoint.position, allEnemies[i].transform.position);

                if (distance < nearestDistance)
                {
                    nearestEnemy = allEnemies[i];
                    nearestDistance = distance;
                }
            }
        }
    }
}
