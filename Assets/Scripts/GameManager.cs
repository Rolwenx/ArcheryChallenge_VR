using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Add this to use TextMeshPro

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance
    public int score; // Player score
    public TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI component

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object between scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    private void Start()
    {
        UpdateScoreDisplay(); // Initialize score display
    }

    public void AddScore(int points)
    {
        score += points; // Add points to score
        UpdateScoreDisplay(); // Update the score display
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score; // Update the text component
        }
    }

    public void ResetScore()
    {
        score = 0; // Reset the score
        UpdateScoreDisplay(); // Update the display to show the reset score
    }

}
