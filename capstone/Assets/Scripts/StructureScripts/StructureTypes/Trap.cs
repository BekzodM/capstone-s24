using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trap : Structure
{
    protected Trap(string name, string description, int cost, int health, float areaEffectRadius)
        : base(name, description, "Trap", cost, health, areaEffectRadius)
    {
    }

}
