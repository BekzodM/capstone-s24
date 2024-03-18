using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleCreateDBAndStructDef : MonoBehaviour
{
    private DatabaseWrapper databaseWrapper = new DatabaseWrapper();
    void Start()
    {
        //create DB
        databaseWrapper.databaseInit();
        //database created in project root

        //Insert your stuff, put path of your sql file
        databaseWrapper.InsertImmutables("Assets/Scripts/DatabaseScripts/Structures.sql");

        string[,] results;
        //Use Getter
        results = databaseWrapper.GetData("structures");

        int structID = Int32.Parse(results[0, 0]);
        string structName = results[0, 1];
        string structType = results[0, 2];
        string structDescription = results[0, 3];
        int structDamage = Int32.Parse(results[0, 4]);
        int structHealth = Int32.Parse(results[0, 5]);
        int structCost = Int32.Parse(results[0, 6]);
        int progLevel = Int32.Parse(results[0, 7]);



        results = databaseWrapper.GetData("structures", "progress_level", 1);
        //This only gets entries with exactly progLevel = 2
        //if you want something like all less than 2
        //you gotta ask for every level one by one for now

        //results = databaseWrapper.GetData("structures", "structure_type", "Defense");

        //Use Setter
        //databaseWrapper.SetData("gameItems", "Bazooka", "Weapon", 999, 0, 1);

    }
}
