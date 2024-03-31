using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UpgradeStructureFunction;

public class StructureUpgradesInfo : MonoBehaviour
{
    private DatabaseWrapper databaseWrapper;
    private UpgradeStructuresSystem upgradeSystem;
    private StructureInfo structureInfo;

    private Dictionary<int, UpgradeFunction> upgradeDictionary;

    [SerializeField] private float upgradeWorthPercentageIncrease = 0.3f;

    private int structureId;
    
    //amount of total upgrades on the structure
    private int totalUpgrades;
    
    //contains the upgrade levels of the structure. 0 means no upgrades bought.
    private int[] upgradeLevels; 

    //set of slots that are blocked and disabled
    HashSet<int> blockedSlot;

    //all database info for the structure upgrades
    private string[,] databaseUpgradesInfo;

    //subarray that contains upgrades for slot 0
    private string[,] upgradeSlot0;

    //subarray that contains upgrades for slot 0
    private string[,] upgradeSlot1;

    //subarray that contains upgrades for slot 0
    private string[,] upgradeSlot2;


    private void Awake()
    {
        databaseWrapper = new DatabaseWrapper();
        totalUpgrades= 0;
        upgradeLevels= new int[3];
        upgradeSystem= FindFirstObjectByType<UpgradeStructuresSystem>();
        structureInfo= FindFirstObjectByType<StructureInfo>();
        upgradeDictionary= new Dictionary<int, UpgradeFunction>();
    }
    
    void Start()
    {
        structureId = GetComponent<Structure>().GetStructureId();
        blockedSlot= new HashSet<int>();
        databaseUpgradesInfo = databaseWrapper.GetData("structureUpgrades", "structure_id", structureId);

        //populate upgradeSlot0 2d string array
        upgradeSlot0 = new string[5, 7];
        for (int i = 0; i < 5; i++) {//rows
            for (int j = 0; j < 7; j++) { //columns
                upgradeSlot0[i, j] = databaseUpgradesInfo[i, j];
            }
        }

        //populate upgradeSlot1 2d string array
        upgradeSlot1 = new string[5, 7];
        for (int i = 0; i < 5; i++)
        {//rows
            for (int j = 0; j < 7; j++)
            { //columns
                upgradeSlot1[i, j] = databaseUpgradesInfo[5 + i, j];
            }
        }

        //populate upgradeSlot2 2d string array
        upgradeSlot2 = new string[5, 7];
        for (int i = 0; i < 5; i++)
        {//rows
            for (int j = 0; j < 7; j++)
            { //columns
                upgradeSlot2[i, j] = databaseUpgradesInfo[10 + i, j];
            }
        }

        //populate upgradeDictionary
        Structure structureComponent = GetComponent<Structure>();

        for (int i = 0; i < 15; i++) {
            UpgradeFunction upgradeFunction = structureComponent.GetUpgradeFunction(i);
            int upgradeId = int.Parse(databaseUpgradesInfo[i,0]);
            upgradeDictionary[upgradeId] = upgradeFunction;
        }
    }

    public void Upgrade(int upgradeButtonIdx) {
        Debug.Log("UPGRADE: " + upgradeButtonIdx.ToString());
        int currentLevel = upgradeLevels[upgradeButtonIdx];
        int upgradeId = 0;
        if (currentLevel < 5)
        {
            totalUpgrades++;
            upgradeLevels[upgradeButtonIdx]++;

            //increasing structure worth from upgrades
            int oldStructureWorth = gameObject.GetComponent<Structure>().GetStructureWorth();
            int newStructureWorth = Mathf.RoundToInt(upgradeWorthPercentageIncrease * oldStructureWorth + oldStructureWorth);
            gameObject.GetComponent<Structure>().SetStructureWorth(newStructureWorth);

            //invoke Upgrade Function's effect]

            if (upgradeButtonIdx == 0)
            {
                upgradeId = int.Parse(upgradeSlot0[currentLevel, 0]);
            }
            if (upgradeButtonIdx == 1)
            {
                upgradeId = int.Parse(upgradeSlot1[currentLevel, 0]);
            }
            if (upgradeButtonIdx == 2)
            {
                upgradeId = int.Parse(upgradeSlot2[currentLevel, 0]);
            }

            if (upgradeId != 0)
            {
                InvokeFunction(upgradeId);
            }
            else {
                Debug.LogError("Invalid upgradeId index");
            }
            
        }
        else {
            Debug.LogError("Current Upgrade is already maxed!");
        }

    }

    public void InvokeFunction(int key)
    {
        if (upgradeDictionary.ContainsKey(key))
        {
            upgradeDictionary[key]();
        }
        else
        {
            Console.WriteLine("No function found for key: " + key);
        }
    }

