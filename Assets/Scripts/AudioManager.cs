using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip menuMusic;
    public AudioClip trainingMusic;
    public AudioClip gameMusic;

    private void Awake()
    {
        // Ensure this GameObject persists across scenes
        DontDestroyOnLoad(gameObject);

        // Check for duplicate instances
        if (FindObjectsOfType<AudioManager>().Length > 1)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    private void Start()
    {
        PlayMenuMusic(); // Start with menu music
    }

    public void PlayMenuMusic()
    {
        PlayMusic(menuMusic);
    }

    public void PlayTrainingMusic()
    {
        PlayMusic(trainingMusic);
    }

    public void PlayGameMusic()
    {
        PlayMusic(gameMusic);
    }

    private void PlayMusic(AudioClip clip)
    {
        // If the current clip is the same as the one we want to play, do nothing
        if (audioSource.clip == clip) return;

        audioSource.Stop(); // Stop current music
        audioSource.clip = clip; // Assign new clip
        audioSource.Play(); // Play the new clip
    }
}
