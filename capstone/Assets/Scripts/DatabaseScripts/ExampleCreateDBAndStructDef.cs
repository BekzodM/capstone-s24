using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class ExampleCreateDBAndStructDef : MonoBehaviour
{
    private DatabaseWrapper databaseWrapper = new DatabaseWrapper();
    void Start()
    {

        string databasePath = "GameData.db"; // Replace "your_database.db" with the path to your SQLite database file

        if (File.Exists(databasePath))
        {
            Debug.Log("The database file exists.");
            // Do something if the file exists, like open the database connection
        }
        else
        {
            Debug.Log("The database file does not exist.");
            //create DB
            databaseWrapper.databaseInit();
            //database created in project root

            //Insert your stuff, put path of your sql file
            databaseWrapper.InsertImmutables("Assets/Scripts/DatabaseScripts/Structures.sql");
            databaseWrapper.InsertImmutables("Assets/Scripts/DatabaseScripts/StructureUpgrades.sql");
        }

    }
}
