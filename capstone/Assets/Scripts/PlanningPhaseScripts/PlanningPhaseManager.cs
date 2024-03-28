using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Manages reseting the Planning Phase UI for the transition between the Planning Phase 
and the Battle Phase.
This mostly acts as a wrapper for MapManager.
*/
public class PlanningPhaseManager : MonoBehaviour
{
    //Money
    [SerializeField] private MapManager mapManager;
    [SerializeField] private WorldSpaceCanvas worldSpaceCanvas;
    [SerializeField] private StructureShop structureShop;
    [SerializeField] private StructureInfo structureInfo;
    [SerializeField] private PlaceStructure placeStructure;
    [SerializeField] private DragStructures dragStructures;
    [SerializeField] private Message message;


    private void Awake()
    {
        /*
        mapManager = GetComponentInChildren<MapManager>();
        worldSpaceCanvas = GetComponentInChildren<WorldSpaceCanvas>();
        
        placeStructure = GetComponentInChildren<PlaceStructure>();
        dragStructures = GetComponentInChildren<DragStructures>();
        
        structureShop = placeStructure.transform.GetChild(0).GetChild(0).GetComponent<StructureShop>();
        structureInfo = structureShop.transform.GetChild(1).GetComponent<StructureInfo>();
        */
    }

    void Start()
    {
    }

    public void StartPlanningPhase() { 
        gameObject.SetActive(true);
        worldSpaceCanvas.gameObject.SetActive(false);
        structureInfo.gameObject.SetActive(false);
    }

    //connected to the Start Wave button
    public void EndPlanningPhase() {
        if (placeStructure.GetIsPlacingStructure())
        {
            Debug.Log("The player cannot end the planning phase when a structure is still being placed.");
            message.SetMessageText("You cannot start the wave until you finish placing down the current structure.");
        }
        else {
            gameObject.SetActive(false);
            worldSpaceCanvas.gameObject.SetActive(false);
            worldSpaceCanvas.SetCanvasParent(gameObject.transform);
            structureInfo.gameObject.SetActive(false);        
            dragStructures.ResetSelectedObject();
            dragStructures.SetSelectedObject(null);
        }
    }

    //Money Wrappers
    public void GetMoney() { 
        mapManager.GetMoney();
    }

    public void SetMoney(int amount) {
        mapManager.SetMoney(amount);
    }

    public void AddMoney(int amount) {
        mapManager.AddMoney(amount);
    }

    public void SubtractMoney(int amount) {
        mapManager.SubtractMoney(amount);
    }

    //Wave Wrappers
    public void GetCurrentWave() { 
        mapManager.GetCurrentWaveNumber();
    }

    public void GetTotalWave() { 
        mapManager.GetTotalWaveNumber();
    }

    public void IncreaseCurrentWaveNumber(int increase) {
        mapManager.IncreaseCurrentWaveNumber(increase);
    }

    public void DecreaseCurrentWaveNumber(int decrease) { 
        mapManager.DecreaseCurrentWaveNumber(decrease);
    }

    public void IncreaseTotalWaves(int increase) { 
        mapManager.IncreaseTotalWaves(increase);
    }

    public void DecreaseTotalWaves(int decrease) { 
        mapManager.DecreaseTotalWaves(decrease);
    }

    //Base Health Wrappers
    public void GetBaseHealth() { 
        mapManager.GetBaseHealth();
    }

    public void GetMaxBaseHealth() { 
        mapManager.GetMaxBaseHealth();
    }

    public void IncreaseBaseHealth(int increase) {
        mapManager.IncreaseBaseHealth(increase);
    }

    public void DecreaseBaseHealth(int decrease) {
        mapManager.DecreaseBaseHealth(decrease);
    }

    public void IncreaseMaxBaseHealth(int increase) { 
        mapManager.IncreaseMaxBaseHealth(increase);
    }

    public void DecreaseMaxBaseHealth(int decrease) {
        mapManager.DecreaseMaxBaseHealth(decrease);
    }


}
