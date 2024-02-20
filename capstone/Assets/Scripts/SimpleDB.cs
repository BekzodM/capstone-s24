using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;
using UnityEditor.Rendering;

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
            string sqlScript = File.ReadAllText("Assets/Scripts/CreateDB.sql");

            //set up an object (called "command") to allow db control
            using (var command = connection.CreateCommand())
            {
                //create a table called weapons if it doesnt already exist
                //has 2 fields: name (up to 20 chars) and damage (an INT)
                command.CommandText = sqlScript;
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }
}
