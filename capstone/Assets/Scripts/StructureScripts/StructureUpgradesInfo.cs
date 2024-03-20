using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureUpgradesInfo : MonoBehaviour
{
    private DatabaseWrapper databaseWrapper;
    private UpgradeStructuresSystem upgradeSystem;
    private StructureInfo structureInfo;

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
        
    }

    //not complete
    public void Upgrade(int upgradeButtonIdx) {
        Debug.Log("UPGRADE: " + upgradeButtonIdx.ToString());
        totalUpgrades++;
        upgradeLevels[upgradeButtonIdx]++;
        
    }

    public int GetCost(int slotIndex) {
        int currentUpgradeLevel = upgradeLevels[slotIndex];
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

}
