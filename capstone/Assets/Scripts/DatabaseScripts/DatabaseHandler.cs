using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

namespace DatabaseAccess
{
    public class DatabaseHandler
    {
        private string dbName = "URI=file:GameData.db";

        public string[,] getStructures(string selectCommand, string specifierCommand = null)
        {
            //string 2d array that stores results of the query
            string[,] results;
            //find out row count to initialize the 2d array
            int rowCount = 0;

            //connect to DB
            using (var connection = new SqliteConnection(dbName))
            {
                connection.Open();

                //set up an object (called "command") to allow db control
                using (var command = connection.CreateCommand())
                {
                    //get the results row count for rowCount
                    command.CommandText = "SELECT COUNT(*) FROM structures";
                    rowCount = Convert.ToInt32(command.ExecuteScalar());
                    // 7 columns as per structures table schema
                    results = new string[rowCount, 7];

                    //write insertion command
                    command.CommandText = selectCommand + specifierCommand;

                    //run the command
                    using (var reader = command.ExecuteReader())
                    {
                        // reusing this variable as index for the results 2d array
                        rowCount = 0;
                        if (reader.HasRows)
                        {
                            Debug.Log("Entry Exists");
                            while (reader.Read())
                            {
                                //2d array layout follows table schema
                                results[rowCount, 0] = reader[0].ToString();
                                results[rowCount, 1] = reader["structure_name"].ToString();
                                results[rowCount, 2] = reader["structure_type"].ToString();
                                results[rowCount, 3] = reader["structure_damage"].ToString();
                                results[rowCount, 4] = reader["structure_health"].ToString();
                                results[rowCount, 5] = reader["structure_cost"].ToString();
                                results[rowCount, 6] = reader["progress_level"].ToString();
                                rowCount++;
                            }
                        }
                        else
                        {
                            Debug.Log("Entry Does not Exist");
                        }
                    }
                }

                connection.Close();
                return results;

            }
        }
        public void addStructureDevUseOnly(string structName, string structType, int structDamage, int structHealth, int structCost, int progLevel)
        {

            //connect to DB
            using (var connection = new SqliteConnection(dbName))
            {
                connection.Open();

                //set up an object (called "command") to allow db control
                using (var command = connection.CreateCommand())
                {
                    //write insertion command
                    command.CommandText = "INSERT INTO structures (structure_name, structure_type, structure_damage, structure_health, structure_cost, progress_level) VALUES ('" + structName + "', '" + structType + "', '" + structDamage + "' , '" + structHealth + "', '" + structCost + "', '" + progLevel + "')";

                    //run the command
                    command.ExecuteNonQuery();
                }

                connection.Close();

            }
        }
    }
}

// CREATE TABLE IF NOT EXISTS structures(
//     structure_id INTEGER NOT NULL PRIMARY KEY,
//     structure_name VARCHAR(20),
//     structure_type VARCHAR(20),
//     structure_damage INTEGER,
//     structure_health INTEGER,
//     structure_cost INTEGER,
//     progress_level INTEGER
// );