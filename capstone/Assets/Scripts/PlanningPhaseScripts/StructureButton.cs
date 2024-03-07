using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StructureButton : MonoBehaviour
{
    private string buttonName;
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        /*
        GameObject shop = transform.parent.parent.parent.parent.parent.parent.gameObject;
        StructureShop structShop = shop.GetComponentInChildren<StructureShop>();
        TextMeshProUGUI buttonText = transform.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = "smth";
        */

    }

    //Sets up button text and appearance
    public void SetUpButton(string text)
    {
        SetButtonText(text);
        ConnectOnClickFunction();

        //other methods to change button appearance
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
