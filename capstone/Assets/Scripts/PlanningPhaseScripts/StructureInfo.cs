using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StructureInfo : MonoBehaviour
{
    private DatabaseWrapper databaseWrapper = new DatabaseWrapper();
    private TextMeshProUGUI structureName;
    private Image structureImage;
    private TextMeshProUGUI healthText;
    private TextMeshProUGUI costText;
    private TextMeshProUGUI structure_description;
    private string[,] results;




    void Start()
    {
        //databaseWrapper = new DatabaseWrapper();
        /*
        structureName = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        structureImage = transform.GetChild(1).GetComponent<Image>();
        healthText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        costText = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        structure_description = transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        */

    }

    public void MakeActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public void SetInfoBasedOnButtonText(string buttonName)
    {
        Debug.Log(buttonName);
        
        results = databaseWrapper.GetData("structures", "structure_name", buttonName);

        structureName = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        structureImage = transform.GetChild(1).GetComponent<Image>();
        healthText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        costText = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        structure_description = transform.GetChild(4).GetComponent<TextMeshProUGUI>();

        structureName.text = results[0, 1];
        //image
        healthText.text = "Health: " + results[0, 4];
        costText.text = "Cost: " + results[0, 5];

    }
}
