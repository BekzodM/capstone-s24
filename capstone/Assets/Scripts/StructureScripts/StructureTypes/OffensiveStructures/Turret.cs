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
        
        //SetDescription("The basic shooting structure.");
        //SetAttackDamage(attackDamage);
    }

}
