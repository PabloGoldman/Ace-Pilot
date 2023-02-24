using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public GameObject endGamePanel; 
    public TextMeshProUGUI finalScoreText;

    private int score = 0;

    private static ScoreManager instance;

    // Get the Singleton instance
    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ScoreManager>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<ScoreManager>();
                    singletonObject.name = typeof(ScoreManager).ToString() + " (Singleton)";
                    //DontDestroyOnLoad(singletonObject);
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            //Destroy(gameObject);
        }
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

    public void DisplayFinalScore()
    {
        endGamePanel.SetActive(true);
        finalScoreText.text = score.ToString();
    }

    void DisplayScore()
    {
        scoreText.text = score.ToString();
    }
}
