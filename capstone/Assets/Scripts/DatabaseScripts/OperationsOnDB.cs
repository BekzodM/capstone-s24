using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;
using UnityEditor.Rendering;
using UnityEditor.MemoryProfiler;
using TMPro;
using Unity.VisualScripting;

public class OperationsOnDB : MonoBehaviour
{
    private string dbName = "URI=file:GameData.db";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Add
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

    //Remove
}
