using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    //Planning Phase UI
    public GameObject planningPhaseUI;

    //World Space Canvas
    public GameObject worldSpaceCanvas;

    //Message to player
    public GameObject messagePanel;

    //Database
    private DatabaseWrapper databaseWrapper = new DatabaseWrapper();

    //Money
    [SerializeField] private int startingMoney = 100;

    //percentage of how much the player gets for selling a structure
    [SerializeField] private float sellingPercentage = 0.7f;
    
    private int money;

    //Waves
    private int currentWave = 1;
    [SerializeField] public int totalWaves = 5;

    //Base HP
    [SerializeField] private int startingBaseHealth = 200;
    [SerializeField] private int maxBaseHealth = 200;
    private int baseHealth;

    // Start is called before the first frame update
    void Start()
    {
        money = startingMoney;
        baseHealth = startingBaseHealth;
    }

    //Money Functions
    public int GetStartingMoney()
    {
        return startingMoney;
    }
    public int GetMoney()
    {
        return money;
    }

    public void SetMoney(int amount) {
        money = amount;
    }

    public void AddMoney(int increase)
    {
        MoneyText moneyText = planningPhaseUI.GetComponentInChildren<MoneyText>();
        money += increase;
        moneyText.ChangeMoneyText(money);
    }

    public void SubtractMoney(int decrease)
    {
        MoneyText moneyText = planningPhaseUI.GetComponentInChildren<MoneyText>();
        money -= decrease;
        moneyText.ChangeMoneyText(money);
    }

    public bool CanPurchase(string structureName) {
        string[,] results = databaseWrapper.GetData("structures", "structure_name", structureName);

        int structCost = Int32.Parse(results[0, 7]);
        bool canPurchase = false;


        if (money >= structCost)
        {
            canPurchase = true;
        }
        return canPurchase;
    }
    public void Purchase(string structureName)
    {
        string[,] results = databaseWrapper.GetData("structures", "structure_name", structureName);
        string structName = results[0, 1];
        int structCost = Int32.Parse(results[0,7]);
        PlaceStructure placeStructureComponent = planningPhaseUI.GetComponent<PlaceStructure>();
        bool isPlacingStructure = placeStructureComponent.GetIsPlacingStructure();
        if (money >= structCost)
        {
            if (isPlacingStructure == false)
            {
                //Player can buy it and they are not currently placing a structure down
                SubtractMoney(structCost);
            }
            else {
                Debug.Log("player must place down the structure first");
                messagePanel.GetComponent<Message>().SetMessageText("You must place down the structure first.");
            }
        }
        else {
            messagePanel.GetComponent<Message>().SetMessageText("Not enough money for " + structName);
            //Insufficent funds
            /*
            Debug.Log("Not enough money for " + structName);
            messagePanel.GetComponent<Message>().SetMessageText("Not enough money for " + structName);
            GameObject obj = planningPhaseUI.GetComponent<DragStructures>().GetSelectedObject();
            obj.GetComponentInChildren<WorldSpaceCanvas>().ResetWorldCanvas();
            Destroy(obj);
            */
        }
    }

    public void OnClickSellButton() {
        GameObject selectedObj = planningPhaseUI.GetComponent<DragStructures>().GetSelectedObject();
        if (selectedObj == null)
        {
            Debug.Log("No selected object for selling");
        }
        else {
            //Sell(selectedObj);
            WorldSpaceCanvas canvas = worldSpaceCanvas.GetComponent<WorldSpaceCanvas>();
            canvas.ShowSellConfirmationPanel(true);
            canvas.ShowPlacementConfirmationPanel(false);
        }
    }

    public void OnClickCancelSellConfirmationButton()
    {
        WorldSpaceCanvas canvas = worldSpaceCanvas.GetComponent<WorldSpaceCanvas>();
        canvas.ShowSellConfirmationPanel(false);
        canvas.ShowPlacementConfirmationPanel(false);
        Tooltip.HideTooltip();
    }

    public void Sell() {
        GameObject obj = planningPhaseUI.GetComponent<DragStructures>().GetSelectedObject();
        if (obj == null)
        {
            Debug.Log("No selected object for selling");
        }
        else {
            int sellingValue = Mathf.RoundToInt(sellingPercentage * obj.GetComponent<Structure>().GetStructureWorth());
            //Refund 70% of structure's worth value
            AddMoney(sellingValue);

            //Remove from placement's set
            planningPhaseUI.GetComponent<PlaceStructure>().RemoveStructurePlacement(obj);
        
            //ReparentWorldSpaceCanvas
            worldSpaceCanvas.GetComponent<WorldSpaceCanvas>().ResetWorldCanvas();

            //Destroy Structure
            planningPhaseUI.GetComponent<DragStructures>().DestroySelectedObject();

            //Hide structure Info
            StructureInfo structureInfo = planningPhaseUI.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<StructureInfo>();
            structureInfo.MakeActive(false);
        }
        Tooltip.HideTooltip();

    }

    //Wave Functions
    public int GetCurrentWaveNumber()
    {
        return currentWave;
    }

    public int GetTotalWaveNumber()
    {
        return totalWaves;
    }

    public void SetWave(int waveNumber)
    {
        WaveText waveText = planningPhaseUI.GetComponentInChildren<WaveText>();
        waveText.ChangeMoneyText(waveNumber, totalWaves);
    }

    public int GetTotalWaves()
    {
        return totalWaves;
    }

    public void IncreaseCurrentWaveNumber(int increase)
    {

        if (currentWave + increase > totalWaves)
        {
            Debug.Log("Current wave number cannot exceed total waves");
        }
        else
        {
            currentWave += increase;
            WaveText waveText = planningPhaseUI.GetComponentInChildren<WaveText>();
            waveText.ChangeMoneyText(currentWave, totalWaves);
        }
    }

    public void DecreaseCurrentWaveNumber(int decrease)
    {
        if (currentWave - decrease < 1)
        {
            Debug.Log("Current wave number cannot be lower than one");
        }
        else
        {
            currentWave -= decrease;
            WaveText waveText = planningPhaseUI.GetComponentInChildren<WaveText>();
            waveText.ChangeMoneyText(currentWave, totalWaves);
        }
    }

    public void IncreaseTotalWaves(int increase)
    {
        totalWaves += increase;
    }

    public void DecreaseTotalWaves(int decrease)
    {
        if (totalWaves - decrease < currentWave)
        {
            Debug.Log("Total amount of waves cannot be lower than the current number of waves");
        }
        else if (totalWaves - decrease < 1)
        {
            Debug.Log("Total amount of waves cannot be lower than 1");
        }
        else
        {
            totalWaves -= decrease;
            WaveText waveText = planningPhaseUI.GetComponentInChildren<WaveText>();
            waveText.ChangeMoneyText(currentWave, totalWaves);
        }
    }

    //Base HP Functions
    public int GetStartingBaseHealth()
    {
        return startingBaseHealth;
    }

    public int GetMaxBaseHealth()
    {
        return maxBaseHealth;
    }

    public int GetBaseHealth() {
        return baseHealth;
    }

    public void IncreaseBaseHealth(int increase)
    {
        if (baseHealth + increase > maxBaseHealth)
        {
            Debug.Log("Base health cannot exceed max base health");
        }
        else
        {
            baseHealth += increase;
            BaseHealthText baseHealthText = planningPhaseUI.GetComponentInChildren<BaseHealthText>();
            baseHealthText.ChangeBaseHealthText(baseHealth, maxBaseHealth);

        }
    }

    public void DecreaseBaseHealth(int decrease)
    {
        if (baseHealth - decrease < 0)
        {
            //MAP LEVEL FAILED. CODE TO MAP LEVEL FAILED SCREEN.
            baseHealth = 0;
        }
        baseHealth -= decrease;
        BaseHealthText baseHealthText = planningPhaseUI.GetComponentInChildren<BaseHealthText>();
        baseHealthText.ChangeBaseHealthText(baseHealth, maxBaseHealth);
    }

    public void IncreaseMaxBaseHealth(int increase)
    {
        maxBaseHealth += increase;
        BaseHealthText baseHealthText = planningPhaseUI.GetComponentInChildren<BaseHealthText>();
        baseHealthText.ChangeBaseHealthText(baseHealth, maxBaseHealth);
    }

    public void DecreaseMaxBaseHealth(int decrease)
    {
        if (maxBaseHealth - decrease < baseHealth)
        {
            Debug.Log("Max Base Health cannot be lower than the current baseHealth");

        }
        else if (maxBaseHealth - decrease <= 0)
        {
            Debug.Log("Max Base Health cannot be lower or equal to zero");
        }
        else
        {
            maxBaseHealth -= decrease;
            BaseHealthText baseHealthText = planningPhaseUI.GetComponentInChildren<BaseHealthText>();
            baseHealthText.ChangeBaseHealthText(baseHealth, maxBaseHealth);
        }
    }
}
