using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectPlayer : MonoBehaviour
{
    CharacterController characterController;
    [SerializeField] float speed;
    [SerializeField] GameObject EndDemoScreen;
    // Start is called before the first frame update
    void Start()
    {
        GameObject currentLevel = GameObject.Find("Level " + GameState.currentProgressLevel);
        characterController = GetComponent<CharacterController>();
        if (currentLevel != null)
        {
            Vector3 levelPosition = currentLevel.transform.position;
            transform.position = levelPosition;
        }
        if (GameState.currentProgressLevel >= 5)
        {
            EndDemoScreen.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController.Move(move * Time.deltaTime * speed);

        float rotateSpeed = 12f;
        transform.forward = Vector3.Slerp(transform.forward, move, Time.deltaTime * rotateSpeed);
    }
}
