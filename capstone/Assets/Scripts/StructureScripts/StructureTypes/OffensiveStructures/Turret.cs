using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Shooter
{
    public Turret(string name, string description, int cost, int health, float attackSpeed, float attackDamage, float critChance, float critDamage)
        : base(name, description, cost, health, attackSpeed, attackDamage, critChance, critDamage) {
    }

    protected override void Start()
    {
        base.Start();
        SetStructureName("Turret");
        SetDescription("The basic shooting structure.");
    }
    
}
