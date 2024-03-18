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
    [SerializeField] protected int progressLevel;
    [SerializeField] protected int attackDamage;
    protected int structureWorth;
    protected int[] upgradeAmounts = {0,0,0};

    protected DatabaseWrapper databaseWrapper;

    /*
    protected Dictionary<int, Dictionary<int,Dictionary<string,string>>> upgrades;
    protected Dictionary<int, Dictionary<string, string>> upgradeLevels;
    protected Dictionary<string, string> upgradeLevelInfo;
    */

    protected Structure(string name, string description, string type, int cost, int health, int progressLevel, int attackDamage) 
    {
        structureName = name;
        this.description = description;
        structureType = type;
        this.cost = cost;
        this.health = health;
        this.progressLevel = progressLevel;
        structureWorth = cost;
    }

    protected virtual void Awake() {
        databaseWrapper = new DatabaseWrapper();
    }

    protected virtual void Start() {
        //SetHealth(health);
        //SetCost(cost);
        gameObject.tag = "Structure";
        gameObject.layer = LayerMask.NameToLayer("Draggable");
        SetStructureProperties();

        /*
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
        */
    }

    protected virtual void SetStructureProperties() {
        string[,] results = databaseWrapper.GetData("structures", "structure_name", structureName);
        //REMINDER: SET THE STRUCTURE DESCRIPTION WHEN IT HAS BEEN ADDED TO THE STRUCTURES TABLE
        SetStructureType(results[0,2]);
        SetDescription(results[0,3]);
        SetAttackDamage(int.Parse(results[0,4]));
        SetHealth(int.Parse(results[0,5]));
        SetCost(int.Parse(results[0,6]));
        SetProgressLevel(int.Parse(results[0,7]));
        SetStructureWorth(cost);
    }

    public void TakeDamage(int damage) {
        if (health - damage <= 0) {
            SetHealth(0);
            PlaceStructure placeStructComponent = FindObjectOfType<PlaceStructure>();
            if (placeStructComponent != null) {
                placeStructComponent.RemoveStructurePlacement(gameObject);
            }
            Destroy(gameObject);
        }
        else{
            SetHealth(health-damage);
        }
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

    public int GetProgressLevel()
    {
        return progressLevel;
    }

    public int GetStructureWorth() {
        return structureWorth;
    }

    public int GetAttackDamage() {
        return attackDamage;
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

    protected void SetProgressLevel(int level) {
        progressLevel= level;
    }

    protected void SetStructureWorth(int worth) {
        structureWorth = worth;
    }

    protected void SetAttackDamage(int damage) {
        attackDamage = damage;
    }

    //Structure Upgrades
 
    //upgradeIdx = the index used to get the upgradeAmounts in the upgradeAmounts list
    /*
    protected void IncreaseUpgradeLevel(int upgradeIdx) {
        upgradeAmounts[upgradeIdx] += 1;
        if (upgradeAmounts[upgradeIdx] > 5) {
            upgradeAmounts[upgradeIdx] = 5;
        }

    }
    */
}
