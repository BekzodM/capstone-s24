using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;


public class StructuresDef : MonoBehaviour
{
    private string dbName = "URI=file:GameData.db";
    // Start is called before the first frame update
    void Start()
    {
        StructuresDefInit();
    }

    public void StructuresDefInit()
    {
        //Create the db connection
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            // Read the SQL script from file
            string sqlScript = File.ReadAllText("Assets/Scripts/DatabaseScripts/StructuresDef.sql");


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


}