using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    private DatabaseWrapper databaseWrapper = new DatabaseWrapper();
    // Start is called before the first frame update
    void Start()
    {
        databaseWrapper.databaseInit();
    }
}
