using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WorldSpaceCanvas : MonoBehaviour
{
    public Camera planningCamera;
    public GameObject root;

    private void LateUpdate()
    {
        transform.LookAt(
            transform.position + planningCamera.transform.rotation * Vector3.forward,
            planningCamera.transform.rotation * Vector3.up
        );
    }

    public void SetCanvasParent(Transform obj) {
        transform.SetParent(obj);
        RectTransform canvasRectTransform = GetComponent<RectTransform>();

        // offset to move the canvas in local space
        Vector3 localOffset = new Vector3(0f, -6f, -15f); // Adjust the values as needed

        // Convert the local offset to global offset
        Vector3 globalOffset = transform.TransformDirection(localOffset);

        canvasRectTransform.position = transform.parent.position + globalOffset;
    }

    public void ShowCanvas(bool show) {
        gameObject.SetActive(show);
    }

    public void ShowPlacementConfirmationPanel(bool show) {
        transform.GetChild(0).gameObject.SetActive(show);
    }

    public void ShowSellPanel(bool show) {
        GameObject sellPanel = transform.GetChild(1).gameObject;
        GameObject sellButton = sellPanel.transform.GetChild(0).gameObject;
        GameObject sellConfirmationPanel = sellPanel.transform.GetChild(1).gameObject;

        sellPanel.SetActive(show);
        sellButton.SetActive(true);
        sellConfirmationPanel.SetActive(false);
        
    }

    public void ShowSellConfirmationPanel(bool show) {
        GameObject sellPanel = transform.GetChild(1).gameObject;
        GameObject sellButton = sellPanel.transform.GetChild(0).gameObject;
        GameObject sellConfirmationPanel = sellPanel.transform.GetChild(1).gameObject;

        sellPanel.SetActive(show);
        sellButton.SetActive(false);
        sellConfirmationPanel.SetActive(true);
    }

    public void ResetWorldCanvas() {
        transform.SetParent(root.transform);
        ShowCanvas(false);
        ShowPlacementConfirmationPanel(true);
        ShowSellPanel(false);
    }
}
