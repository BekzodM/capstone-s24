using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePhaseController : MonoBehaviour
{
    //current information:
    public bool levelComplete;
    public int currentRound;

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
    public int baseDamageAmount = 5;
    public bool baseAlive = true;

    //planning phase connection:
    public GameObject planningPhaseObject;

    [SerializeField] GameObject player;
    public PlanningPhaseManager planningPhaseManager;

    private void Awake()
    {
        planningPhaseManager = planningPhaseObject.GetComponent<PlanningPhaseManager>();
        maxHealth = planningPhaseManager.GetMaxBaseHealth();
        currentRound = 1;
        levelComplete = false;
        

    }

    // Start is called before the first frame update
    void Start()
    {
        planningPhaseManager.SetWave(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!baseAlive)
        {
            planningPhaseObject.SetActive(false);
            gameObject.SetActive(false);
        }
        //round complete will invoke planning phase:
        if (roundComplete)
        {
            planningPhaseObject.SetActive(true);
            //increaseRound();
            planningPhaseManager.SetWave(increaseRound());
            waveIndex = 0;
            numberOfWaves++;
            countdown = 5f;
            roundComplete = false;
            player.transform.GetChild(2).gameObject.GetComponent<PlayerController>().ToggleCursorUnlocked();
            player.SetActive(false);
            gameObject.SetActive(false);

        }
    }

    int increaseRound()
    {
        currentRound++;
        return currentRound;
    }

}
