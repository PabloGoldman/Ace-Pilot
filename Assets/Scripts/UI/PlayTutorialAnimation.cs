using UnityEngine;

public class PlayTutorialAnimation : MonoBehaviour
{
    public GameObject tutorialPanel;

    private void Start()
    {
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Time.timeScale = 1;
            tutorialPanel.SetActive(false);
        }
    }
}
