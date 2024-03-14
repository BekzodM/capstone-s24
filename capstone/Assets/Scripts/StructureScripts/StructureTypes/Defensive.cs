using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Defensive : Structure
{
    protected Defensive(string name, string description, int cost, int health, int progressLevel, int attackDamage)
        : base(name, description, "Defensive", cost, health, progressLevel, attackDamage)
    {
    }

    protected override void Start()
    {
        base.Start();
    }

}
