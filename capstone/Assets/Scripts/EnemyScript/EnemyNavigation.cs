using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    Transform homeBase;
    private NavMeshAgent enemy;
    
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        homeBase = GameObject.Find("Base Controller").transform;
    }

    // Update is called once per frame
    void Update()
    {
        enemy.destination = homeBase.position;
    }

    public void SetHomeBase(Transform newHomeBase)
    {
        homeBase = newHomeBase;
        enemy.destination = homeBase.position;
    }
}
