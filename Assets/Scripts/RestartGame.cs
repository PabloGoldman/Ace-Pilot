using UnityEngine;

public class RestartGame : MonoBehaviour
{
    public void RestartingGame()
    {
        GameManager.Instance.RestartGame();
    }
}
