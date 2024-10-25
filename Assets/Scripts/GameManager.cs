using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance
    public int score; // Player score
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText; 
    public float maxTime = 60f; 
    private float currentTime; 

    public AudioClip gameOverSound;
    private AudioSource audioSource;

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
        ResetGame(); // Initialize the game
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

    public void ResetGame()
    {
        ResetScore(); // Reset the score
        currentTime = maxTime; // Reset current time to max time
        StartCoroutine(TimerCoroutine()); // Start the timer coroutine
    }

    private IEnumerator TimerCoroutine()
    {
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime; // Decrease current time
            UpdateTimerDisplay(); // Update the timer display
            yield return null; // Wait for the next frame
        }

        EndGame(); // Call EndGame when the timer reaches zero
    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.Ceil(currentTime).ToString(); // Update the timer text
        }
    }

    private void EndGame()
    {

        if (gameOverSound != null)
        {
            audioSource.PlayOneShot(gameOverSound);
        }

 
        StartCoroutine(WaitForSoundAndLoadMenu());
    }

    private IEnumerator WaitForSoundAndLoadMenu()
    {
        yield return new WaitForSeconds(gameOverSound.length);

        SceneManager.LoadScene(0);
    }
}
