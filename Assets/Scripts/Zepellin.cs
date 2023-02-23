using UnityEngine;

public class Zepellin : MonoBehaviour
{
    ScoreManager scoreManager;

    private void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            scoreManager.IncreaseScore(1);
        }
    }
}
