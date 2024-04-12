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
    private TextMeshProUGUI structureDescription;
    private string[,] results;
    private string[,] upgradeResults;

    void Start()
    {
        /*
        structureName = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        structureImage = transform.GetChild(1).GetComponent<Image>();
        healthText = transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        costText = transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        structureDescription = transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>();
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

        structureName = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        structureImage = transform.GetChild(1).GetComponent<Image>();
        healthText = transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        costText = transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        structureDescription = transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>();



        structureName.text = results[0, 1];
        //image
        string path = results[0, 4];
        Texture2D texture = Resources.Load<Texture2D>(path);
        Sprite loadedSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        if (texture != null)
        {
            structureImage.sprite = loadedSprite;
        }
        else
        {
            Debug.LogError("Texture2D not found at path: " + path);
        }

        healthText.text = "Health: " + results[0, 6];
        costText.text = "Cost: " + results[0, 7];
        structureDescription.text = results[0, 3];

        //upgrades
        string structureId = results[0,0];
        upgradeResults = databaseWrapper.GetData("structureUpgrades","structure_id", structureId);

        //changing upgrade text

        SetUpgradeButtonText(0, upgradeResults[0, 1]);
        SetUpgradeButtonText(1, upgradeResults[5, 1]);
        SetUpgradeButtonText(2, upgradeResults[10,1]);

        //changing upgrade image

        SetUpgradeButtonImage(0, upgradeResults[0,3]);
        SetUpgradeButtonImage(1, upgradeResults[5,3]);
        SetUpgradeButtonImage(2, upgradeResults[10,3]);

    }

    public void SetInfoBasedOnSelectedObject(GameObject obj) {
        Structure structure= obj.GetComponent<Structure>();
        structureName.text = structure.GetStructureName();
        healthText.text = "Health: " + structure.GetMaxHealth();
        costText.text = "Cost: " + structure.GetCost();
        structureDescription.text = structure.GetDescription();
    }

    
    public void SetUpgradeInfoBasedOnSelectedObject(GameObject obj) {
        StructureUpgradesInfo upgradesInfo = obj.GetComponent<StructureUpgradesInfo>();
        Transform upgradeScrollView = transform.GetChild(5);
        Transform content = upgradeScrollView.GetChild(0).GetChild(0);

        for (int i = 1; i < 4; i++ ) {
            Transform upgradeButton = content.GetChild(i);
            //Transform upgradePanel = upgradeButton.GetChild(0);

            int slotIndex = i - 1;

            SetUpgradeButtonImage(slotIndex,upgradesInfo.GetSlotImagePath(slotIndex));

            SetUpgradeButtonText(slotIndex,upgradesInfo.GetSlotUpgradeName(slotIndex));

            SetSliderValue(slotIndex,upgradesInfo.GetCurrentUpgradeLevel(slotIndex));

            //check blocked and unblocked slots and handle visibility
            UpgradeStructuresSystem upgradeSystem= upgradeScrollView.GetComponent<UpgradeStructuresSystem>();
            if (upgradesInfo.GetBlockedSlots().Contains(slotIndex)) {
                //blocked slots
                upgradeSystem.DisableSlot(upgradeButton.GetComponent<CanvasGroup>());
            }
            else 
            {
                //unblocked slots
                if (upgradesInfo.GetCurrentUpgradeLevel(slotIndex) == 5)
                {
                    //button is not interactable
                    upgradeButton.GetComponent<Button>().interactable = false;
                }
                else { 
                    //upgrade button is interactable
                    upgradeButton.GetComponent<Button>().interactable = true;
                    upgradeSystem.ActivateSlot(upgradeButton.GetComponent<CanvasGroup>());
                }
            }
        }
    }
    
    public void SetUpgradeButtonImage(int slotIndex, string imagePath) {
        Transform upgradeScrollView = transform.GetChild(5);
        Transform content = upgradeScrollView.GetChild(0).GetChild(0);
        Transform upgradeButton = content.GetChild(slotIndex + 1);
        Transform upgradePanel = upgradeButton.GetChild(0);

        //SetImage
        GameObject upgradeImage = upgradePanel.GetChild(0).gameObject;
        Texture2D texture = Resources.Load<Texture2D>(imagePath);
        Sprite loadedSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        if (texture != null)
        {
            upgradeImage.GetComponent<Image>().sprite = loadedSprite;
        }
        else
        {
            Debug.LogError("Texture2D not found at path: " + imagePath);
        }
    }

    public void SetUpgradeButtonText(int slotIndex, string text) {
        Transform upgradeScrollView = transform.GetChild(5);
        Transform content = upgradeScrollView.GetChild(0).GetChild(0);
        Transform upgradeButton = content.GetChild(slotIndex + 1);
        Transform upgradePanel = upgradeButton.GetChild(0);

        //Set text
        GameObject upgradeText = upgradePanel.GetChild(1).gameObject;
        upgradeText.GetComponent<TextMeshProUGUI>().text = text;
    }

    public void SetSliderValue(int slotIndex, int currentLevel)
    {
        Transform upgradeScrollView = transform.GetChild(5);
        Transform content = upgradeScrollView.GetChild(0).GetChild(0);
        Transform upgradeButton = content.GetChild(slotIndex + 1);
        Transform upgradePanel = upgradeButton.GetChild(0);
        Transform slider = upgradePanel.GetChild(2);
        //Set slider value

        float sliderValue = (1f/5f) * currentLevel;

        slider.GetComponent<UpgradeLevelProgressBar>().SetProgressBar(sliderValue);

    }
}
