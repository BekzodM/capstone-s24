using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StructureShop : MonoBehaviour
{
    [SerializeField] private GameObject[] offensiveStructurePrefabs;
    [SerializeField] private GameObject[] defensiveStructurePrefabs;
    [SerializeField] private GameObject[] supportStructurePrefabs;
    [SerializeField] private GameObject[] trapStructurePrefabs;
    [SerializeField] private GameObject structureShopPrefab;
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

        Transform shopContent = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0); //Content

        for (int i = 0; i < shopContent.childCount; i++)
        {
            Transform child = shopContent.GetChild(i);//Offensive..Structures container
            GameObject[] prefabs = structurePrefabs[i];

            foreach (GameObject obj in prefabs)
            {
                GameObject buttonInstance = Instantiate(structureShopPrefab);
                Button buttonComponent = buttonInstance.GetComponent<Button>();
                buttonComponent.onClick.AddListener(HandleButtonClick);
                buttonInstance.transform.SetParent(child.transform);
            }
        }
    }

    //Structure Type Buttons
    public void showStructureShop(int index)
    {
        Transform shopContent = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0);
        GameObject previousShopContent = shopContent.GetChild(previousIndex).gameObject;
        GameObject child = shopContent.GetChild(index).gameObject;

        previousShopContent.SetActive(false);
        child.SetActive(true);
        previousIndex = index;
    }

    //Structure Shop Button onClick function
    private void HandleButtonClick() {
        GameObject structureInfo = transform.GetChild(1).gameObject;

        structureInfo.SetActive(!structureInfo.activeSelf);

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
