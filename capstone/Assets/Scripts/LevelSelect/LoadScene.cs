using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class LoadScene : MonoBehaviour
{
    [SerializeField] string SceneName;
    [SerializeField] int level; 
    public void Start() {
        Transform padlock = transform.Find("Locked");
        if (GameState.currentProgressLevel >= level) {
            padlock.gameObject.SetActive(false);
        }
    }
    public void InteractLoadScene() {
        if(GameState.currentProgressLevel >= level) {
            SceneManager.LoadScene(SceneName);
        }
        else Debug.Log("Level Locked");
    }
}