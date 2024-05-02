using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gamePhase;
    public GameObject planningPhase;

    public static GameManager Instance;

    public gameStateManager State;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        updateGameState(gameStateManager.planningPhaseState);
    }

    // Update is called once per frame
    void Update()
    {
        if (State == gameStateManager.planningPhaseState)
        {
            gamePhase.SetActive(false);
            planningPhase.SetActive(true);
        }
        else if (State == gameStateManager.gamePlayState)
        {
            gamePhase.SetActive(true);
            planningPhase.SetActive(false);
        }
    }

    public void updateGameState(gameStateManager newState)
    {
        State = newState;

        switch (newState)
        {
            case gameStateManager.menuState:
                break;
            case gameStateManager.levelSelectState:
                break;
            case gameStateManager.planningPhaseState:
                break;
            case gameStateManager.gamePlayState:
                break;
            case gameStateManager.gamePauseState:
                break;
            case gameStateManager.gameOverState:
                break;
        }
    }
}

public enum gameStateManager
{
    menuState,
    levelSelectState,
    planningPhaseState,
    gamePlayState,
    gamePauseState,
    gameOverState
}