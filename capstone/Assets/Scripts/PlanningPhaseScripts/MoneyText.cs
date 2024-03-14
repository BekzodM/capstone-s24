using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyText : MonoBehaviour
{
    GameObject mapManager;
    private TextMeshProUGUI moneyText;
    private int money;

    // Start is called before the first frame update
    void Start()
    {
        moneyText = GetComponent<TextMeshProUGUI>();
        mapManager = transform.parent.parent.gameObject.GetComponent<StructureShop>().GetMapManager();
        if (mapManager == null)
        {
            Debug.Log("Connect the MapManager gameobject to the money text in the inspector");
        }
        MapManager mapManagerComponent = mapManager.GetComponent<MapManager>();
        money = mapManagerComponent.GetStartingMoney();
        ChangeMoneyText(money);
    }

    public void ChangeMoneyText(int money) {
        moneyText.text = "Money: " + money.ToString();
    }
}
