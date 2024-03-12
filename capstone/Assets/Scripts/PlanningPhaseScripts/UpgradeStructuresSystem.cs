using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStructuresSystem : MonoBehaviour
{
    //[SerializeField] GameObject upgradeButton0;
    //[SerializeField] GameObject upgradeButton1;
    //[SerializeField] GameObject upgradeButton2;
    [SerializeField] GameObject upgradeButtonContent;
    private GameObject planningPhaseUI;
    private PlaceStructure placeStructure;
    private DragStructures dragStructures;

    // Start is called before the first frame update
    void Start()
    { planningPhaseUI = transform.parent.parent.parent.parent.gameObject;
        placeStructure = planningPhaseUI.GetComponent<PlaceStructure>();
        dragStructures = planningPhaseUI.GetComponent<DragStructures>();
    }

    // Update is called once per frame
    void Update()
    {

    }

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
        //CODE TO HANDLE UPGRADING HERE
        OnClickCancelUpgrade(idx);
    }

    public void OnClickCancelUpgrade(int idx) {
        GameObject upgradeButton = upgradeButtonContent.transform.GetChild(idx + 1).gameObject;
        GameObject confirmPanel = upgradeButton.transform.GetChild(1).gameObject;
        confirmPanel.SetActive(false);

    }
}
