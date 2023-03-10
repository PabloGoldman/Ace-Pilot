using UnityEngine;

public class PlayTutorialAnimation : MonoBehaviour
{
    public GameObject tutorialPanel;

    PlayerController player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        Time.timeScale = 0;
    }

    private void Update()
    {
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
        {
            //Aca arranca el juego despues de tocar
            Time.timeScale = 1;
            player.SetShootAudio(false);
            tutorialPanel.SetActive(false);
        }
    }
}
