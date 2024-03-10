using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    //Money
    [SerializeField] private int startingMoney = 100;

    //Waves
    [SerializeField] private int currentWaveNumber = 1;
    [SerializeField] private int totalWaveNumber = 5;

    //Base HP
    [SerializeField] private int startingBaseHealth = 200;
    [SerializeField] private int maxBaseHealth = 200;

    // Start is called before the first frame update
    void Start()
    {
        
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
    public bool CanPurchase(string structureName) {
        //need database look up
        return false;
    }

    //Wave Functions
    public int GetCurrentWaveNumber()
    {
        return currentWaveNumber;
    }

    public int GetTotalWaveNumber()
    {
        return totalWaveNumber;
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

}
