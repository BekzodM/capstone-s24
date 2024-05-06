using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class LoadScene : MonoBehaviour
{
    [SerializeField] string SceneName;
    [SerializeField] int level;

    [SerializeField] GameObject LoadingScreen;
    public void Start()
    {
        Transform padlock = transform.Find("Locked");
        if (GameState.currentProgressLevel >= level)
        {
            padlock.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Transform canvas = transform.Find("Canvas");
            canvas.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Transform canvas = transform.Find("Canvas");
            canvas.gameObject.SetActive(false);
        }
    }
    public void InteractLoadScene()
    {
        if (GameState.currentProgressLevel >= level)
        {
            LoadingScreen.SetActive(true);
            SceneManager.LoadSceneAsync(SceneName);
        }
        else Debug.Log("Level Locked");
    }
}