using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class LoadScene : MonoBehaviour
{
    [SerializeField] string SceneName;
    public void InteractLoadScene() {
        SceneManager.LoadScene(SceneName);
    }
}