using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Offensive : Structure
{
    protected Offensive(string name, string description, int cost, int health, float areaEffectRadius)
        : base(name, description, "Offensive", cost, health, areaEffectRadius)
    {
    }
}
