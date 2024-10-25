using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource audioSource;
    public AudioClip menuMusic;
    public AudioClip trainingMusic;
    public AudioClip gameMusic;

    public static float SfxVolume { get; private set; } = 1f; // Static SFX volume

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMenuMusic();
    }

    public void PlayMenuMusic() => PlayMusic(menuMusic);
    public void PlayTrainingMusic() => PlayMusic(trainingMusic);
    public void PlayGameMusic() => PlayMusic(gameMusic);

    private void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip == clip) return;
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void SetBackgroundMusicVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void SetSfxVolume(float volume)
    {
        SfxVolume = volume;
    }
}
