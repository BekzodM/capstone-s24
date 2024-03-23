using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Shooter
{
    public Turret(string name, string description, int cost, int health, int progressLevel, int attackDamage)
        : base(name, description, cost, health, progressLevel, attackDamage) {
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
        throw new System.NotImplementedException();
    }

    protected override void Slot0UpgradeLevel2()
    {
        throw new System.NotImplementedException();
    }

    protected override void Slot0UpgradeLevel3()
    {
        throw new System.NotImplementedException();
    }

    protected override void Slot0UpgradeLevel4()
    {
        throw new System.NotImplementedException();
    }

    protected override void Slot0UpgradeLevel5()
    {
        throw new System.NotImplementedException();
    }
    //slot1
    protected override void Slot1UpgradeLevel1()
    {
        throw new System.NotImplementedException();
    }

    protected override void Slot1UpgradeLevel2()
    {
        throw new System.NotImplementedException();
    }

    protected override void Slot1UpgradeLevel3()
    {
        throw new System.NotImplementedException();
    }

    protected override void Slot1UpgradeLevel4()
    {
        throw new System.NotImplementedException();
    }

    protected override void Slot1UpgradeLevel5()
    {
        throw new System.NotImplementedException();
    }
    //slot2
    protected override void Slot2UpgradeLevel1()
    {
        throw new System.NotImplementedException();
    }

    protected override void Slot2UpgradeLevel2()
    {
        throw new System.NotImplementedException();
    }

    protected override void Slot2UpgradeLevel3()
    {
        throw new System.NotImplementedException();
    }

    protected override void Slot2UpgradeLevel4()
    {
        throw new System.NotImplementedException();
    }

    protected override void Slot2UpgradeLevel5()
    {
        throw new System.NotImplementedException();
    }
}
