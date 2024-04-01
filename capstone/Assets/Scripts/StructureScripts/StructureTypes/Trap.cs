using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trap : Structure
{
    protected Trap(string name, string description, int cost, int health, int progressLevel, int attackDamage)
        : base(name, description, "Trap", cost, health, progressLevel, attackDamage)
    {
    }

    protected override void Start()
    {
        base.Start();
    }

}
