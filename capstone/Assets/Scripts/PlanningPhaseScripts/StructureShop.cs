using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StructureShop : MonoBehaviour
{
    [SerializeField] private GameObject[] structurePrefabs;
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
}
