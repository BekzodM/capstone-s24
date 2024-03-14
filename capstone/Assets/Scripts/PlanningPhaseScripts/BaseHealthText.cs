using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseHealthText : MonoBehaviour
{
    GameObject mapManager;
    private TextMeshProUGUI baseHealthText;
    private int baseHealth;
    private int maxBaseHealth;

    // Start is called before the first frame update
    void Start()
    {
        baseHealthText = GetComponent<TextMeshProUGUI>();
        mapManager = transform.parent.parent.gameObject.GetComponent<StructureShop>().GetMapManager();
        if (mapManager == null)
        {
            Debug.Log("Connect the MapManager gameobject to the base health text in the inspector");
        }
        MapManager mapManagerComponent = mapManager.GetComponent<MapManager>();
        baseHealth = mapManagerComponent.GetStartingBaseHealth();
        maxBaseHealth = mapManagerComponent.GetMaxBaseHealth();
        ChangeBaseHealthText(baseHealth, maxBaseHealth);
    }

    public void ChangeBaseHealthText(int baseHP, int maxHP) {
        baseHealthText.text = "Base HP: " + baseHP.ToString() + '/' + maxHP.ToString();
    }
}
