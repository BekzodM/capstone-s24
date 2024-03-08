using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;

namespace DatabaseAccess
{
    public class DatabaseHandler
    {
        private string dbName = "URI=file:GameData.db";

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

        public void InsertData(string tableName, string insertCommand)
        {
            //connect to DB
            using (var connection = new SqliteConnection(dbName))
            {
                connection.Open();

                //set up an object (called "command") to allow db control
                using (var command = connection.CreateCommand())
                {
                    //write insertion command
                    command.CommandText = "INSERT INTO " + tableName + insertCommand;

                    //run the command
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public string[,] SelectData(string tableName, string selectCommand, string specifierCommand)
        {
            //string 2d array that stores results of the query
            string[,] results;
            //find out row count to initialize the 2d array
            int rowCount = 0;
            int colCount = 0;

            //connect to DB
            using (var connection = new SqliteConnection(dbName))
            {
                connection.Open();

                //set up an object (called "command") to allow db control
                using (var command = connection.CreateCommand())
                {
                    //get the results row count for rowCount
                    command.CommandText = "SELECT COUNT(*) FROM " + tableName;
                    rowCount = Convert.ToInt32(command.ExecuteScalar());
                    command.CommandText = "SELECT COUNT(*) FROM pragma_table_info('" + tableName + "')";
                    colCount = Convert.ToInt32(command.ExecuteScalar());
                    // 7 columns as per structures table schema
                    results = new string[rowCount, colCount];

                    command.CommandText = selectCommand + specifierCommand;

                    //run the command
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            // Debug.Log("Entry Exists");

                            //2d array layout follows table schema
                            for (int i = 0; i < rowCount; i++)
                            {
                                reader.Read();
                                for (int j = 0; j < colCount; j++)
                                {
                                    results[i, j] = reader[reader.GetName(j)].ToString();
                                }
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
        public void AddStructureDevUseOnly(string structName, string structType, int structDamage, int structHealth, int structCost, int progLevel)
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