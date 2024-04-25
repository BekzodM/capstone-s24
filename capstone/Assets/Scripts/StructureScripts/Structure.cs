using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UpgradeStructureFunction;
public abstract class Structure : MonoBehaviour
{
    [SerializeField] protected string structureName;
    [SerializeField] protected string description;
    [SerializeField] protected string structureType;
    [SerializeField] protected int cost;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int health;
    [SerializeField] protected int progressLevel;
    [SerializeField] protected int attackDamage;
    protected int structureWorth;
    protected int structureId;
    protected string imagePath;
    protected GameObject areaZone;

    protected HealthBar healthBar;

    protected UpgradeFunction[] upgradeFunctions;

    protected DatabaseWrapper databaseWrapper;

    protected StructureUpgradesInfo upgradesInfo;

    protected TooltipHover tooltipHover;

    protected GameObject planningPhaseManager;

    protected PlaceStructure placeStruct;

    protected bool isDead = false;

    protected Structure(string name, string description, string type, int cost, int health, int progressLevel, int attackDamage)
    {
        structureName = name;
        this.description = description;
        structureType = type;
        this.cost = cost;
        this.maxHealth = health;
        this.health = maxHealth;
        this.progressLevel = progressLevel;
        structureWorth = cost;
    }

    protected virtual void Awake()
    {
        areaZone = transform.GetChild(0).gameObject;

        healthBar = transform.GetChild(2).GetChild(0).GetComponent<HealthBar>();

        databaseWrapper = new DatabaseWrapper();

        upgradeFunctions = new UpgradeFunction[15] {
            Slot0UpgradeLevel1,
            Slot0UpgradeLevel2,
            Slot0UpgradeLevel3,
            Slot0UpgradeLevel4,
            Slot0UpgradeLevel5,
            Slot1UpgradeLevel1,
            Slot1UpgradeLevel2,
            Slot1UpgradeLevel3,
            Slot1UpgradeLevel4,
            Slot1UpgradeLevel5,
            Slot2UpgradeLevel1,
            Slot2UpgradeLevel2,
            Slot2UpgradeLevel3,
            Slot2UpgradeLevel4,
            Slot2UpgradeLevel5,
        };

        planningPhaseManager = FindFirstObjectByType<PlanningPhaseManager>().gameObject;
        if (planningPhaseManager == null) {
            Debug.LogError("Cannot find planning phase manager");
        }

        placeStruct = FindObjectOfType<PlaceStructure>();
        if (placeStruct == null)
        {
            Debug.LogError("Cannot find Place Structure");
        }
    }

    protected virtual void Start()
    {
        gameObject.tag = "Structure";
        gameObject.layer = LayerMask.NameToLayer("Draggable");
        SetStructureProperties();
        upgradesInfo = gameObject.AddComponent<StructureUpgradesInfo>();
        tooltipHover = gameObject.AddComponent<TooltipHover>();
        tooltipHover.type = TooltipHover.HoverType.Structure;
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

    }

    protected virtual void SetStructureProperties()
    {
        //structure properties
        string[,] results = databaseWrapper.GetData("structures", "structure_name", structureName);
        SetStructureId(int.Parse(results[0,0]));
        SetStructureType(results[0,2]);
        SetDescription(results[0,3]);
        SetImagePath(results[0,4]);
        SetAttackDamage(int.Parse(results[0,5]));
        SetMaxHealth(int.Parse(results[0,6]));
        SetCost(int.Parse(results[0,7]));
        SetProgressLevel(int.Parse(results[0,8]));
        SetStructureWorth(cost);
    }

