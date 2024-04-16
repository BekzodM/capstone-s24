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
    public Vector3 structure;
    public LayerMask whatIsStructure;
    public LayerMask whatIsPlayer;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public bool structureInSightRange, structureInAttackRange;


    void Awake()
    {
        player = GameObject.Find("Female 1").transform;
        enemy = GetComponent<NavMeshAgent>();
        whatIsPlayer = 1 << LayerMask.NameToLayer("Player");
        whatIsStructure = 1 << LayerMask.NameToLayer("Draggable");
        sightRange = 10;
        attackRange = 2;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        homeBase = GameObject.Find("Base Controller").transform;
    }

    // Update is called once per frame
    void Update()
    {
        structureInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsStructure);
        structureInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsStructure);

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //if(!playerInSightRange && !playerInAttackRange && !DestinationReachable()) enemy.SetDestination(homeBase.position);
        if(playerInSightRange && !playerInAttackRange && PlayerReachable()) ChasePlayer();
        else if(playerInSightRange && playerInAttackRange) AttackPlayer();
        else if(structureInSightRange && !structureInAttackRange && StructureReachable()) ChaseStructure();
        else if(structureInSightRange && structureInAttackRange) AttackStructure();
        else enemy.SetDestination(homeBase.position);
        
    }

    void ChasePlayer()
    {
        enemy.SetDestination(player.position);

    }

    void ChaseStructure() {
        // Get the position of the structure
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, sightRange, whatIsStructure);
        if (hitColliders.Length > 0)
        {
            structure = hitColliders[0].transform.position;
            // Store the position of the first detected structure
            enemy.SetDestination(structure);
            // You can also iterate through hitColliders to find the closest or any other criteria
        }
    }

    void AttackStructure() {
        enemy.ResetPath();
        transform.LookAt(structure);
    }

    void AttackPlayer()
    {
        enemy.ResetPath();
        transform.LookAt(player);
    }

    bool PlayerReachable(){
        NavMeshHit hit;
        bool isReachable = NavMesh.SamplePosition(player.position, out hit, 1f, NavMesh.AllAreas);

        return isReachable;
    }

    bool StructureReachable(){
        NavMeshHit hit;
        bool isReachable = NavMesh.SamplePosition(structure, out hit, 4f, NavMesh.AllAreas);

        return isReachable;
    }
}
