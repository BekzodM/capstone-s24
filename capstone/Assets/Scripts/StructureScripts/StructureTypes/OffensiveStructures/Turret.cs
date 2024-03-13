using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Shooter
{
    public Turret(string name, string description, int cost, int health, int progressLevel, int attackDamage)
        : base(name, description, cost, health, progressLevel, attackDamage) {
    }

    protected override void Start()
    {
        base.Start();
        SetStructureName("Turret");
        //SetDescription("The basic shooting structure.");
        //SetAttackDamage(attackDamage);
    }

}