    public void TakeDamage(int damage) {
        if (damage <= 0) {
            Debug.LogError("Damage must be greater than 0");
        }

        if (health - damage <= 0 && isDead == false) {
            SetHealth(0);
            healthBar.SetHealth(0);

            if (placeStruct != null)
            {
                //placeStruct.RemoveStructurePlacement(gameObject);
                isDead = true;
                Destroy(gameObject);
                placeStruct.RemoveStructurePlacement(gameObject);
                    
            }
            else {
                Debug.LogError("place structure is missing");
            }

        }
        else{
            health -= damage;
            healthBar.SetHealth(health);
        }
    }

    public void HealHealth(int heal) {
        if (heal <= 0) {
            Debug.LogError("Heal must be greater than 0");
        }
        if (health + heal > maxHealth)
        {
            SetMaxHealth(maxHealth);
        }
        else {
            health += heal;
        }
    }

    //Getters
    public int GetStructureId()
    {
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

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetHealth() {
        return health;
    }

    public int GetProgressLevel()
    {
        return progressLevel;
    }

    public int GetStructureWorth()
    {
        return structureWorth;
    }

    public int GetAttackDamage()
    {
        return attackDamage;
    }

    public string GetImagePath()
    {
        return imagePath;
    }

    public UpgradeFunction[] GetUpgradeFunctions()
    {
        return upgradeFunctions;
    }

    public UpgradeFunction GetUpgradeFunction(int index)
    {
        return upgradeFunctions[index];
    }

    public float GetAreaZoneRadius()
    {
        return areaZone.GetComponent<AreaZone>().GetAreaEffectRadius();
    }

    public string[] GetStructureInfo()
    {
        string[] structureInfo = new string[6];
        structureInfo[0] = GetStructureName();
        structureInfo[1] = GetStructureType();
        structureInfo[2] = GetDescription();
        structureInfo[3] = "Health: " + GetMaxHealth().ToString();
        structureInfo[4] = "Attack: " + GetAttackDamage().ToString();
        structureInfo[5] = "Worth: " + GetStructureWorth().ToString();
        return structureInfo;
    }

    //Setters

    protected void SetStructureId(int id)
    {
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

    protected void SetMaxHealth(int h)
    {
        maxHealth = h;
    }

    protected void SetHealth(int h) {
        health = h;
    }

    protected void SetProgressLevel(int level)
    {
        progressLevel = level;
    }

    public void SetStructureWorth(int worth)
    {
        structureWorth = worth;
    }

    protected void SetAttackDamage(int damage)
    {
        attackDamage = damage;
    }

    protected void SetImagePath(string path)
    {
        imagePath = path;
    }

    //Area Zone methods
    protected void SetAreaZoneRadius(float radius)
    {
        areaZone.GetComponent<AreaZone>().SetAreaEffectRadius(radius);
    }

    public void ShowAreaZone(bool show)
    {
        //areaZone.SetActive(show);
        areaZone.GetComponent<MeshRenderer>().enabled = show;

    }

    public void ActivateAreaZoneCollider(bool isActive) { 
        areaZone.GetComponent<Collider>().enabled = isActive;

    }

    //audio
    protected void PlayAudio()
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null) {
            audio.Play();
        }

    }


    //Abstract methods upgrade functions
    //slot 0 upgrades
    protected abstract void Slot0UpgradeLevel1();
    protected abstract void Slot0UpgradeLevel2();
    protected abstract void Slot0UpgradeLevel3();
    protected abstract void Slot0UpgradeLevel4();
    protected abstract void Slot0UpgradeLevel5();
    //slot1 upgrades
    protected abstract void Slot1UpgradeLevel1();
    protected abstract void Slot1UpgradeLevel2();
    protected abstract void Slot1UpgradeLevel3();
    protected abstract void Slot1UpgradeLevel4();
    protected abstract void Slot1UpgradeLevel5();
    //slot2 upgrades
    protected abstract void Slot2UpgradeLevel1();
    protected abstract void Slot2UpgradeLevel2();
    protected abstract void Slot2UpgradeLevel3();
    protected abstract void Slot2UpgradeLevel4();
    protected abstract void Slot2UpgradeLevel5();

}
