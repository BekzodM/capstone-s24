using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetTmpdataTest : MonoBehaviour
{

    [SerializeField] TMP_Text tempData;
    // Start is called before the first frame update
    void Start()
    {
        tempData.text = $"Game Scene loadout: \nsave id: {GameState.saveId} \nplayer id: {GameState.playerId} \nplayer name: {GameState.playerName} \nplayer class: {GameState.playerType} \nplayer health: {GameState.playerHealth}hp \nplayer mana:{GameState.playerMana}mp \nplayer level: {GameState.currentProgressLevel}";
        Debug.Log($" Game Scene loadout: \nsave id: {GameState.saveId} \nplayer id: {GameState.playerId} \nplayer name: {GameState.playerName} \nplayer class: {GameState.playerType} \nplayer health: {GameState.playerHealth}hp \nplayer mana:{GameState.playerMana}mp \nplayer level: {GameState.currentProgressLevel}");

    }

    // Update is called once per frame
    void Update()
    {

    }
}
