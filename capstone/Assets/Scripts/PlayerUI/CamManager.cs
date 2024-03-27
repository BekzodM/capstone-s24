using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    public GameObject mainCam;
    public GameObject mapCam;

    // Update is called once per frame

    private void Start()
    {
        mainCam.SetActive(true);
        mapCam.SetActive(false);
    }
    void Update()
    {
        if (Input.GetButtonDown("MapKey"))
        {
            if (mainCam.activeInHierarchy)
            { 
                mainCam.SetActive(false);
                mapCam.SetActive(true);
            }
            else
            {
                mainCam.SetActive(true);
                mapCam.SetActive(false);
            }
        }
    }
}
