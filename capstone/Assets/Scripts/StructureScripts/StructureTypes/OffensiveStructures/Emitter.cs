using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Emitter : Offensive
{
    private List<GameObject> AOEZone;

    public Emitter(string name, string description, int cost, int health, int progressLevel, int attackDamage)
        : base(name, description, cost, health, progressLevel, attackDamage)
    {
    }

    protected override void Start()
    {
        AOEZone = new List<GameObject>();
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

            foreach (var enemy in AOEZone)
            {
                if (enemy != null)
                {
                    DealDamage(enemy);
                }
            }
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
