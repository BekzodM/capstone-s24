using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Structure : MonoBehaviour
{
    [SerializeField] protected string structureName;
    [SerializeField] protected string description;
    [SerializeField] protected string structureType;
    [SerializeField] protected int cost;
    [SerializeField] protected int health;

    protected Structure(string name, string description, string type, int cost, int health) 
    {
        structureName = name;
        this.description = description;
        structureType = type;
        this.cost = cost;
        this.health = health;
    }

    protected virtual void Start() {
        SetHealth(health);
        SetCost(cost);
        gameObject.tag = "Structure";
    }

    //Getters
    public string GetStructureName()
    {
        return structureName;
    }

    public string GetDescription()
    {
        return description;
    }

    public string GetStructureType()
    {
        return structureType;
    }

    public int GetCost()
    {
        return cost;
    }

    public int GetHealth()
    {
        return health;
    }

    //Setters
    protected void SetStructureName(string structName)
    {
        structureName = structName;
    }

    protected void SetDescription(string desc)
    {
        description = desc;
    }

    protected void SetStructureType(string structType)
    {
        structureType = structType;
    }

    protected void SetCost(int c)
    {
        cost = c;
    }

    protected void SetHealth(int h)
    {
        health = h;
    }

    //Structure Upgrades
    protected abstract void UseUpgrade1();
    protected abstract void UseUpgrade2();
    protected abstract void UseUpgrade3();
}
