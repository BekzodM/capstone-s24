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
    public GameObject message;

    HashSet<GameObject> structures; //structures that have been placed down already

    private void Awake()
    {
        structures = new HashSet<GameObject>();
    }

    void Start()
    {
        //planningCamera = Camera.main;
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
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("StructureGround"))
                {
                    Debug.Log("Place structure");
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
                else {
                    message.GetComponent<Message>().SetMessageText("Cannot place structure. Move the center of the camera on the avaliable space.");
                    Debug.Log("Move the camera's center with WASD on avaliable space. Cannot place structure.");
                }
            }
            else
            {
                message.GetComponent<Message>().SetMessageText("Cannot place structure. Move the center of the camera on the avaliable space.");
                Debug.Log("Move the camera's center with WASD on avaliable space. Cannot place structure.");
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
        if (obj != null)
        {
            structures.Remove(obj);
            Destroy(obj);
        }
        else {
            Debug.LogError("Cannnot remove structure placement");
        }
    }

    public void ActivateStructureAreaZoneColliders(bool activate) {
        foreach (GameObject structure in structures) {
            GameObject areaZone = structure.transform.GetChild(0).gameObject;
            if (areaZone != null)
            {
                if (areaZone.GetComponent<AreaZone>())
                {
                    Collider collider = areaZone.GetComponent<Collider>();
                    if (collider != null)
                    {
                        collider.enabled = activate;
                    }
                    else {
                        Debug.LogError("Structure collider is missing");
                    }
                }
                else {
                    Debug.LogError("Area zone script is missing");
                }
            }
            else {
                Debug.LogError("areaZone GameObject is null");
            }
        }
    }

    public void ActivateStructureAreaZoneMesh(bool activate) {
        foreach (GameObject structure in structures) {
            GameObject areaZone = structure.transform.GetChild(0).gameObject;
            if (areaZone != null)
            {
                MeshRenderer render = areaZone.GetComponent<MeshRenderer>();
                if (render != null) {
                    render.enabled = activate;
                }
                else {
                    Debug.LogError("Mesh Render is missing");
                }
            }
            else {
                Debug.LogError("Area zone GameObject is null");
            }
        }
    }
}
