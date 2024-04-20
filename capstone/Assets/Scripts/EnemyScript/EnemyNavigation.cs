using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    Transform homeBase;
    private NavMeshAgent enemy;
    public Transform player;
    [System.NonSerialized] public Collider structure;
    public LayerMask whatIsStructure;
    public LayerMask whatIsPlayer;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public EnemyAttack enemyAttack;

    void Awake()
    {
        player = GameObject.Find("Female 1").transform;
        enemy = GetComponent<NavMeshAgent>();
        enemyAttack = GetComponent<EnemyAttack>();
        whatIsPlayer = 1 << LayerMask.NameToLayer("Player");
        whatIsStructure = 1 << LayerMask.NameToLayer("Draggable");
        sightRange = 5;
        attackRange = 1;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        homeBase = GameObject.Find("Base Controller").transform;
    }

    // Update is called once per frame
    void Update()
    {

        // Checks if a structure (Draggable Layered object) is in sight range
        //structureInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsStructure);
        // Checks if a structure (Draggable Layered object) is in attack range
        //structureInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsStructure);

        // Checks if player is in sight range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        // Checks if player is in attack range
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(playerInSightRange && !playerInAttackRange && PlayerReachable()) ChasePlayer();  // 1st Prioirty = Player: if nearby chase player
        else if(playerInSightRange && playerInAttackRange) AttackPlayer();  // if within enemy attack range then deal damage to player
        else if(structureInSightRange() && !structureInAttackRange()) ChaseStructure(); // 2nd Priority = Structure: if nearyby go to structure
        else if(structureInSightRange() && structureInAttackRange()) AttackStructure(); // if within enemy attack range then deal damage to structure
        else enemy.SetDestination(homeBase.position); // If nothing nearby, go to base
        
    }

    void ChasePlayer()
    {
        enemy.SetDestination(player.position);

    }

    void ChaseStructure() {
        if (structure != null) {
            if(StructureReachable()) enemy.SetDestination(structure.transform.position); // If Structure reachable go to it
            else enemy.SetDestination(homeBase.position); // Else go to base
        }
    }

    /*
    Make enemy stay in one place.
    Deal damage to structure detected by ChaseStructure while looking at it.
    */
    void AttackStructure() {
        enemy.ResetPath();
        if(structure != null) {
            enemyAttack.EnemyAttackStructure(structure);
            transform.LookAt(structure.transform.position);
        }
    }

    void AttackPlayer()
    {
        enemy.ResetPath();
        transform.LookAt(player);
        enemyAttack.EnemyAttackPlayer();
    }

    /*
    Check if Player is within NavMesh Area
    */
    bool PlayerReachable(){
        NavMeshHit hit;
        bool isReachable = NavMesh.SamplePosition(player.position, out hit, 1f, NavMesh.AllAreas);

        return isReachable;
    }

    /*
    Check if Structure is within NavMesh Area
    */
    bool StructureReachable(){
        NavMeshHit hit;
        bool isReachable = NavMesh.SamplePosition(structure.transform.position, out hit, 2f, NavMesh.AllAreas);

        return isReachable;
    }

    bool structureInSightRange() {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, sightRange, whatIsStructure);
        
        foreach (Collider collider in hitColliders)
        {
            //check if structure has a Nav Mesh Obstacle (not a trap)
            NavMeshObstacle navObstacle = collider.GetComponentInChildren<NavMeshObstacle>();
            if (navObstacle != null)
            {
                structure = collider;
                return true;
            }
        }
        return false;
    }

    bool structureInAttackRange() {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange, whatIsStructure);
        
        foreach (Collider collider in hitColliders)
        {
            //check if structure has a Nav Mesh Obstacle (not a trap)
            NavMeshObstacle navObstacle = collider.GetComponentInChildren<NavMeshObstacle>();
            if (navObstacle != null)
            {
                structure = collider;
                return true;
            }
        }
        return false;
    }
}
