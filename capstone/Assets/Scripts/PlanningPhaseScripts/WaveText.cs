using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class WaveText : MonoBehaviour
{
    private GameObject mapManager;
    private TextMeshProUGUI waveText;
    private int currentWave;
    private int totalWaves;

    // Start is called before the first frame update
    void Start()
    {
        waveText= GetComponent<TextMeshProUGUI>();
        mapManager = transform.parent.parent.gameObject.GetComponent<StructureShop>().GetMapManager();
        if (mapManager == null) {
            Debug.Log("Connect the MapManager gameobject to the wave text in the inspector");
        }
        MapManager mapManagerComponent = mapManager.GetComponent<MapManager>();
        
        
        currentWave = mapManagerComponent.GetCurrentWaveNumber();
        totalWaves = mapManagerComponent.GetTotalWaveNumber();
        ChangeMoneyText(currentWave, totalWaves);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseCurrentWaveNumber(int increase) {

        if (currentWave + increase > totalWaves)
        {
            Debug.Log("Current wave number cannot exceed total waves");
        }
        else {
            currentWave += increase;
            ChangeMoneyText(currentWave, totalWaves);
        }
    }

    public void DecreaseCurrentWaveNumber(int decrease) {
        if (currentWave - decrease < 1)
        {
            Debug.Log("Current wave number cannot be lower than one");
        }
        else {
            currentWave -= decrease;
            ChangeMoneyText(currentWave, totalWaves);

        }
    }

    public void IncreaseTotalWaves(int increase) {
        totalWaves+= increase;
    }

    public void DecreaseTotalWaves(int decrease) {
        if (totalWaves - decrease < currentWave)
        {
            Debug.Log("Total amount of waves cannot be lower than the current number of waves");
        }
        else if (totalWaves - decrease < 1)
        {
            Debug.Log("Total amount of waves cannot be lower than 1");
        }
        else {
            totalWaves -= decrease;
            ChangeMoneyText(currentWave, totalWaves);
        }
    }

    private void ChangeMoneyText(int currWave, int totalWaves) {
        waveText.text = "Wave: " + currentWave.ToString() + '/' + totalWaves.ToString();
    }
}
