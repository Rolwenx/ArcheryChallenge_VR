using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource; 
    public AudioClip menuMusic; 
    public AudioClip trainingMusic; 
    public AudioClip gameMusic;

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
        if (audioSource.clip == clip) return; // If the same clip is already playing, do nothing

        audioSource.Stop(); // Stop current music
        audioSource.clip = clip; // Assign new clip
        audioSource.Play(); // Play the new clip
    }
}
