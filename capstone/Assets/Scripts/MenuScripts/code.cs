using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro namespace
using Mono.Data.Sqlite;
using System;



public class LoadSavesMenuScript : MonoBehaviour
{

    DatabaseWrapper AccessSave = new DatabaseWrapper();
    public bool OverWriteSave = false;
    public bool LoadSave = false;
    // private string dbName = "URI=file:GameData.db";
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

    private void RenderSave(int slotId, TMP_Text SlotText)
    {
        string[,] saveData = AccessSave.GetData("saves", "slot_id", slotId);
        Debug.Log($"{saveData[0, 0]} {saveData[0, 1]} {saveData[0, 2]}");
        string[,] playerData = AccessSave.GetData("players", "player_id", Int32.Parse(saveData[0, 2]));
        Debug.Log($"{playerData[0, 0]} {playerData[0, 1]} {playerData[0, 2]} {playerData[0, 3]} ");

        SlotText.text = "Save " + saveData[0, 0] + "\n" + playerData[0, 1] + "\n" + "Level: " + saveData[0, 3];
    }
}