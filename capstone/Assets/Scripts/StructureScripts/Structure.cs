using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Structure
{
    protected string name;
    protected string description;
    protected string type;
    protected int cost;
    protected int health;
    protected float areaEffectRadius;

    protected Structure(string name, string description, string type, int cost, int health, float areaEffectRadius) 
    {
        this.name = name;
        this.description = description;
        this.type = type;
        this.cost = cost;
        this.health = health;
        this.areaEffectRadius = areaEffectRadius;
    }
}
