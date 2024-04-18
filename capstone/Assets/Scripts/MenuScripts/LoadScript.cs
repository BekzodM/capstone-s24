using System;
using System.Collections;
using System.Collections.Generic;
//using Mono.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScript : MonoBehaviour
{
    public bool OverWriteSave = false;
    [SerializeField] TMP_Text Instruction;

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

    private void RenderSave(int saveId, TMP_Text SlotText)
    {
        string[,] saveData = AccessSave.GetData("saves", "save_id", saveId);
        if (saveData.GetLength(0) > 0 && saveData.GetLength(1) > 0)
        {
            Debug.Log($"{saveData[0, 0]} {saveData[0, 1]} {saveData[0, 2]}");
            string[,] playerData = AccessSave.GetData("players", "player_id", Int32.Parse(saveData[0, 1]));
            Debug.Log($"{playerData[0, 0]} {playerData[0, 1]} {playerData[0, 2]} {playerData[0, 3]} {playerData[0, 4]}");

            SlotText.text = "Save " + saveData[0, 0] + "\n" + playerData[0, 1] + "\n" + "Level: " + saveData[0, 2];
        }
    }

    public void SlotSelect(int saveId)
    {
        GameState.saveId = saveId;

        if (OverWriteSave == false)
        {
            Instruction.text = $"Slot {saveId} selected";
        }
        else if (OverWriteSave == true)
        {
            Instruction.text = $"You are about to overwrite save {saveId}!";
        }
        else
        {
            Debug.Log("No action taken, an error has occured: SlotSelect Load and Save");
        }

    }

    public void ContinueToGame()
    {
        if (OverWriteSave == true)
        {
            string[,] saveData = AccessSave.GetData("saves", "save_id", GameState.saveId);
            if (saveData.GetLength(0) > 0 && saveData.GetLength(1) > 0)
            {
                string[,] playerData = AccessSave.GetData("players", "player_id", Int32.Parse(saveData[0, 1]));
                AccessSave.DeleteDataById("players", "player_id", Int32.Parse(playerData[0, 0]));
                AccessSave.DeleteDataById("saves", "save_id", GameState.saveId);
            }

            GameState.playerHealth = 100;
            GameState.playerMana = 100;
            GameState.currentProgressLevel = 1;
            GameState.playerId = AccessSave.SetData("players", GameState.playerName, GameState.playerType, GameState.playerHealth, GameState.playerMana);
            GameState.saveId = AccessSave.SetData("saves", GameState.saveId, GameState.playerId, GameState.currentProgressLevel);
        }
        else if (OverWriteSave == false)
        {
            string[,] saveData = AccessSave.GetData("saves", "save_id", GameState.saveId);
            string[,] playerData = AccessSave.GetData("players", "player_id", Int32.Parse(saveData[0, 1]));

            GameState.playerId = Int32.Parse(playerData[0, 0]);
            GameState.playerName = playerData[0, 1];
            GameState.playerType = playerData[0, 2];
            GameState.playerHealth = Int32.Parse(playerData[0, 3]);
            GameState.playerMana = Int32.Parse(playerData[0, 4]);
            GameState.currentProgressLevel = Int32.Parse(saveData[0, 2]);
        }

        Debug.Log($"loadout: \nsave id: {GameState.saveId} \nplayer id: {GameState.playerId} \nplayer name: {GameState.playerName} \nplayer class: {GameState.playerType} \nplayer health: {GameState.playerHealth}hp \nplayer mana:{GameState.playerMana}mp \nplayer level: {GameState.currentProgressLevel}");
        // SceneManager.LoadScene("OverWorld");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }




}
