using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePhaseController : MonoBehaviour
{
    //wave spawner:
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public float timeBetweenWaves = 3f;
    public float countdown = 5f;
    public int numberOfWaves = 5;
    public Text waveCountdownText;
    public Text waveCounter;
    public float waveIndex = 0;
    public bool spawning = false;
    public bool roundComplete = false;

    //base damage:
    public float radius = 0.2f;
    public Transform basePoint;
    public GameObject[] allEnemies;
    public GameObject nearestEnemy;
    public int maxHealth = 100;
    public HealthBar healthBar;

    //planning phase connection:
    public GameObject planningPhaseObject;
    private PlanningPhaseManager planningPhaseManager;

    // Start is called before the first frame update
    void Start()
    {
        planningPhaseManager = planningPhaseObject.GetComponent<PlanningPhaseManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //round complete will invoke planning phase:
        if (roundComplete)
        {
            planningPhaseObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
