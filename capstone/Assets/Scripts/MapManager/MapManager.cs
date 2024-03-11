using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    //Planning Phase UI
    public GameObject planningPhaseUI;

    //Database
    private DatabaseWrapper databaseWrapper = new DatabaseWrapper();

    //Money
    [SerializeField] private int startingMoney = 100;
    private int money;

    //Waves
    [SerializeField] private int currentWave = 1;
    [SerializeField] private int totalWaves = 5;

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

    // Update is called once per frame
    void Update()
    {
        
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

    public bool CanPurchase(string structureName)
    {
        string[,] results = databaseWrapper.GetData("structures", "structure_name", "Turret");
        string structName = results[0, 1];
        int structCost = Int32.Parse(results[0,5]);
        if (money >= structCost)
        {
            //Player can buy it
            SubtractMoney(structCost);
            //CODE TO INSTANTIATE STRUCTURE
        }
        else {
            //Insufficent funds
            Debug.Log("Not enough money for " + structName);
            //MAKE A PANEL WITH TEXT TELLING THE PLAYER THAT THEY DON'T HAVE ENOUGH MONEY
        }
        return false;
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
