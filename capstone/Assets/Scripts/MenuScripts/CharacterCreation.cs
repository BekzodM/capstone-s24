using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterCreation : MonoBehaviour
{
    [SerializeField] TMP_InputField newName;
    [SerializeField] TMP_Dropdown newClass;

    public void setName()
    {
        GameState.playerName = newName.text;
    }

    public void setClass()
    {
        GameState.playerType = newClass.captionText.text;
    }
}
