using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour
{
    public Slider backgroundMusicSlider;
    public Slider sfxVolumeSlider;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.Instance;

        backgroundMusicSlider.value = audioManager.audioSource.volume;
        sfxVolumeSlider.value = AudioManager.SfxVolume;

        backgroundMusicSlider.onValueChanged.AddListener(SetBackgroundMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSfxVolume);
    }

    private void SetBackgroundMusicVolume(float volume)
    {
        audioManager.SetBackgroundMusicVolume(volume);
    }

    private void SetSfxVolume(float volume)
    {
        audioManager.SetSfxVolume(volume);
    }
}
