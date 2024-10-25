using System.Collections;
using System.Collections.Generic;
using System.IO; // Needed for file handling
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

    private string filePath;
    private Scoreboard scoreboard = new Scoreboard();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object between scenes
            filePath = Path.Combine(Application.persistentDataPath, "scores.json");
            LoadScores(); // Load scores when the game starts
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
        currentTime = maxTime;
        StartCoroutine(TimerCoroutine());
    }

    private IEnumerator TimerCoroutine()
    {
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime; // Decrease current time
            UpdateTimerDisplay(); // Update the timer display
            yield return null; // Wait for the next frame
        }

        EndGame();
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

        VirtualKeyboard virtualKeyboard = FindObjectOfType<VirtualKeyboard>();
        if (virtualKeyboard != null)
        {
            virtualKeyboard.ShowKeyboard(); // This will display the keyboard
        }

        // Wait for the sound to finish before transitioning to the main menu
        StartCoroutine(WaitForSoundAndLoadMenu());
    }

    private IEnumerator WaitForSoundAndLoadMenu()
    {
        yield return new WaitForSeconds(gameOverSound.length);

        // Optionally, you can hide the keyboard here, or leave it open for input
        // VirtualKeyboard virtualKeyboard = FindObjectOfType<VirtualKeyboard>();
        // if (virtualKeyboard != null)
        // {
        //     virtualKeyboard.HideKeyboard();
        // }

        // Load the main menu
        SceneManager.LoadScene(0);
    }


    public void SaveScore(string playerName, int points, string difficulty)
    {
        // Create a new PlayerScore instance
        PlayerScore newScore = new PlayerScore
        {
            playerName = playerName,
            score = points,
            difficulty = difficulty
        };

        // Add the new score to the scoreboard
        scoreboard.scores.Add(newScore);

        // Save the scores to a file
        string json = JsonUtility.ToJson(scoreboard, true);
        File.WriteAllText(filePath, json);
    }

    public void LoadScores()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            scoreboard = JsonUtility.FromJson<Scoreboard>(json);
        }
    }
}
