using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    //private ActorInputActions actorInputActions;

    //private void Awake()
    //{
    //    actorInputActions = new ActorInputActions();
    //    actorInputActions.Player.Enable();
    //}
    //public Vector2 GetMovementVectorNormalized()
    //{
    //    Vector2 inputVector = actorInputActions.Player.Move.ReadValue<Vector2>();

    //    inputVector = inputVector.normalized;

    //    Debug.Log(inputVector);

    //    return inputVector;
    //}
}

// OLD INPUT METHOD
//
// if (Input.GetKey(KeyCode.W))
// {
//     inputVector.y += 1;
// }
// if (Input.GetKey(KeyCode.S))
// {
//     inputVector.y -= 1;
// }
// if (Input.GetKey(KeyCode.A))
// {
//     inputVector.x -= 1;
// }
// if (Input.GetKey(KeyCode.D))
// {
//     inputVector.x += 1;
// }