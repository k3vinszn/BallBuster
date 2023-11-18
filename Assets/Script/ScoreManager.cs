using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private int score = 0;
    [SerializeField] private Text pointsText;

    // stores the prefabs score
    private string playerPrefsKey = "PlayerScore";
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // doesnt destroy objects when loading the scene
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ResetScore(); // Reset the player's score to 0 when the game starts
    }

    public void IncreasePoints()
    {
        score += 1;

        // Check if the current score is higher than the saved high score
        int highScore = GetHighScore();
        if (score > highScore)
        {
            SetHighScore(score); // Update the high score
        }

        UpdatePointsText();
    }

    private void UpdatePointsText()
    {
        if (pointsText != null)
        {
            pointsText.text = score.ToString();
        }
    }

    private int GetHighScore()
    {
        // Retrieve the high score from PlayerPrefs
        return PlayerPrefs.GetInt("HighScore", 0);
    }

    private void SetHighScore(int newHighScore)
    {
        // Save the new high score to PlayerPrefs
        PlayerPrefs.SetInt("HighScore", newHighScore);
        PlayerPrefs.Save();
    }

    // Reset the score to 0
    public void ResetScore()
    {
        score = 0;
        SaveScore(); // Save the reset score to PlayerPrefs
        UpdatePointsText();
    }

    private void LoadScore()
    {
        // Load the player's score from PlayerPrefs
        score = PlayerPrefs.GetInt(playerPrefsKey, 0);
    }

    private void SaveScore()
    {
        // saves the player score
        PlayerPrefs.SetInt(playerPrefsKey, score);
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
        SaveScore(); //score is stored when the application closes
    }
}