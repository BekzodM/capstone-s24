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
        databaseWrapper.getStructuresAll();
        databaseWrapper.getStructuresByLevel(1);
        databaseWrapper.getStructuresByType("offensive");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
