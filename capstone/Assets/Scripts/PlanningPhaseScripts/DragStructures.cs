using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
Requirements:
 - Structures are tagged "Structure" and in the layer "Draggable".
 - Make sure the floor where structures are placed have the layer "Ground".
 - NOTE: Possible errors may arise from reorganizing the layers.
If layers are reorganized, make sure the ground layer is set to "Ground"
and draggable layer is set to "Draggable" in the inspector of the PlanningPhaseUI gameObject.

 */
public class DragStructures : MonoBehaviour
{
    public LayerMask structureGroundLayer;
    public LayerMask draggableLayer;

    public GameObject worldSpaceCanvas;

    public Camera planningCamera;
    private GameObject selectedObject;
    private RaycastHit hitInfo;
    private bool isDragging = false;

    // Start is called before the first frame update
    void Start()
    {
        //mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //Check Mouse Input is hold down the mouse button
        if (Input.GetMouseButtonDown(0)) {
            //Raycast from mouse to ground

            //camera to screen point ray
            Ray ray = planningCamera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.blue, 0.1f);
            
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, draggableLayer)) {
                if (hitInfo.collider.transform.parent.tag == "Structure") {


                    //if there is a structure that hasn't been placed down yet, you cannot selected another placed down structure
                    
                    //HideSelectedStructureAreaZoneMesh();
                    
                    //get selected structure gameobject
                    selectedObject = hitInfo.collider.transform.parent.gameObject;

                    PlaceStructure placeStructure = GetComponent<PlaceStructure>();
                    Structure structureComponent = selectedObject.GetComponent<Structure>();
                    
                    //check if structure has as been placed down already
                    if (placeStructure.CheckStructurePlacement(selectedObject))
                    {
                        Debug.Log("Selected structure has been placed down already");
                        
                        StructureInfo structInfo = transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<StructureInfo>();
                        structInfo.MakeActive(true);

                        structureComponent.ShowAreaZone(true);
                        //structureComponent.ActivateAreaZoneCollider(false);

                        if (gameObject.GetComponent<PlaceStructure>().GetIsPlacingStructure() == true)
                        {
                            Debug.Log("You are currently placing a structure. Do not reparent world space canvas.");
                        }
                        else { 
                            WorldSpaceCanvas worldCanvas = worldSpaceCanvas.GetComponent<WorldSpaceCanvas>();
                            worldCanvas.SetCanvasParent(selectedObject.transform);
                            worldCanvas.ShowCanvas(true);
                            worldCanvas.ShowSellPanel(true);
                            worldCanvas.ShowRotationPanel(false);
                            
                            StructureInfo structureInfo = FindObjectOfType<StructureInfo>();
                            structureInfo.SetInfoBasedOnSelectedObject(selectedObject);
                            structureInfo.SetUpgradeInfoBasedOnSelectedObject(selectedObject);
                        }
                    }
                    else {
                        isDragging = true;
                        Debug.Log("object has not been placed down already");                    
                    }
                }
            }

            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Mouse is hovering over UI");
            }
            else {
                //Clicking elsewhere that is not on the Draggable layer deselects the object
                if (Physics.Raycast(ray, out RaycastHit hit)) {
                    if (hit.collider.gameObject != null && hit.collider.gameObject.layer != LayerMask.NameToLayer("Draggable") && hit.collider.gameObject.layer != LayerMask.NameToLayer("UI")) {
                        //make sure the structure isn't placed down yet. Cannot deselect if the player is still placing down a structure
                        if (!gameObject.GetComponent<PlaceStructure>().GetIsPlacingStructure())
                        {
                            worldSpaceCanvas.GetComponent<WorldSpaceCanvas>().ResetWorldCanvas();

                            HideSelectedStructureAreaZoneMesh();

                            selectedObject = null;
                            StructureInfo infoPanel = transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<StructureInfo>();
                            infoPanel.MakeActive(false);
                            Debug.Log("Deselected Object");
                        }
                        else {
                            Debug.Log("Player is placing structure and selected object cannot be deselected.");
                        }
                    }
                }
            }
        }

        //Dragging a valid selected object
        if (selectedObject != null) {
            Ray ray = planningCamera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 0.1f);

            //moving the selected object to the postions along the ground
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, structureGroundLayer) && isDragging) {
                selectedObject.transform.position = hitInfo.point;            
            }

            //stop dragging but object is still selected
            if (Input.GetMouseButtonUp(0)) {
                isDragging = false;
            }

        }
        
    }

    public GameObject GetSelectedObject() {
        return selectedObject;
    }

    public void SetSelectedObject(GameObject obj) {
        selectedObject = obj;
    }

    public void DestroySelectedObject() {
        Destroy(selectedObject);
        selectedObject= null;
    }

    public void HideSelectedStructureAreaZoneMesh() {
        if (selectedObject != null) {
            Structure selectedStructure = selectedObject.GetComponent<Structure>();
            selectedStructure.ShowAreaZone(false);
        }
    }
}
