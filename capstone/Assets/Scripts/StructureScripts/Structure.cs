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
    protected int[] upgradeAmounts = {0,0,0};
    protected Dictionary<int, Dictionary<int,Dictionary<string,string>>> upgrades;
    protected Dictionary<int, Dictionary<string, string>> upgradeLevels;
    protected Dictionary<string, string> upgradeLevelInfo;

    

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
        upgrades = new Dictionary<int, Dictionary<int, Dictionary<string, string>>> {
            {0, upgradeLevels}, //first upgrade of structure
            {1, upgradeLevels},
            {2, upgradeLevels}
        };
        upgradeLevels = new Dictionary<int, Dictionary<string, string>> {
            {0, upgradeLevelInfo}, //level 1 of the first,second, or third upgrade
            {1, upgradeLevelInfo },
            {2, upgradeLevelInfo },
            {3, upgradeLevelInfo },
            {4, upgradeLevelInfo},
        };
        upgradeLevelInfo = new Dictionary<string, string> {
            {"name", ""},
            {"description", ""},
        };
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
    protected abstract void UseUpgrade0();
    protected abstract void UseUpgrade1();
    protected abstract void UseUpgrade2();
}
