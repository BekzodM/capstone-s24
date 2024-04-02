using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlanningCamera : MonoBehaviour
{
    public float moveSpeed = 100f;
    public float zoomSpeed = 100f;
    public float minZoomSize = 5f;
    public float maxZoomSize = 200f;

    public InputAction cameraControls;
    Vector2 move = Vector2.zero;


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
