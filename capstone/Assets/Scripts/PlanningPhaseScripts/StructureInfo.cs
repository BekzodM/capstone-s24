using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureInfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public void SetInfoBasedOnButtonText(string text)
    {
        Debug.Log(text);
        //CODE TO DISPLAY INFO

    }
}
