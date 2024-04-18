using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlanningCamera : MonoBehaviour
{
    public float moveSpeed = 100f;
    public float zoomSpeed = 100f;
    public float minZoomSize = 5f;
    public float maxZoomSize = 200f;

    public InputAction cameraControls;
    Vector2 move = Vector2.zero;

    public Image w;
    public Image a;
    public Image s;
    public Image d;

    Camera planningCamera;
    void Start()
    {
        planningCamera = gameObject.GetComponent<Camera>();
    }

    void Update()
    {
        // Camera movement with A and D keys (left and right)

        move = cameraControls.ReadValue<Vector2>();
        float directionX = move.x;
        float directionY = move.y;

        Vector3 moveDirection = transform.up * directionY + transform.right *directionX;

        //Debug.Log(moveDirection);
        //changing wasd buttons colors
        if (moveDirection.z > 0) {
            w.color = Color.red;
        }
        else
        {
            w.color = Color.white;
        }
        
        if (moveDirection.z < 0) {
            s.color = Color.red;
        }
        else
        {
            s.color = Color.white;
        }
        
        if (moveDirection.x > 0) {
            d.color = Color.red;
        }
        else
        {
            d.color = Color.white;
        }

        if (moveDirection.x < 0) {
            a.color = Color.red;
        }
        else
        {
            a.color = Color.white;
        }


        // Normalize the movement direction to ensure consistent speed diagonally
        moveDirection = moveDirection.normalized;

        // Translate the camera's position based on the movement direction and speed
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        // Camera zoom with mouse scroll wheel
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
        float newZoomSize = planningCamera.orthographicSize - scrollWheelInput * zoomSpeed;

        // Clamp the zoom size to min and max values
        newZoomSize = Mathf.Clamp(newZoomSize, minZoomSize, maxZoomSize);

        // Apply the new zoom size
        planningCamera.orthographicSize = newZoomSize;
    }

    private void OnEnable()
    {
        cameraControls.Enable();
    }

    private void OnDisable()
    {
        cameraControls.Disable();
    }
}
