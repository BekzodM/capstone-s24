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
    public LayerMask whatIsPlayer;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    void Awake()
    {
        player = GameObject.Find("Female 1").transform;
        enemy = GetComponent<NavMeshAgent>();
        whatIsPlayer = 1 << LayerMask.NameToLayer("Player");
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
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //if(!playerInSightRange && !playerInAttackRange && !DestinationReachable()) enemy.SetDestination(homeBase.position);
        if(playerInSightRange && !playerInAttackRange && DestinationReachable()) ChasePlayer();
        else if(playerInSightRange && playerInAttackRange) AttackPlayer();
        else enemy.SetDestination(homeBase.position);

        
    }

    void ChasePlayer()
    {
        enemy.SetDestination(player.position);

    }

    void AttackPlayer()
    {
        enemy.ResetPath();
        transform.LookAt(player);
    }

    bool DestinationReachable(){
        NavMeshHit hit;
        bool isReachable = NavMesh.SamplePosition(player.position, out hit, 1f, NavMesh.AllAreas);

        return isReachable;
    }
}
