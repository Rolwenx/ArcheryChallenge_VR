using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VirtualKeyboard : MonoBehaviour
{
    public TMP_InputField inputField; // Assign in Inspector
    public GameObject keyboardPanel; // Assign in Inspector
    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    public void ShowKeyboard()
    {
        keyboardPanel.SetActive(true);
    }

    public void HideKeyboard()
    {
        keyboardPanel.SetActive(false);
    }

    public void AddCharacter(string character)
    {
        inputField.text += character;
    }

    public void SubmitName()
    {
        string playerName = inputField.text;
        int playerScore = GameManager.Instance.score; // Get the player's score
        string difficulty = PlayerPrefs.GetString("level"); // Assume you have this variable

        // Save the score using the GameManager's method
        GameManager.Instance.SaveScore(playerName, playerScore, difficulty);

        HideKeyboard(); // Hide the keyboard after submission
                        // Optionally: Load menu or handle game transition here
    }



    public void ClearInput()
    {
        inputField.text = string.Empty;
    }
}
