using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    //[SerializeField] private int priorityBoost = 10;

    //[SerializeField] private PlayerInput playerInput;
    //private CinemachineVirtualCamera aimVirtualCamera;
    //private InputAction aim;

    //private void Awake()
    //{
    //    aimVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    //    aim = playerInput.actions["Aim"];
    //}

    //private void OnEnable()
    //{
    //    aim.performed += _ => StartAim();
    //    aim.canceled += _ => CancelAim();
    //}

    //private void OnDisable()
    //{
    //    aim.performed -= _ => StartAim();
    //    aim.canceled -= _ => CancelAim();
    //}

    //private void StartAim()
    //{
    //    aimVirtualCamera.Priority += priorityBoost;
    //    Debug.Log("add");
    //}

    //private void CancelAim()
    //{
    //    aimVirtualCamera.Priority -= priorityBoost;
    //    Debug.Log("sub");
    //}

    //private void Update()
    //{
    //    //Debug.Log("combat");
    //}



    //[SerializeField] private int priorityBoost = 10;

    ////private PlayerInputActions playerInputActions;
    //[SerializeField] private PlayerInput playerInput;
    //private InputAction aim;

    //private CinemachineVirtualCamera aimVirtualCamera;

    //// Most use Start() to initialize values because playerInputActions is initialized (and created)
    //// in the PlayerController.cs script in the Awake() call
    //private void Awake()
    //{
    //    aimVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    //    aim = playerInput.actions["Aim"];
    //    //playerInputActions = GetComponent<PlayerController>().playerInputActions;
    //    //aim = playerInputActions.Player.Aim;  // Get ref to "Movement" action from PlayerInputActions
    //    //aim.performed += _ => StartAim();
    //    //aim.canceled += _ => CancelAim();
    //}

    //private void OnEnable()
    //{
    //    aim.performed += _ => StartAim();
    //    aim.canceled += _ => CancelAim();
    //    //aim = playerInputActions.Player.Aim;
    //    //playerInputActions.Player.Aim.Enable();
    //    //playerInputActions.Player.Aim.performed += Aim; // Subscribe to Jump() method
    //    //movement = playerInputActions.Player.Movement;  // Get ref to "Movement" action from PlayerInputActions
    //    //playerInputActions.Player.Enable();
    //}

    //private void OnDisable()
    //{
    //    aim.performed -= _ => StartAim();
    //    aim.canceled -= _ => CancelAim();
    //    //playerInputActions.Player.Aim.Disable();
    //    //playerInputActions.Player.Jump.performed -= Jump;
    //    //playerInputActions.Player.Disable();
    //}

    //private void StartAim()
    //{
    //    aimVirtualCamera.Priority += priorityBoost;
    //}

    //private void CancelAim()
    //{
    //    aimVirtualCamera.Priority -= priorityBoost;
    //}

}
