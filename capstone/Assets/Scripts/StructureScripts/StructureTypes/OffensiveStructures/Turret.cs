using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Shooter
{
    public Turret(string name, string description, int cost, int health, int progressLevel, int attackDamage)
        : base(name, description, cost, health, progressLevel, attackDamage)
    {
    }

    protected override void Awake()
    {
        SetStructureName("Turret");
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    //upgrades
    //slot0
    protected override void Slot0UpgradeLevel1()
    {
        attackDamage += 20;
    }

    protected override void Slot0UpgradeLevel2()
    {
        attackDamage += 20;
    }

    protected override void Slot0UpgradeLevel3()
    {
        attackDamage += 20;
    }

    protected override void Slot0UpgradeLevel4()
    {
        attackDamage += 20;
    }

    protected override void Slot0UpgradeLevel5()
    {
        attackDamage += 20;
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
