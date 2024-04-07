using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Actor
{
    // Player class should own game data associated with player

    //[SerializeField] protected float jumpForce = 5f;

    Rigidbody playerRigidbody;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        // Press esc to exit Locked State
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            // ToggleCursorLock();
        }
    }

    public void ToggleCursorLock()
    {
        // Toggle cursor lock state between locked and confined to the window
        Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;

        // Toggle cursor visibility
        Cursor.visible = !Cursor.visible;
    }

    public void ToggleCursorUnlocked()
    {
        // Toggle cursor lock state between locked and confined to the window
        Cursor.lockState = CursorLockMode.None;

        // Toggle cursor visibility
        Cursor.visible = true;
    }


    /* public float JumpForce()
    {
        return jumpForce;
    } */
}
