using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private int score;

    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // Keep GameManager between scenes
            ResetScore(); // Reset score at the start
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetScore()
    {
        score = 0;
    }

    public void AddScore(int points)
    {
        score += points;
    }

    public int GetScore()
    {
        return score;
    }
}
