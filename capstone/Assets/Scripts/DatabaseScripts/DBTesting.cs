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
        string[,] Array2d;

        // databaseWrapper.databaseInit();
        // databaseWrapper.InsertImmutables("Assets/Scripts/DatabaseScripts/DummyData.sql");

        Array2d = databaseWrapper.GetData("structures");

        Array2d = databaseWrapper.GetData("structures", "progress_level", 2);

        Array2d = databaseWrapper.GetData("structures", "structure_type", "Defense");

        // Array2d = databaseWrapper.GetData("players");
        // // console printing results in set form, individual values in the form results[a,b] if you desire to access/handle single values.
        // Debug.Log($"{Array2d[3, 0]} {Array2d[3, 1]} {Array2d[3, 2]} {Array2d[3, 3]}");
        // Debug.Log(Array2d);

        // databaseWrapper.SetData("players", "Morticia", "Mage", 100, 100);
        // Array2d = databaseWrapper.GetData("players", "player_name", "Morticia");
        // Debug.Log($"{Array2d[0, 0]} {Array2d[0, 1]} {Array2d[0, 2]} {Array2d[0, 3]}");

    }

}
