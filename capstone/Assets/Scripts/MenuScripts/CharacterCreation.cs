using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterCreation : MonoBehaviour
{
    [SerializeField] TMP_InputField newName;

    void Start()
    {
        GameState.playerName = "Mr. Player";
    }

    public void setName()
    {
        GameState.playerName = newName.text;
    }
}
