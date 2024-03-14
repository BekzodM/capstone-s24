using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StructureButton : MonoBehaviour
{
    private string buttonName;
    private Button button;
    private int tabIndex;
    private int buttonIndex;

    // Start is called before the first frame update
    void Start()
    {
    }

    //Sets up button text and appearance
    public void SetUpButton(string text, int tabIdx, int buttonIdx)
    {
        SetButtonText(text);
        ConnectOnClickFunction();
        SetUpIndex(tabIdx, buttonIdx);

        //other methods to change button appearance
    }

    public void SetUpIndex(int tabIdx, int buttonIdx) {
        tabIndex = tabIdx;
        buttonIndex = buttonIdx;
    }

    public void OnClickCancelBuy() {
        ShowInfoPanel(false);
        ShowConfirmBuyPanel(false);
    }

    public void OnClickBuy()
    {
        TryToBuy();
        ShowInfoPanel(false);
        ShowConfirmBuyPanel(false);
    }

    private void SetButtonText(string text)
    {
        TextMeshProUGUI buttonText = transform.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = text;
        buttonName = text;
    }

    private void ConnectOnClickFunction()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(HandleButtonClick);
    }

    private void HandleButtonClick()
    {
        GameObject infoPanel = transform.parent.parent.parent.parent.parent.parent.GetChild(1).gameObject;
        StructureInfo info = infoPanel.GetComponent<StructureInfo>();
        info.MakeActive(true);
        info.SetInfoBasedOnButtonText(buttonName);
        ShowConfirmBuyPanel(true);
        
    }

    private void ShowInfoPanel(bool show) {
        GameObject infoPanel = transform.parent.parent.parent.parent.parent.parent.GetChild(1).gameObject;
        StructureInfo info = infoPanel.GetComponent<StructureInfo>();
        info.MakeActive(show);
        /*
        GameObject upgradeButtonContent = info.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        Button [] upgradeButtons = upgradeButtonContent.GetComponentsInChildren<Button>();
        foreach (Button button in upgradeButtons) {
            button.interactable = false;
        }
        */
    }

    private void ShowConfirmBuyPanel(bool show) {
        transform.GetChild(1).gameObject.SetActive(show);
    }

    private void TryToBuy() {
        GameObject mapManager = transform.parent.parent.parent.parent.parent.parent.GetComponent<StructureShop>().GetMapManager();
        MapManager mapManagerComponent = mapManager.GetComponent<MapManager>();
        mapManagerComponent.Purchase(buttonName, tabIndex, buttonIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
