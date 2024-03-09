using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseHealthText : MonoBehaviour
{
    private TextMeshProUGUI baseHealthText;
    private int baseHealth;
    private int maxBasehealth;

    // Start is called before the first frame update
    void Start()
    {
        baseHealthText = GetComponent<TextMeshProUGUI>();
        StructureShop structShop = transform.parent.parent.GetComponent<StructureShop>();
        baseHealth = structShop.GetStartingBaseHealth();
        maxBasehealth= structShop.GetMaxBaseHealth();
        ChangeBaseHealthText(baseHealth, maxBasehealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseBaseHealth(int increase) {
        if (baseHealth + increase > maxBasehealth)
        {
            Debug.Log("Base health cannot exceed max base health");
        }
        else {
            baseHealth += 1;
            ChangeBaseHealthText(baseHealth, baseHealth);
        }
    }

    public void DecreaseBaseHealth(int decrease) {
        if (baseHealth - decrease < 0) {
            //MAP LEVEL FAILED. CODE TO MAP LEVEL FAILED SCREEN.
            baseHealth = 0;
        }
        baseHealth -= decrease;
        ChangeBaseHealthText(baseHealth, baseHealth);
    }

    public void IncreaseMaxBaseHealth(int increase) {
        maxBasehealth += increase;
        ChangeBaseHealthText(maxBasehealth, maxBasehealth);
    }

    public void DecreaseMaxBaseHealth(int decrease) {
        if (maxBasehealth - decrease < baseHealth)
        {
            Debug.Log("Max Base Health cannot be lower than the current baseHealth");

        }
        else if (maxBasehealth - decrease <= 0)
        {
            Debug.Log("Max Base Health cannot be lower or equal to zero");
        }
        else {
            maxBasehealth -= decrease;
            ChangeBaseHealthText(maxBasehealth, maxBasehealth);
        }
    }

    private void ChangeBaseHealthText(int baseHP, int maxHP) {
        baseHealthText.text = "Base HP: " + baseHP.ToString() + '/' + maxHP.ToString();
    }
}
