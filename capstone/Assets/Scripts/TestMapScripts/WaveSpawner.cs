using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class WaveSpawner : MonoBehaviour
{
    public GameObject battlePhaseControllerObject;
    private BattlePhaseController battlePhaseController;

    //public GameObject baseObject;
    //private battlePhaseController battlePhaseController;

    private void Start()
    {
        battlePhaseController = battlePhaseControllerObject.GetComponent<BattlePhaseController>();

    }

    void Update()
    {
        if (!battlePhaseController.roundComplete)  
        {
            battlePhaseController.waveCounter.text = battlePhaseController.waveIndex.ToString();
            if (battlePhaseController.countdown <= 0f && battlePhaseController.waveIndex < battlePhaseController.numberOfWaves && battlePhaseController.allEnemies.Length == 0)
            {
                battlePhaseController.waveIndex++;
                battlePhaseController.spawning = true;
                StartCoroutine(SpawnWave());
            }

            DetermineColorOfCountdown();

            if (battlePhaseController.waveIndex <= battlePhaseController.numberOfWaves && battlePhaseController.countdown > 0f)//timer counting down:
            {
                battlePhaseController.countdown -= Time.deltaTime;
                
                battlePhaseController.waveCountdownText.text = Mathf.Round(battlePhaseController.countdown).ToString();
            }
            if (battlePhaseController.waveIndex >= battlePhaseController.numberOfWaves && battlePhaseController.allEnemies.Length == 0 && battlePhaseController.spawning == false)
            {
                battlePhaseController.roundComplete = true;
                battlePhaseController.waveCountdownText.text = "Done";
            }
        }
    }

    private void DetermineColorOfCountdown()
    {
        if (battlePhaseController.spawning)
        {
            battlePhaseController.waveCountdownText.color = Color.red;
        }
        else
        {
            battlePhaseController.waveCountdownText.color = Color.green;
        }
    }

    IEnumerator SpawnWave() //function to start wave, waiting for previous wave to be complete ( first spawnwave function has to complete before it can get triggered again)
    {
        float numEnemies = battlePhaseController.waveIndex * battlePhaseController.numberOfWaves;
        battlePhaseController.countdown = numEnemies;
        for (int i = 0; i < numEnemies; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f);
        }
        battlePhaseController.spawning = false;
        battlePhaseController.waveCountdownText.enabled = false;
        while (battlePhaseController.allEnemies.Length != 0)
        {
            yield return null; // Wait for next frame
        }
        battlePhaseController.countdown = battlePhaseController.timeBetweenWaves;
        battlePhaseController.waveCountdownText.enabled = true;
        
    }

    private void SpawnEnemy() //function to spawn enemy
    {
        Instantiate(battlePhaseController.enemyPrefab, battlePhaseController.spawnPoint.position, battlePhaseController.spawnPoint.rotation);
    }

}
