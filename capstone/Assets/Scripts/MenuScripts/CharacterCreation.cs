using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterCreation : MonoBehaviour
{
    [SerializeField] TMP_InputField newName;
    [SerializeField] TMP_Dropdown newClass;

    void Start()
    {
        GameState.playerName = "Mr. Player";
        GameState.playerType = newClass.options[0].text;
    }

    public void setName()
    {
        GameState.playerName = newName.text;
    }

    public void setClass()
    {
        GameState.playerType = newClass.options[newClass.value].text;
    }
}
