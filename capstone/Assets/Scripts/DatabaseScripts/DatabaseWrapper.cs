using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DatabaseAccess;

public class DatabaseWrapper
{
    private DatabaseHandler databaseHandler;

    public DatabaseWrapper()
    {
        databaseHandler = new DatabaseHandler();
    }

    public void databaseInit()
    {
        databaseHandler.CreateDB();
    }

    public void InsertImmutables(string pathToSqlFile)
    {
        databaseHandler.InsertImmutables(pathToSqlFile);
    }

    public int SetData(string tableName, int val1, int val2, int val3)
    {
        string insertCommand = "";
        switch (tableName)
        {
            case "saves":
                // code block
                insertCommand = "INSERT INTO saves (save_id, player_id, progress_level) VALUES (" + val1 + ", " + val2 + ", " + val3 + ")";
                break;
            case "inventory":
                // code block
                insertCommand = "INSERT INTO inventory (player_id, item_id, item_quantity) VALUES (" + val1 + ", " + val2 + ", " + val3 + ")";
                break;
            default:
                // code block
                //shouldnt get here
                Debug.Log("table does not exist");
                break;
        }

        return databaseHandler.ExecuteScalar(insertCommand);

    }

    public int SetData(string tableName, string val1, string val2, int val3 = 0, int val4 = 0, int val5 = 0, int val6 = 0)
    {
        string insertCommand = "";

        switch (tableName)
        {
            case "gameItems":
                // code block
                Debug.Log("gameitems case");
                insertCommand = "INSERT INTO gameItems (item_name, item_type, item_damage, item_armor, progress_level) VALUES ('" + val1 + "', '" + val2 + "', " + val3 + " , " + val4 + ", " + val5 + ")";
                break;
            case "structures":
                // code block
                Debug.Log("structs case");
                insertCommand = "INSERT INTO structures (structure_name, structure_type, structure_damage, structure_health, structure_cost, progress_level) VALUES ('" + val1 + "', '" + val2 + "', " + val3 + " , " + val4 + ", " + val5 + ", " + val6 + ")";
                break;
            case "enemies":
                // code block
                Debug.Log("enemies case");
                insertCommand = "INSERT INTO enemies (enemy_name, enemy_type, enemy_damage, enemy_health, enemy_mana, progress_level) VALUES ('" + val1 + "', '" + val2 + "', " + val3 + " , " + val4 + ", " + val5 + ", " + val6 + ")";
                break;
            case "players":
                // code block
                Debug.Log("players case");
                insertCommand = "INSERT INTO players (player_name, player_type, player_health, player_mana) VALUES ('" + val1 + "', '" + val2 + "', " + val3 + " , " + val4 + ")";
                break;
            default:
                // code block
                //shouldnt get here
                Debug.Log("table does not exist");
                break;
        }

        return databaseHandler.ExecuteScalar(insertCommand);

    }

    //overloading function for int or string specifier. for string, needs to be surrounded by quotations
    public string[,] GetData(string tableName, string specifierColumn = null, string specifier = null)
    {
        string selectCommand = "SELECT * FROM " + tableName;
        string specifierCommand = "";

        if (specifierColumn != null && specifier != null)
        {
            specifierCommand = " WHERE " + specifierColumn + " = '" + specifier + "'";
        }

        string[,] results = databaseHandler.SelectData(tableName, selectCommand, specifierCommand);

        //returns results as a 2d array. if data does not exist, 2d array is empty
        return results;
    }

    //overloading function for int or string specifier. for int, no quotations
    public string[,] GetData(string tableName, string specifierColumn, int specifier)
    {
        string selectCommand = "SELECT * FROM " + tableName;
        string specifierCommand = " WHERE " + specifierColumn + " = " + specifier;

        string[,] results = databaseHandler.SelectData(tableName, selectCommand, specifierCommand);

        //returns results as a 2d array. if data does not exist, 2d array is empty
        return results;
    }

    public void DeleteDataById(string tableName, string specifierColumn, int specifierId)
    {
        string deleteCommand = "DELETE FROM " + tableName + " WHERE " + specifierColumn + " = " + specifierId;
        databaseHandler.NonQuery(deleteCommand);

    }


    public void UpdateData(int progLevel, int saveId)
    {
        string updateCommand = "UPDATE saves SET progress_level = " + progLevel + " WHERE save_id = " + saveId;
        databaseHandler.NonQuery(updateCommand);
    }
}
