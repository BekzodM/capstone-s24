using System.Collections;
using System.Collections.Generic;
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
        attackRange = 5;
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

        if(!playerInSightRange && !playerInAttackRange) enemy.SetDestination(homeBase.position);
        if(playerInSightRange && !playerInAttackRange) ChasePlayer();
        if(playerInSightRange && playerInAttackRange) AttackPlayer();
        
    }

    void ChasePlayer()
    {
        enemy.SetDestination(player.position);

    }

    void AttackPlayer()
    {
        enemy.SetDestination(transform.position);
        transform.LookAt(player);
    }
}
