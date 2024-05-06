using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource backgroundMusicSource;
    public AudioClip[] backgroundMusicTracks;

    private void Start()
    {
        // Ensure that the AudioManager persists across scenes
        DontDestroyOnLoad(gameObject);

        // Subscribe to scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Play initial background music
        PlayBackgroundMusic(0);
    }

    private void OnDestroy()
    {
        // Unsubscribe from scene loaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Switch background music based on scene
        if (scene.name == "Menu" || scene.name == "CityScape1" || scene.name == "CityScape2" || scene.name == "CityScape3" || scene.name == "CityScape4") {
            PlayBackgroundMusic(0);
        }
    }

    public void PlayBackgroundMusic(int trackIndex)
    {
        if (trackIndex < 0 || trackIndex >= backgroundMusicTracks.Length)
        {
            Debug.LogWarning("Invalid track index.");
            return;
        }

        backgroundMusicSource.Stop();
        backgroundMusicSource.clip = backgroundMusicTracks[trackIndex];
        backgroundMusicSource.loop = true;
        backgroundMusicSource.Play();
    }

    public void StopBackgroundMusic()
    {
        backgroundMusicSource.Stop();
    }
}
