using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlaceStructure : MonoBehaviour
{
    bool isPlacingStructure = false;
    private Camera mainCamera;
    public GameObject worldSpaceCanvas;

    //List<GameObject> structures;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        //structures = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateStructure(int tabIndex, int buttonIndex) {
        GameObject prefab = transform.GetChild(0).GetChild(0).GetComponent<StructureShop>().GetPrefabFromStructurePrefabs(tabIndex, buttonIndex);
        if (prefab != null)
        {
            Vector3 cameraPosition = mainCamera.transform.position;
            Vector3 cameraForward = mainCamera.transform.forward;

            Debug.DrawRay(cameraPosition, cameraForward * 100f, Color.blue, 0.1f);

            // Raycast from camera to ground
            Ray ray = new Ray(cameraPosition, cameraForward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // Get the hit point and instantiate the prefab
                Vector3 spawnPosition = hit.point;
                
                GameObject prefabInstance = Instantiate(prefab, spawnPosition, Quaternion.identity);
                //structures.Add(prefabInstance);

                WorldSpaceCanvas canvas = worldSpaceCanvas.GetComponent<WorldSpaceCanvas>();
                canvas.SetCanvasParent(prefabInstance.transform);
                canvas.ShowCanvas(true);

                isPlacingStructure = true;
                
            }
        }
    }

    public bool GetIsPlacingStructure() {
        return isPlacingStructure;
    }
}
