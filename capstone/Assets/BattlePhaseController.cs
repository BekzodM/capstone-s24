using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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


    public GameObject levelOverScene;
    public Text gameOverText;

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

    }

    // Update is called once per frame
    void Update()
    {
        if (levelComplete)
        {
            levelCompleteFunctions();

        }
        if (!baseAlive || player.transform.GetChild(2).gameObject.GetComponent<Player>().currentHealth <= 0)
        {
            planningPhaseObject.SetActive(false);
            player.transform.GetChild(2).gameObject.GetComponent<PlayerController>().ToggleCursorUnlocked();
            StartCoroutine(WaitForEnemiesDestroyed());
            player.SetActive(false);
            gameObject.SetActive(false);
            gameOverText.text = "Level Failed";
            levelOverScene.SetActive(true);
        }


        //round complete will invoke planning phase:
        if (roundComplete)
        {
            StartCoroutine(WaitForEnemiesDestroyed());
            planningPhaseObject.SetActive(true);
            //increaseRound();
            planningPhaseManager.SetWave(increaseRound());
            if (currentRound <= planningPhaseManager.GetTotalWaves())
            {
                waveIndex = 0;
                numberOfWaves++;
                countdown = 5f;
                roundComplete = false;
                player.transform.GetChild(2).gameObject.GetComponent<PlayerController>().ToggleCursorUnlocked();
                player.SetActive(false);
                gameObject.SetActive(false);
            }
            else
            {
                levelComplete = true;
                levelCompleteFunctions();
            }

        }
    }

    int increaseRound()
    {
        currentRound++;
        return currentRound;
    }

    IEnumerator WaitForEnemiesDestroyed()
    {
        for (int i = 0; i < allEnemies.Length; i++)
        {
            Destroy(allEnemies[i]);
        }
        yield return (allEnemies.Length == 0);
    }

    void levelCompleteFunctions()
    {
        player.transform.GetChild(2).gameObject.GetComponent<PlayerController>().ToggleCursorUnlocked();
        player.SetActive(false);
        planningPhaseObject.SetActive(false);
        gameOverText.text = "Level Complete";
        levelOverScene.SetActive(true);
        gameObject.SetActive(false);
    }

}
