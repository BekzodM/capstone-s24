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

    //map info
    [SerializeField] private int startingMoney = 100;
    [SerializeField] private int currentWaveNumber = 1;
    [SerializeField] private int totalWaveNumber = 5;
    [SerializeField] private int startingBaseHealth = 200;
    [SerializeField] private int maxBaseHealth = 200;

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

        for (int i = 0; i < shopContent.childCount; i++) //iterate children in Content
        {
            Transform child = shopContent.GetChild(i);//Offensive,...Structures container
            GameObject[] prefabs = structurePrefabs[i];

            for (int j = 0; j < prefabs.Length; j++) 
            //foreach (GameObject obj in prefabs) //iterate prefabs of Offensive, Defensive..etc.
            {
                GameObject obj= prefabs[j];

                GameObject buttonInstance = Instantiate(structureShopButtonPrefab);
                Button buttonComponent = buttonInstance.GetComponent<Button>();
                buttonInstance.transform.SetParent(child.transform);

                buttonInstance.GetComponent<StructureButton>().SetUpButton(obj.name,j);
            }
        }
    }

    //Map Info Functions
    public int GetStartingMoney() {
        return startingMoney;
    }

    public int GetCurrentWaveNumber() {
        return currentWaveNumber;
    }

    public int GetTotalWaveNumber() {
        return totalWaveNumber;
    }

    public int GetStartingBaseHealth() {
        return startingBaseHealth;
    }

    public int GetMaxBaseHealth() {
        return maxBaseHealth;
    }

}
