using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class Scoreboard
{
    public List<PlayerScore> scores = new List<PlayerScore>();
}

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int score;
    public TextMeshProUGUI scoreText;
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

    public void SaveScores()
    {
        PlayerScore newScore = new PlayerScore
        {
            playerName = "PlayerName", // Replace with actual player name
            score = score,
            difficulty = PlayerPrefs.GetString("level") // Assumes you are storing difficulty level in PlayerPrefs
        };

        scoreboard.scores.Add(newScore);
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
