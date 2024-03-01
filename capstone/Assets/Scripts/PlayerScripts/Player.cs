using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    // Player class should own game data associated with player

    [SerializeField] protected float jumpForce = 5f;

    Rigidbody playerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public float JumpForce()
    {
        return jumpForce;
    }
}
