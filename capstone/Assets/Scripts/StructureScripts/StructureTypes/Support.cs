using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Support : Structure
{
    protected Support(string name, string description, int cost, int health, int progressLevel, int attackDamage)
        : base(name, description, "Support", cost, health, progressLevel, attackDamage)
    {
    }

    protected override void Start()
    {
        base.Start();
    }

}
