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

    public void ChangeMoneyText(int currWave, int totalWaves) {
        waveText.text = "Round: " + currWave.ToString() + '/' + totalWaves.ToString();
    }
}
