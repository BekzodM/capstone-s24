using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Emitter : Offensive
{
    [SerializeField] private float rayDistanceFromHead;


    private List<GameObject> AOEZone;

    public Emitter(string name, string description, int cost, int health, int progressLevel, int attackDamage)
        : base(name, description, cost, health, progressLevel, attackDamage)
    {
    }

    protected override void Start()
    {
        base.Start();
    }

    public void AddToAOE(GameObject enemy)
    {
        AOEZone.Add(enemy);
    }

    public void RemoveFromAOE(GameObject enemy)
    {
        AOEZone.Remove(enemy);
    }



    protected override void Attack(GameObject target)
    {
        if (target == null)
        {
            Debug.LogWarning("Target is null. Cannot shoot.");
            return;
        }

        GameObject model = transform.GetChild(1).gameObject;
        Transform modelHead = model.transform.GetChild(1);
        Vector3 direction = target.transform.position - modelHead.transform.position;

        if (direction != Vector3.zero)
        {
            //rotate head
            modelHead.rotation = Quaternion.LookRotation(direction);

            // foreach (var enemy in AOEZone)
            // {
            //     if (enemy != null)
            //     {
            //         DealDamage(enemy);
            //     }
            // }

            //Use Raycast that points to the enemy's direction and damage them
            // RaycastHit hit;
            // Vector3 startPos = modelHead.position + modelHead.forward * rayDistanceFromHead;
            // float distanceToTarget = Vector3.Distance(target.transform.position, startPos);//ray length
            // if (Physics.Raycast(startPos, direction, out hit, distanceToTarget))
            // {
            //     Debug.Log(hit.collider.name);
            //     Debug.DrawLine(startPos, hit.point, Color.blue, 5);
            //     // if (hit.collider.gameObject.tag == "Enemy")
            //     // {

            //     //     if (AOE != null)
            //     //     {
            //     //         //attacking particles
            //     //         GameObject muzzleInstance = Instantiate(AOE, startPos, Quaternion.identity);
            //     //         ParticleSystem ps1 = AOE.GetComponent<ParticleSystem>();

            //     //         if (ps1 != null)
            //     //         {
            //     //             ps1.Play();
            //     //             Destroy(muzzleInstance, ps1.main.duration);
            //     //         }
            //     //         DealDamage(hit.collider.gameObject);
            //     //     }

            //     // }
            // }
        }
    }


    protected override void DealDamage(GameObject target)
    {
        if (target != null)
        {
            target.GetComponent<Enemy>().TakeDamage(attackDamage, gameObject);
        }
        else
        {
            //Debug.Log("No valid target to deal damage to");
        }

    }

}