    public int GetCost(int slotIndex) {
        int currentUpgradeLevel = upgradeLevels[slotIndex];
        if (currentUpgradeLevel == 5) {
            Debug.Log("reached max level");
            return -1;
        }
        if (slotIndex == 0)
        {
            return int.Parse(upgradeSlot0[currentUpgradeLevel,5]);
        }
        else if (slotIndex == 1)
        {
            return int.Parse(upgradeSlot1[currentUpgradeLevel, 5]);
        }
        else if (slotIndex == 2)
        {
            return int.Parse(upgradeSlot2[currentUpgradeLevel, 5]);
        }
        else {
            Debug.LogError("Invalid upgrade slot index");
            return -1;
        }
    }

    public int GetTotalUpgrades() { 
        return totalUpgrades;
    }

    public int[] GetUpgradeLevels() { 
        return upgradeLevels;
    }

    public HashSet<int> GetBlockedSlots() {
        return blockedSlot;
    }

    public string GetSlotImagePath(int slotIndex) {
        int currentLevel = upgradeLevels[slotIndex];
        string imagePath = "";

        if (slotIndex == 0)
        {
            if (currentLevel == 5)
            {
                Debug.Log("Image path of level 5 is the last upgrade image");
                imagePath = upgradeSlot0[4, 3];
            }
            else
            {
                imagePath = upgradeSlot0[currentLevel, 3];
            }
        }
        else if (slotIndex == 1)
        {
            if (currentLevel == 5)
            {
                Debug.Log("Image path of level 5 is the last upgrade image");
                imagePath = upgradeSlot1[4, 3];
            }
            else
            {
                imagePath = upgradeSlot1[currentLevel, 3];
            }
        }
        else if (slotIndex == 2)
        {
            if (currentLevel == 5)
            {
                Debug.Log("Image path of level 5 is the last upgrade image");
                imagePath = upgradeSlot2[4, 3];
            }
            else
            {
                imagePath = upgradeSlot2[currentLevel, 3];
            }
        }
        else {
            Debug.LogError("Invalid slot index");
        }
        return imagePath;
    }

    public string GetSlotUpgradeName(int slotIndex)
    {
        int currentLevel = upgradeLevels[slotIndex];
        string upgradeName = "";

        if (slotIndex == 0)
        {
            if (currentLevel == 5)
            {
                Debug.Log("Upgrade Name of level 5 is the last upgrade name");
                upgradeName = upgradeSlot0[4, 1];
            }
            else
            {
                upgradeName = upgradeSlot0[currentLevel, 1];
            }
        }
        else if (slotIndex == 1)
        {
            if (currentLevel == 5)
            {
                Debug.Log("Upgrade Name of level 5 is the last upgrade name");
                upgradeName = upgradeSlot1[4, 1];
            }
            else
            {
                upgradeName = upgradeSlot1[currentLevel, 1];
            }
        }
        else if (slotIndex == 2)
        {
            if (currentLevel == 5)
            {
                Debug.Log("Upgrade Name of level 5 is the last upgrade name");
                upgradeName = upgradeSlot2[4, 1];
            }
            else
            {
                upgradeName = upgradeSlot2[currentLevel, 1];
            }
        }
        else
        {
            Debug.LogError("Invalid slot index");
        }
        return upgradeName;
    }

    public string GetSlotUpgradeDescription(int slotIndex)
    {
        int currentLevel = upgradeLevels[slotIndex];
        string upgradeDesc = "";

        if (slotIndex == 0)
        {
            if (currentLevel == 5)
            {
                Debug.Log("Upgrade Name of level 5 is the last upgrade description");
                upgradeDesc = upgradeSlot0[4, 2];
            }
            else
            {
                upgradeDesc = upgradeSlot0[currentLevel, 2];
            }
        }
        else if (slotIndex == 1)
        {
            if (currentLevel == 5)
            {
                Debug.Log("Upgrade Name of level 5 is the last upgrade description");
                upgradeDesc = upgradeSlot1[4, 2];
            }
            else
            {
                upgradeDesc = upgradeSlot1[currentLevel, 2];
            }
        }
        else if (slotIndex == 2)
        {
            if (currentLevel == 5)
            {
                Debug.Log("Upgrade Name of level 5 is the last upgrade description");
                upgradeDesc = upgradeSlot2[4, 2];
            }
            else
            {
                upgradeDesc = upgradeSlot2[currentLevel, 2];
            }
        }
        else
        {
            Debug.LogError("Invalid slot index");
        }
        return upgradeDesc;
    }


    public int GetCurrentUpgradeLevel(int slotIndex) { 
        return upgradeLevels[slotIndex];
    }

    public string[] GetUpgradeSlotInfo(int slotIndex)
    {
        string[] upgradeSlotInfo = new string[4];
        upgradeSlotInfo[0] = GetSlotUpgradeName(slotIndex);
        upgradeSlotInfo[1] = GetSlotUpgradeDescription(slotIndex);
        upgradeSlotInfo[2] = GetCost(slotIndex).ToString();
        upgradeSlotInfo[3] = GetCurrentUpgradeLevel(slotIndex).ToString();
        return upgradeSlotInfo;
    }

    public void AddBlockedSlot(int slotIndex) {
        blockedSlot.Add(slotIndex);
    }
}
