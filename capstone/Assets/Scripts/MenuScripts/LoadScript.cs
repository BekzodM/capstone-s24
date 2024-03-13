using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LoadScript : MonoBehaviour
{
    public bool OverWriteSave = false;
    [SerializeField] TMP_Text Instruction;
    [SerializeField] TMP_Text OverWriteMessage;

    int selectedSlot = 0;

    DatabaseWrapper AccessSave = new DatabaseWrapper();

    public TMP_Text Slot1;
    public TMP_Text Slot2;
    public TMP_Text Slot3;

    public void NewGame()
    {
        OverWriteSave = true;
        Instruction.text = "Select a load file to overwrite:";
    }
    public void LoadGame()
    {
        OverWriteSave = false;
        Instruction.text = "Select your load file:";
    }



    // RenderSaveSlots is called when Load is selected
    public void RenderSaveSlots()
    {
        RenderSave(1, Slot1);
        RenderSave(2, Slot2);
        RenderSave(3, Slot3);

    }

    private void RenderSave(int slotId, TMP_Text SlotText)
    {
        string[,] saveData = AccessSave.GetData("saves", "slot_id", slotId);
        Debug.Log($"{saveData[0, 0]} {saveData[0, 1]} {saveData[0, 2]} {saveData[0, 3]}");
        string[,] playerData = AccessSave.GetData("players", "player_id", Int32.Parse(saveData[0, 2]));
        Debug.Log($"{playerData[0, 0]} {playerData[0, 1]} {playerData[0, 2]} {playerData[0, 3]} {playerData[0, 4]}");

        SlotText.text = "Save " + saveData[0, 0] + "\n" + playerData[0, 1] + "\n" + "Level: " + saveData[0, 3];
    }

    public void SlotSelect(int slotId)
    {
        if (OverWriteSave == false)
        {
            selectedSlot = slotId;
            string[,] saveData = AccessSave.GetData("saves", "slot_id", slotId);
            string[,] playerData = AccessSave.GetData("players", "player_id", Int32.Parse(saveData[0, 2]));
            Debug.Log($"Slot {slotId} selected");

            GameState.saveId = 0;
            GameState.playerId = 0;
            GameState.playerName = "";
            GameState.playerType = "";
            GameState.playerHealth = 0;
            GameState.playerMana = 0;
        }
        else if (OverWriteSave == true)
        {
            selectedSlot = slotId;
            Debug.Log($"Slot {slotId} selected for overwrite");
            Instruction.text = $"You are about to overwrite save {slotId}!";
        }
        else
        {
            Debug.Log("No action taken, an error has occured: SlotSelect Load and Save");
        }

    }

    public void Continue()
    {
        if (OverWriteSave == true)
        {
            //get old save
            //get old player
            //Delete playerID from player
            //Delete saveID from saves

            //create new character
            // create new save

            GameState.saveId = 0;
            GameState.playerId = 0;
            GameState.playerName = "";
            GameState.playerType = "";
            GameState.playerHealth = 0;
            GameState.playerMana = 0;
            // AccessSave.SetData();
        }
    }




}
