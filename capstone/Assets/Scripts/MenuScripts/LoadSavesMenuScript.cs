using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro namespace
using Mono.Data.Sqlite;



public class LoadSavesMenuScript : MonoBehaviour
{
    public bool OverWriteSave = false;
    public bool LoadSave = false;
    private string dbName = "URI=file:GameData.db";
    public TMP_Text Slot1;
    public TMP_Text Slot2;
    public TMP_Text Slot3;


    // RenderSaveSlots is called when Load is selected
    public void RenderSaveSlots()
    {
        RenderSave(1, Slot1);
        RenderSave(2, Slot2);
        RenderSave(3, Slot3);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void RenderSave(int id, TMP_Text SlotText)
    {
        string query = "SELECT * FROM saves WHERE save_Id = " + id;
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = query;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        Debug.Log("Entry Exists");
                        SlotText.text = "Save " + reader["save_Id"] + "\n" + reader["player_Id"] + "\n" + "Level: " + reader["progress_Level"];

                    }
                    else
                    {
                        Debug.Log("Entry Does not Exist");
                        SlotText.text = "No Save";
                    }
                }
            }

            connection.Close();
        }
    }
}
