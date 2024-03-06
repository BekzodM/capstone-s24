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

    public void getStructuresAll()
    {
        string selectCommand = "SELECT * FROM structures";
        string[,] results = _databaseHandler.getStructures(selectCommand);
        // console printing results in set form, individual values in the form results[a,b] if you desire to access/handle single values.
        Debug.Log($"{results[0, 0]} {results[0, 1]} {results[0, 2]} {results[0, 3]} {results[0, 4]} {results[0, 5]} {results[0, 6]}");
    }
    public void getStructuresByType(string Type)
    {
        string selectCommand = "SELECT * FROM structures";
        string specifierCommand = " WHERE structure_type = '" + Type + "'";
        string[,] results = _databaseHandler.getStructures(selectCommand, specifierCommand);
        // console printing results in set form, individual values in the form results[a,b] if you desire to access/handle single values.
        Debug.Log($"{results[0, 0]} {results[0, 1]} {results[0, 2]} {results[0, 3]} {results[0, 4]} {results[0, 5]} {results[0, 6]}");

    }
    public void getStructuresByLevel(int Level)
    {
        string selectCommand = "SELECT * FROM structures";
        string specifierCommand = " WHERE progress_level <= " + Level;
        string[,] results = _databaseHandler.getStructures(selectCommand, specifierCommand);
        // console printing results in set form, individual values in the form results[a,b] if you desire to access/handle single values.
        Debug.Log($"{results[0, 0]} {results[0, 1]} {results[0, 2]} {results[0, 3]} {results[0, 4]} {results[0, 5]} {results[0, 6]}");

    }

    public void setUpStructureDevFunc()
    {
        _databaseHandler.addStructureDevUseOnly("turret", "offensive", 10, 100, 10, 1);
        _databaseHandler.addStructureDevUseOnly("wall", "defensive", 0, 100, 10, 1);
        _databaseHandler.addStructureDevUseOnly("fence", "defensive", 0, 100, 10, 1);
    }


}
