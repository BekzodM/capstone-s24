using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;
using UnityEditor.Rendering;
using UnityEditor.MemoryProfiler;
using TMPro;
using Unity.VisualScripting;

public class SimpleDB : MonoBehaviour
{
    private string dbName = "URI=file:GameData.db";
    // Start is called before the first frame update
    void Start()
    {
        CreateDB();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateDB()
    {
        //Create the db connection
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            // Read the SQL script from file
            string sqlScript = File.ReadAllText("Assets/Scripts/DatabaseScripts/CreateDB.sql");

            //set up an object (called "command") to allow db control
            using (var command = connection.CreateCommand())
            {
                //create tables using sql commands from createdb.sql
                command.CommandText = sqlScript;

                //run the command
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        AddSave(1, -99, -99);
        AddSave(2, -99, -99);
    }

    public void AddSave(int slotNumber, int playerId, int progLevel)
    {
        //connect to DB
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            //set up an object (called "command") to allow db control
            using (var command = connection.CreateCommand())
            {
                //write insertion command
                command.CommandText = "INSERT INTO saves (slot_number, player_id, progress_level) VALUES ('" + slotNumber + "', '" + playerId + "', '" + progLevel + "')";

                //run the command
                command.ExecuteNonQuery();
            }

            connection.Close();

        }
    }
    //change
    public void ChangeSave(int saveId, int playerId, int progLevel)
    {
        //connect to DB
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            //set up an object (called "command") to allow db control
            using (var command = connection.CreateCommand())
            {
                //write Update command
                command.CommandText = "UPDATE saves SET player_id = " + playerId + ", progress_level = " + progLevel + " WHERE save_id = " + saveId;

                //run the command
                command.ExecuteNonQuery();
            }

            connection.Close();

        }
    }
}