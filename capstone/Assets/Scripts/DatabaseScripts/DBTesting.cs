using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBTesting : MonoBehaviour
{
    DatabaseWrapper databaseWrapper;
    // Start is called before the first frame update
    void Start()
    {
        databaseWrapper = new DatabaseWrapper();
        // databaseWrapper.databaseInit();
        databaseWrapper.GetData("structures", "progress_level", 2);
        databaseWrapper.GetData("structures", "structure_type", "Defense");
        databaseWrapper.GetData("structures");

    }

    // Update is called once per frame
    void Update()
    {

    }
}
