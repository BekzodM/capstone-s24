using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shooter : Offensive
{
    [SerializeField] private float projectileSpeed = 20f;
    [SerializeField] private float projectileDistanceFromHead = 1.5f;
    [SerializeField] private GameObject attackPrefab;
    [SerializeField] protected float attackDamage;
    [SerializeField] protected float critChance;
    [SerializeField] protected float critDamage;
    public Shooter(string name, string description, int cost, int health, float attackSpeed, float attackDamage, float critChance, float critDamage)
        : base(name, description, cost, health)
    {
        this.attackDamage = attackDamage;
        this.critChance = critChance;
        this.critDamage = critDamage;
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

}
