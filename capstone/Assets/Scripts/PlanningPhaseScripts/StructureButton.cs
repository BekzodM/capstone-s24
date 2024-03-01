using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StructureButton : MonoBehaviour
{
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

    public void SetButtonText(string text)
    {
        TextMeshProUGUI buttonText = transform.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = text;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
