using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public LayerMask groundLayer;
    public LayerMask draggableLayer;

    private Camera mainCamera;
    private GameObject selectedObject;
    private RaycastHit hitInfo;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //Check Mouse Input is hold down the mouse button
        if (Input.GetMouseButtonDown(0)) {
            //Raycast from mouse to ground

            //camera to screen point ray
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.blue, 0.1f);
            
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, draggableLayer)) {
                if (hitInfo.collider.transform.parent.tag == "Structure") {
                    //get selected structure gameobject
                    selectedObject = hitInfo.collider.transform.parent.gameObject;
                }
            }
        }

        //Dragging a valid selected object
        if (selectedObject != null) {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 0.1f);

            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, groundLayer)) {
                selectedObject.transform.position = hitInfo.point;            
            }

            if (Input.GetMouseButtonUp(0)) {
                selectedObject = null;
            }

        }
        
    }
}
