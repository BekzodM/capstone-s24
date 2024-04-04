using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDamage : MonoBehaviour
{
    public GameObject battlePhaseControllerObject;
    private BattlePhaseController battlePhaseController;

    float distance;
    float nearestDistance = 100000;
    int currentHealth;

    void Start()
    {
        battlePhaseController = battlePhaseControllerObject.GetComponent<BattlePhaseController>();

        currentHealth = battlePhaseController.maxHealth;
        battlePhaseController.healthBar.SetMaxHealth(battlePhaseController.maxHealth);
        battlePhaseController.nearestEnemy = null;
    }

    private void Update()
    {
        //nearestEnemyFunction();
        baseEnemyAttack();
    }

    private void baseEnemyAttack()
    {
            nearestEnemyFunction();
            if (battlePhaseController.nearestEnemy!= null && Vector3.Distance(battlePhaseController.basePoint.position, battlePhaseController.nearestEnemy.transform.position) < battlePhaseController.radius)
            {
                Destroy(battlePhaseController.nearestEnemy);
                battlePhaseController.nearestEnemy = null;
                currentHealth -= 5;
                battlePhaseController.healthBar.SetHealth(currentHealth);
            }
            if (currentHealth <= 0)
            {
            //GAME OVER IMPLEMENT
            //temp delete player to simulate game over
                Destroy(GameObject.FindGameObjectWithTag("Player"));
            }

    }
    private void nearestEnemyFunction()
    {
        battlePhaseController.allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (battlePhaseController.allEnemies.Length > 0)
        {
            for (int i = 0; i < battlePhaseController.allEnemies.Length; i++)
            {
                distance = Vector3.Distance(battlePhaseController.basePoint.position, battlePhaseController.allEnemies[i].transform.position);

                if (distance <= nearestDistance)
                {
                    battlePhaseController.nearestEnemy = battlePhaseController.allEnemies[i];
                    nearestDistance = distance;
                }
            }
        }
    }
}
