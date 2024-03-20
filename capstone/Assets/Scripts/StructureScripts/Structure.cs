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
    protected int structureId;
    protected string imagePath;
    protected DatabaseWrapper databaseWrapper;

    protected StructureUpgradesInfo upgradesInfo;

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
        gameObject.tag = "Structure";
        gameObject.layer = LayerMask.NameToLayer("Draggable");
        SetStructureProperties();
        upgradesInfo= gameObject.AddComponent<StructureUpgradesInfo>();
    }

    protected virtual void SetStructureProperties() {
        //structure properties
        string[,] results = databaseWrapper.GetData("structures", "structure_name", structureName);
        SetStructureId(int.Parse(results[0,0]));
        SetStructureType(results[0,2]);
        SetDescription(results[0,3]);
        SetImagePath(results[0,4]);
        SetAttackDamage(int.Parse(results[0,5]));
        SetHealth(int.Parse(results[0,6]));
        SetCost(int.Parse(results[0,7]));
        SetProgressLevel(int.Parse(results[0,8]));
        SetStructureWorth(cost);
    }

    public void TakeDamage(int damage) {
        if (health - damage <= 0) {
            SetHealth(0);
            PlaceStructure placeStructComponent = FindObjectOfType<PlaceStructure>();
            if (placeStructComponent != null) {
                placeStructComponent.RemoveStructurePlacement(gameObject);
            }
            
            placeStructComponent.RemoveStructurePlacement(gameObject);
            Destroy(gameObject);
        }
        else{
            SetHealth(health-damage);
        }
    }

    //Getters
    public int GetStructureId() {
        return structureId;
    }

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

    public string GetImagePath() {
        return imagePath;
    }

    //Setters

    protected void SetStructureId(int id) {
        structureId = id;
    }
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

    protected void SetImagePath(string path) { 
        imagePath= path;
    }

}
