using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class WaveSpawner : MonoBehaviour
{

    public Transform enemyPrefab;

    public Transform spawnPoint;

    public float timeBetweenWaves = 3f;
    private float countdown = 5f;
    public int numberOfWaves = 5;

    public Text waveCountdownText;
    public Text waveCounter;

    private float waveIndex = 0;
    private bool spawning = false;

    public GameObject baseObject;
    private BaseDamage baseDamage;

    bool complete = false;

    private void Start()
    {
        baseDamage = baseObject.GetComponent<BaseDamage>();
    }
    void Update()
    {
        waveCounter.text = waveIndex.ToString();
        if (countdown <= 0f && waveIndex <= numberOfWaves && baseDamage.allEnemies.Length == 0)
        {
            spawning = true;
            StartCoroutine(SpawnWave());
        }

        DetermineColorOfCountdown();

        if (waveIndex <= numberOfWaves && countdown > 0f)//timer counting down:
        {
            countdown -= Time.deltaTime;

            waveCountdownText.text = Mathf.Round(countdown).ToString();
        }
        if (waveIndex >= numberOfWaves && baseDamage.allEnemies.Length == 0 && spawning == false)
        {
            complete = true;
            waveCountdownText.text = "Done";
        }
    }

    private void DetermineColorOfCountdown()
    {
        if (spawning)
        {
            waveCountdownText.color = Color.red;
        }
        else
        {
            waveCountdownText.color = Color.green;
        }
    }

    IEnumerator SpawnWave() //function to start wave, waiting for previous wave to be complete ( first spawnwave function has to complete before it can get triggered again)
    {
        waveIndex++;
        float numEnemies = waveIndex * numberOfWaves;
        countdown = numEnemies;
        for (int i = 0; i < numEnemies; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f);
        }
        spawning = false;
        waveCountdownText.enabled = false;
        while (baseDamage.allEnemies.Length != 0)
        {
            yield return null; // Wait for next frame
        }
        countdown = timeBetweenWaves;
        waveCountdownText.enabled = true;
        
    }

    private void SpawnEnemy() //function to spawn enemy
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

}
