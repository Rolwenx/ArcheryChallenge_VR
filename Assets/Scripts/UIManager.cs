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
 
        audioManager.PlayTrainingMusic(); 
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

    public void OnOptionsSelected()
    {
        audioManager.PlayMenuMusic(); 
    }

    public void OnDifficultySelected()
    {
        audioManager.PlayMenuMusic(); // Play menu music when choosing difficulty
        // Load difficulty selection scene here
    }
}
