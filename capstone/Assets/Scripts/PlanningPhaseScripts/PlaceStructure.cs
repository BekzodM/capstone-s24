using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlaceStructure : MonoBehaviour
{
    private bool isPlacingStructure = false;
    public Camera planningCamera;
    public GameObject worldSpaceCanvas;
    public GameObject mapManager;

    HashSet<GameObject> structures; //structures that have been placed down already

    // Start is called before the first frame update
    void Start()
    {
        //planningCamera = Camera.main;
        structures = new HashSet<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateStructure(int tabIndex, int buttonIndex) {
        //hide the previously selected structure's area zone and make its collider true
        GameObject selectedObject = GetComponent<DragStructures>().GetSelectedObject();
        if (selectedObject != null) { 
            Structure selectedStructure = selectedObject.GetComponent<Structure>();
            selectedStructure.ShowAreaZone(false);
            //selectedStructure.ActivateAreaZoneCollider(true);        
        }


        //Instantiate
        GameObject prefab = transform.GetChild(0).GetChild(0).GetComponent<StructureShop>().GetPrefabFromStructurePrefabs(tabIndex, buttonIndex);
        if (prefab != null)
        {
            Vector3 cameraPosition = planningCamera.transform.position;
            Vector3 cameraForward = planningCamera.transform.forward;

            Debug.DrawRay(cameraPosition, cameraForward * 100f, Color.blue, 0.1f);

            // Raycast from camera to ground
            Ray ray = new Ray(cameraPosition, cameraForward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // Get the hit point and instantiate the prefab
                Vector3 spawnPosition = hit.point;
                
                GameObject prefabInstance = Instantiate(prefab, spawnPosition, Quaternion.identity);

                prefabInstance.GetComponent<Structure>().ActivateAreaZoneCollider(false);

                //structures.Add(prefabInstance);

                WorldSpaceCanvas canvas = worldSpaceCanvas.GetComponent<WorldSpaceCanvas>();
                canvas.SetCanvasParent(prefabInstance.transform);
                canvas.ShowCanvas(true);
                canvas.ShowPlacementConfirmationPanel(true);
                canvas.ShowRotationPanel(true);
                canvas.ShowSellPanel(false);

                isPlacingStructure = true;

                gameObject.GetComponent<DragStructures>().SetSelectedObject(prefabInstance);
                
            }
        }
    }

    public bool GetIsPlacingStructure() {
        return isPlacingStructure;
    }

    public void OnClickConfirmPlacement() {
        WorldSpaceCanvas canvas = worldSpaceCanvas.GetComponent<WorldSpaceCanvas>();
        canvas.ShowCanvas(false);
        isPlacingStructure = false;

        //add to set
        structures.Add(canvas.transform.parent.gameObject);

        //purchase
        string name = transform.GetComponent<DragStructures>().GetSelectedObject().GetComponent<Structure>().GetStructureName();
        mapManager.GetComponent<MapManager>().Purchase(name);

        Tooltip.HideTooltip();
    }

    public void OnClickCancelPlacement() {
        WorldSpaceCanvas canvas = worldSpaceCanvas.GetComponent<WorldSpaceCanvas>();
        canvas.ShowCanvas(false);
        isPlacingStructure = false;

        //give full refund
        
        GameObject obj = worldSpaceCanvas.transform.parent.gameObject;
        //mapManager.GetComponent<MapManager>().FullRefund(obj.GetComponent<Structure>().GetStructureName());
        

        //destroy instance and reparent the canvas
        canvas.transform.SetParent(null);
        gameObject.GetComponent<DragStructures>().DestroySelectedObject();

        Tooltip.HideTooltip();
    }

    public bool CheckStructurePlacement(GameObject obj) {
        if (structures.Contains(obj) && obj != null) {
            return true;
        }
        return false;
    }

    public void RemoveStructurePlacement(GameObject obj) {
        structures.Remove(obj);
    }
}
