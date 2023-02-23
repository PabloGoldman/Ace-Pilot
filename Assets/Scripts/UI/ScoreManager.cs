using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score = 0;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        // Initialize the score text with the starting score value
        DisplayScore();
    }

    public void IncreaseScore(int amount)
    {
        // Increase the score value and update the score text
        score += amount;
        DisplayScore();
    }

    public void DecreaseScore(int amount)
    {
        // Decrease the score value and update the score text
        score -= amount;
        DisplayScore();
    }

    void DisplayScore()
    {
        scoreText.text = score.ToString();
    }
}
