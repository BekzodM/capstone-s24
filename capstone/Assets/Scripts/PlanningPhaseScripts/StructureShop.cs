using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StructureShop : MonoBehaviour
{
    [SerializeField] private GameObject[] offensiveStructurePrefabs;
    [SerializeField] private GameObject[] defensiveStructurePrefabs;
    [SerializeField] private GameObject[] supportStructurePrefabs;
    [SerializeField] private GameObject[] trapStructurePrefabs;
    [SerializeField] private GameObject structureShopButtonPrefab;
    private GameObject[][] structurePrefabs;
    private int previousIndex = 0;

    private void Start()
    {
        structurePrefabs = new GameObject[][]
            {
                offensiveStructurePrefabs,
                defensiveStructurePrefabs,
                supportStructurePrefabs,
                trapStructurePrefabs
            };

        //Fill shop with buttons

        FillShopWithButtons();
    }

    public GameObject[][] GetStructurePrefabs()
    {
        return structurePrefabs;
    }

    //Structure Type Buttons. Switch between the different kinds of structures in the shop
    public void showStructureShop(int index)
    {
        Transform shopContent = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0);
        GameObject previousShopContent = shopContent.GetChild(previousIndex).gameObject;
        GameObject child = shopContent.GetChild(index).gameObject;

        previousShopContent.SetActive(false);
        child.SetActive(true);
        previousIndex = index;
    }

    //Fill shop with buttons on the bottom part of the screen
    private void FillShopWithButtons() 
    {
        Transform shopContent = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0); //Content

        for (int i = 0; i < shopContent.childCount; i++)
        {
            Transform child = shopContent.GetChild(i);//Offensive..Structures container
            GameObject[] prefabs = structurePrefabs[i];

            foreach (GameObject obj in prefabs)
            {
                GameObject buttonInstance = Instantiate(structureShopButtonPrefab);
                Button buttonComponent = buttonInstance.GetComponent<Button>();
                buttonComponent.onClick.AddListener(HandleButtonClick);
                buttonInstance.transform.SetParent(child.transform);

                buttonInstance.GetComponent<StructureButton>().SetButtonText(obj.name);
            }
        }
    }

    //Structure Shop Button onClick function
    private void HandleButtonClick() {
        GameObject structureInfo = transform.GetChild(1).gameObject;

        //structureInfo.SetActive(!structureInfo.activeSelf);
        structureInfo.SetActive(true);
        Transform structureShopPanel = transform.GetChild(0);
        RectTransform rectTransform = structureShopPanel.GetComponent<RectTransform>();

    }


    /*
    public void InstantiatePrefabOnClick(int prefabIndex)
    {
        if (prefabIndex >= 0 && prefabIndex < structurePrefabs.Length)
        {
            Instantiate(structurePrefabs[prefabIndex], Vector3.zero, Quaternion.identity);
        }
        else {
            Debug.LogWarning("Invalid prefab index: " + prefabIndex);
        }
    }
    */
  

}
