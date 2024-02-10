using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Defensive
{
    public Wall(int cost, int health, float areaEffectRadius)
        : base("Wall", "A basic wall", cost, health, areaEffectRadius)
    {
    }
}
