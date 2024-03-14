using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceCanvas : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.LookAt(
            transform.position + Camera.main.transform.rotation * Vector3.forward,
            Camera.main.transform.rotation * Vector3.up
        );
    }

    public void SetCanvasParent(Transform obj) {
        transform.SetParent(obj);
        RectTransform canvasRectTransform = GetComponent<RectTransform>();

        // offset to move the canvas in local space
        Vector3 localOffset = new Vector3(0f, -6f, 0f); // Adjust the values as needed

        // Convert the local offset to global offset
        Vector3 globalOffset = transform.TransformDirection(localOffset);

        canvasRectTransform.position = transform.parent.position + globalOffset;
    }

    public void ShowCanvas(bool show) {
        gameObject.SetActive(show);
    }
}
