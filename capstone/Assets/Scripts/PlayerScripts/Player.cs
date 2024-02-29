using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class Player : Actor
{
    // The input class used for controlling the player class
    // Player class should own game data associated with player
    //[SerializeField] TestInput testInput;

    Rigidbody playerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        //Vector2 inputVector = testInput.GetMovementVectorNormalized();
        //Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        //transform.position += moveDir * moveSpeed * Time.deltaTime;

        //isWalking = moveDir != Vector3.zero;

        //float rotateSpeed = 12f;
        //transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 inputVector = testInput.GetMovementVectorNormalized();
        //playerRigidbody.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * moveSpeed, ForceMode.Force);
    }
}
