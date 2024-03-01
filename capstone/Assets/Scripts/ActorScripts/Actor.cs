using System.Collections;
using System.Collections.Generic;
// using System.Numerics; // What is this for?
using UnityEngine;

public class Actor : MonoBehaviour
{
    // Protected fields because inherited classes need to access these values

    [SerializeField] protected float moveSpeed = 7f;
    //[SerializeField] private GameInput gameInput;

    protected bool isWalking;

    private void Update()
    {
        //Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        //Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        //transform.position += moveDir * moveSpeed * Time.deltaTime;

        //isWalking = moveDir != Vector3.zero;

        //float rotateSpeed = 12f;
        //transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    // moveSpeed getter
    public float MoveSpeed()
    {
        return moveSpeed;
    }
}
