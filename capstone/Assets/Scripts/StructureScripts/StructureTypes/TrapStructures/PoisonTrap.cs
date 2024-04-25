using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTrap : Trap
{

    public PoisonTrap(string name, string description, int cost, int health, int progressLevel, int attackDamage)
        : base(name, description, cost, health, progressLevel, attackDamage)
    {
    }

    protected override void Awake()
    {
        SetStructureName("PoisonTrap");
        SetStructureType("Trap");
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    /*
    protected override void Attack(GameObject target)
    {
        if (target == null)
        {
            Debug.LogWarning("Target is null. Cannot attack.");
            return;
        }


        DealDamage(target);
    }
    */

    protected override void Attack()
    {
        base.Attack();
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

    protected override void Slot0UpgradeLevel1()
    {
        attackDamage += 2;
    }

    protected override void Slot0UpgradeLevel2()
    {
        attackDamage += 2;
    }

    protected override void Slot0UpgradeLevel3()
    {
        attackDamage += 2;
    }

    protected override void Slot0UpgradeLevel4()
    {
        attackDamage += 2;
    }

    protected override void Slot0UpgradeLevel5()
    {
        attackDamage += 2;
    }
    //slot1
    protected override void Slot1UpgradeLevel1()
    {
        cooldown = cooldown * 0.5f;
    }

    protected override void Slot1UpgradeLevel2()
    {
        cooldown = cooldown * 0.5f;
    }

    protected override void Slot1UpgradeLevel3()
    {
        cooldown = cooldown * 0.5f;
    }

    protected override void Slot1UpgradeLevel4()
    {
        cooldown = cooldown * 0.5f;
    }

    protected override void Slot1UpgradeLevel5()
    {
        cooldown = cooldown * 0.5f;
    }
    //slot2
    protected override void Slot2UpgradeLevel1()
    {
        float currentRadius = GetAreaZoneRadius();
        SetAreaZoneRadius(currentRadius * 0.5f + currentRadius);
    }

    protected override void Slot2UpgradeLevel2()
    {
        float currentRadius = GetAreaZoneRadius();
        SetAreaZoneRadius(currentRadius * 0.5f + currentRadius);
    }

    protected override void Slot2UpgradeLevel3()
    {
        float currentRadius = GetAreaZoneRadius();
        SetAreaZoneRadius(currentRadius * 0.5f + currentRadius);
    }

    protected override void Slot2UpgradeLevel4()
    {
        float currentRadius = GetAreaZoneRadius();
        SetAreaZoneRadius(currentRadius * 0.5f + currentRadius);
    }

    protected override void Slot2UpgradeLevel5()
    {
        float currentRadius = GetAreaZoneRadius();
        SetAreaZoneRadius(currentRadius * 0.5f + currentRadius);
    }
}


