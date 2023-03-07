using UnityEngine;
using UnityEngine.UI;

public class ButtonSpriteChange : MonoBehaviour
{
    public Sprite originalSprite; // The original sprite of the button
    public Sprite touchedSprite; // The sprite to show when the button is touched

    private Image buttonImage; // The Image component of the button

    bool isOriginalSprite;

    // Start is called before the first frame update
    void Start()
    {
        buttonImage = GetComponent<Image>();

        isOriginalSprite = true;

        buttonImage.sprite = originalSprite;
    }

    public void SwapSprite()
    {
        if (isOriginalSprite)
        {
            buttonImage.sprite = touchedSprite;

            isOriginalSprite = false;
        }
        else
        {
            buttonImage.sprite = originalSprite;

            isOriginalSprite = true;
        }
    }
}
