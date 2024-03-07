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

    public void SetData(string tableName, int val1, int val2, int val3, int val4)
    {

    }

    public void SetData(string tableName, string val1, string val2, int val3, int val4)
    {

    }

    //overloading function for int or string specifier
    public string[,] GetData(string tableName, string specifierColumn = null, string specifier = null)
    {
        string selectCommand = "SELECT * FROM " + tableName;
        string specifierCommand = "";

        if (specifierColumn != null && specifier != null)
        {
            specifierCommand = " WHERE " + specifierColumn + " = '" + specifier + "'";
        }

        string[,] results = _databaseHandler.SelectData(tableName, selectCommand, specifierCommand);

        return results;
    }

    //overloading function for int or string specifier
    public string[,] GetData(string tableName, string specifierColumn, int specifier)
    {
        string selectCommand = "SELECT * FROM " + tableName;
        // string specifierCommand = "WHERE " + specifierColumn + " <= " + specifier;
        string specifierCommand = " WHERE " + specifierColumn + " <= " + specifier;


        string[,] results = _databaseHandler.SelectData(tableName, selectCommand, specifierCommand);

        return results;
    }


    public void setUpStructureDevFunc()
    {
        _databaseHandler.AddStructureDevUseOnly("Machine Gun", "Offense", 10, 100, 10, 1);
        _databaseHandler.AddStructureDevUseOnly("Trap", "Offense", 0, 100, 10, 1);
        _databaseHandler.AddStructureDevUseOnly("Tripwire", "Offense", 0, 100, 10, 1);
    }


}
