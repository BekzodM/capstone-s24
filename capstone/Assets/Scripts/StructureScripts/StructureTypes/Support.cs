using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Support : Structure
{
    protected Support(string name, string description, int cost, int health, float areaEffectRadius)
        : base(name, description, "Support", cost, health, areaEffectRadius)
    {
    }

}
