using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = AudioManager.SfxVolume;

        InvokeRepeating("UpdateVolume", 0.1f, 0.1f);
    }

    private void UpdateVolume()
    {
        audioSource.volume = AudioManager.SfxVolume;
    }
}
