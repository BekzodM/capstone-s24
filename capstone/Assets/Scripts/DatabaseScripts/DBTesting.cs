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
        string[,] Array2d;
        Array2d = databaseWrapper.GetData("structures", "progress_level", 2);
        Array2d = databaseWrapper.GetData("structures", "structure_type", "Defense");
        Array2d = databaseWrapper.GetData("structures");
        // console printing results in set form, individual values in the form results[a,b] if you desire to access/handle single values.
        Debug.Log($"{Array2d[0, 0]} {Array2d[0, 1]} {Array2d[0, 2]} {Array2d[0, 3]}");

    }

    // Update is called once per frame
    void Update()
    {

    }
}
