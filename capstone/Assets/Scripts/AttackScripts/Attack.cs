using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack
{
    protected string name;
    protected int damage;
    protected Attack(string name, int damage) {
        this.name = name;
        this.damage = damage;
    }
}
