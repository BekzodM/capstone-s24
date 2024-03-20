using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStructuresSystem : MonoBehaviour
{
    [SerializeField] GameObject upgradeButtonContent;
    private GameObject planningPhaseUI;
    private PlaceStructure placeStructure;
    private DragStructures dragStructures;

    public delegate void FunctionDelegate();
    private Dictionary<int, FunctionDelegate> functionDictionary;

    // Start is called before the first frame update
    void Start()
    {
        planningPhaseUI = transform.parent.parent.parent.parent.gameObject;
        placeStructure = planningPhaseUI.GetComponent<PlaceStructure>();
        dragStructures = planningPhaseUI.GetComponent<DragStructures>();
        functionDictionary = new Dictionary<int, FunctionDelegate>();
    }

    //Upgrade UI Button Functions
    public void OnClickUpgradeButton(int idx) {
        //only show confirm panel if the player is not placing a structure
        if (placeStructure.CheckStructurePlacement(dragStructures.GetSelectedObject())) {
            GameObject upgradeButton = upgradeButtonContent.transform.GetChild(idx + 1).gameObject;
            GameObject confirmPanel = upgradeButton.transform.GetChild(1).gameObject;
            confirmPanel.SetActive(true);
        }
    }

    public void OnClickConfirmUpgrade(int idx) {
        Debug.Log("Confirm button for upgrade: " + idx);
        TryToBuyUpgrade(idx);
        OnClickCancelUpgrade(idx);
    }

    public void OnClickCancelUpgrade(int idx) {
        GameObject upgradeButton = upgradeButtonContent.transform.GetChild(idx + 1).gameObject;
        GameObject confirmPanel = upgradeButton.transform.GetChild(1).gameObject;
        confirmPanel.SetActive(false);

    }

    //Upgrade System Functions
    public void InvokeFunction(int key)
    {
        if (functionDictionary.ContainsKey(key))
        {
            functionDictionary[key]();
        }
        else
        {
            Console.WriteLine("No function found for key: " + key);
        }
    }

    private void TryToBuyUpgrade(int slotIndex) {
        MapManager mapManager = placeStructure.mapManager.GetComponent<MapManager>();
        int currentMoney = mapManager.GetMoney();
        GameObject currentObj = dragStructures.GetSelectedObject();
        if (currentObj != null)
        {
            StructureUpgradesInfo upgradesInfo = currentObj.GetComponent<StructureUpgradesInfo>();
            int upgradeCost = upgradesInfo.GetCost(slotIndex);

            if (currentMoney >= upgradeCost)
            { //enough money?
                if (upgradesInfo.GetTotalUpgrades() < 5)
                { //less than 5 total upgrades? 
                    if (!upgradesInfo.GetBlockedSlots().Contains(slotIndex))
                    {//slot is not blocked?
                        upgradesInfo.Upgrade(slotIndex);
                        mapManager.SubtractMoney(upgradeCost);
                    }
                    else {
                        Debug.Log("Slot index: " + slotIndex.ToString() + " is a blocked slot");
                    }
                }
                else {
                    Debug.Log("Reached the maximum amount of upgrades. Cannot upgrade more than 5 times.");
                }
            }
            else {
                Debug.Log("Not enough money to upgrade");
            }
        }
        else {
            Debug.LogError("Tried to buy upgrade but the currently selected object is null");
        }
    }

    //When 2 different upgrade options are selected for upgrade, the 3rd upgrade option is blocked off
    //When 1 upgrade is fully upgrade to Level 5, the 2 other upgrade options are blocked off
    //Blocked off upgradeSlots cannot be used for upgrading
    private void BlockSlot(StructureUpgradesInfo upgradesInfo)
    {
        int[] upgradeLevels = upgradesInfo.GetUpgradeLevels();
        Transform content = transform.GetChild(0).GetChild(0);

        GameObject slot0 = content.GetChild(1).gameObject;
        GameObject slot1 = content.GetChild(2).gameObject;
        GameObject slot2 = content.GetChild(3).gameObject;

        CanvasGroup canvasGroup0= slot0.GetComponent<CanvasGroup>();
        CanvasGroup canvasGroup1= slot1.GetComponent<CanvasGroup>();
        CanvasGroup canvasGroup2= slot2.GetComponent<CanvasGroup>();

        int upgradeSlotCounter = 0; //counts how many upgradeSlots have upgrades
        HashSet<int>upgradedSlots= new HashSet<int>();//slots that have upgrades
        
        for (int i = 0; i < upgradeLevels.Length; i++)
        {   //Check if there is a fully upgraded slot
            if (upgradeLevels[i] == 5)
            {
                Debug.Log("Upgrade Slot " + i.ToString() + " is fully upgraded");
                //block off the other 2 slots
                if (i == 0)
                {
                    DisableSlot(canvasGroup1);
                    DisableSlot(canvasGroup2);
                    Debug.Log("slot 1 and 2 cannot be interacted with.");
                }
                else if (i == 1)
                {
                    DisableSlot(canvasGroup0);
                    DisableSlot(canvasGroup2);
                    Debug.Log("slot 0 and 2 cannot be interacted with.");
                }
                else if (i == 2)
                {
                    DisableSlot(canvasGroup0);
                    DisableSlot(canvasGroup1);
                    Debug.Log("slot 0 and 1 cannot be interacted with.");
                }
                else {
                    Debug.LogError("Invalid index for upgradeLevels");
                }
            }

            //Check if two different upgrade options have been upgraded
            if (upgradeLevels[i] > 0) {
                upgradeSlotCounter++;
                upgradedSlots.Add(i);
            }
        }

        //disable the 3rd slot that hasn't been upgraded
        if (upgradeSlotCounter >= 2) {
            if (!upgradedSlots.Contains(0))
            {
                DisableSlot(canvasGroup0);
            }
            else if (!upgradedSlots.Contains(1))
            {
                DisableSlot(canvasGroup1);
            }
            else if (!upgradedSlots.Contains(2))
            {
                DisableSlot(canvasGroup2);
            }
            else {
                Debug.Log("None of the upgrade slots have been leveled up");
            }
        }
    }

    private void DisableSlot(CanvasGroup canvasGroup) { 
        canvasGroup.interactable = false;
        canvasGroup.alpha = 0.5f;
    }

}
