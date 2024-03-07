using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Shooter
{
    public Turret(string name, string description, int cost, int health, int attackDamage)
        : base(name, description, cost, health, attackDamage) {
    }

    protected override void Start()
    {
        base.Start();
        SetStructureName("Turret");
        SetDescription("The basic shooting structure.");
        SetAttackDamage(attackDamage);
    }

    protected override void UseUpgrade1() {
        Debug.Log("Upgrade1");
        //More damage
    }
    protected override void UseUpgrade2() {
        Debug.Log("Upgrade2");
        //Faster
    }
    protected override void UseUpgrade3() {
        Debug.Log("Upgrade3");
        //Wider Range
    }

}
