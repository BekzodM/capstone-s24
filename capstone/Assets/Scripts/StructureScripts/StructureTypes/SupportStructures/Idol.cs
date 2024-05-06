using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idol : Support
{
    [SerializeField] private int healingAmount;
    public Idol(string name, string description, int cost, int health, int progressLevel, int attackDamage)
    : base("Idol", "", cost, health, progressLevel, attackDamage)
    {
    }

    protected override void Awake() {
        SetStructureName("Idol");
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void SupportAbility() {
        foreach (GameObject allies in alliesInZone)
        {
            if (allies != null)
            {
                if (allies.CompareTag("Structure"))
                {
                    Structure structureComponent = allies.GetComponent<Structure>();
                    if (structureComponent != null)
                    {
                        structureComponent.HealHealth(healingAmount);
                    }
                }
            }
        }
    }

    //upgrades
    //slot0
    protected override void Slot0UpgradeLevel1()
    {
        healingAmount+= 10;
    }

    protected override void Slot0UpgradeLevel2()
    {
        healingAmount+= 10;
    }

    protected override void Slot0UpgradeLevel3()
    {
        healingAmount+= 10;
    }

    protected override void Slot0UpgradeLevel4()
    {
        healingAmount+= 10;
    }

    protected override void Slot0UpgradeLevel5()
    {
        healingAmount+= 10;
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
