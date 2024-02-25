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
        AddSave(-99, -99);
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
    }

    public void AddSave(int playerId, int progLevel)
    {
        //connect to DB
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            //set up an object (called "command") to allow db control
            using (var command = connection.CreateCommand())
            {
                //write insertion command
                command.CommandText = "INSERT INTO saves (player_id, progress_level) VALUES ('" + playerId + "', '" + progLevel + "')";

                //run the command
                command.ExecuteNonQuery();
            }

            connection.Close();

        }
    }

    public void ChangeSave(int save_id, int playerId, int progLevel)
    {
        //connect to DB
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            //set up an object (called "command") to allow db control
            using (var command = connection.CreateCommand())
            {
                //write insertion command
                command.CommandText = "INSERT INTO saves (player_id, progress_level) VALUES ('" + playerId + "', '" + progLevel + "')";

                //run the command
                command.ExecuteNonQuery();
            }

            connection.Close();

        }
    }

}