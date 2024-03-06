using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DatabaseAccess;

public class DatabaseWrapper
{
    private DatabaseHandler _databaseHandler;

    public DatabaseWrapper()
    {
        _databaseHandler = new DatabaseHandler();
    }

    public void databaseInit()
    {
        _databaseHandler.CreateDB();
    }

    //overloading function for int or string specifier
    public void GetData(string tableName, string specifierColumn = null, string specifier = null)
    {
        string selectCommand = "SELECT * FROM " + tableName;
        string specifierCommand = "";

        if (specifierColumn != null && specifier != null)
        {
            specifierCommand = " WHERE " + specifierColumn + " = '" + specifier + "'";
        }

        string[,] results = _databaseHandler.SelectData(tableName, selectCommand, specifierCommand);
        // console printing results in set form, individual values in the form results[a,b] if you desire to access/handle single values.
        Debug.Log($"{results[0, 0]} {results[0, 1]} {results[0, 2]} {results[0, 3]}");
    }

    //overloading function for int or string specifier
    public void GetData(string tableName, string specifierColumn, int specifier)
    {
        string selectCommand = "SELECT * FROM " + tableName;
        // string specifierCommand = "WHERE " + specifierColumn + " <= " + specifier;
        string specifierCommand = " WHERE " + specifierColumn + " <= " + specifier;


        string[,] results = _databaseHandler.SelectData(tableName, selectCommand, specifierCommand);
        // console printing results in set form, individual values in the form results[a,b] if you desire to access/handle single values.
        Debug.Log($"{results[0, 0]} {results[0, 1]} {results[0, 2]} {results[0, 3]}");
    }


    public void setUpStructureDevFunc()
    {
        _databaseHandler.AddStructureDevUseOnly("Machine Gun", "Offense", 10, 100, 10, 1);
        _databaseHandler.AddStructureDevUseOnly("Trap", "Offense", 0, 100, 10, 1);
        _databaseHandler.AddStructureDevUseOnly("Tripwire", "Offense", 0, 100, 10, 1);
    }


}
