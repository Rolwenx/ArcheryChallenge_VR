using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UIManager : MonoBehaviour
{
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void OnStartButtonPressed()
    {
        // Logic to start the game or go to the training mode
        audioManager.PlayTrainingMusic(); // Change this based on the selected mode
    }

    public void OnTrainingModeSelected()
    {
        audioManager.PlayTrainingMusic();
    }

    public void OnGameModeSelected()
    {
        audioManager.PlayGameMusic();
    }

    public void OnBackToMenu()
    {
        audioManager.PlayMenuMusic();
    }
}
