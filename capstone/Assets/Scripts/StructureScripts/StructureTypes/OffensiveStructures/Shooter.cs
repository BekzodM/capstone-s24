using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shooter : Offensive
{
    [SerializeField] private float projectileSpeed = 20f;
    [SerializeField] private float projectileDistanceFromHead = 1.5f;
    [SerializeField] private GameObject attackPrefab;

    public Shooter(string name, string description, int cost, int health, int attackDamage)
        : base(name, description, cost, health, attackDamage)
    {
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack(GameObject target)
    {
        if (target == null)
        {
            Debug.LogWarning("Target is null. Cannot shoot.");
            return;
        }

        GameObject model = transform.GetChild(1).gameObject;
        Transform modelHead = model.transform.GetChild(0);
        Vector3 direction = target.transform.position - modelHead.transform.position;

        if (direction != Vector3.zero)
        {
            //rotate head
            modelHead.rotation = Quaternion.LookRotation(direction);

            //instantiate projectile
            Vector3 spawnPos = modelHead.position + modelHead.forward * projectileDistanceFromHead;
            if (attackPrefab != null)
            {
                GameObject projectileInstance = Instantiate(attackPrefab, spawnPos, Quaternion.LookRotation(direction));

                //launching projectile
                Rigidbody attackRB = projectileInstance.GetComponent<Rigidbody>();
                if (attackRB != null)
                {
                    attackRB.velocity = direction.normalized * projectileSpeed;
                }
            }
        }
    }

    protected override void DealDamage(GameObject enemy)
    {
        //enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
    }

}
