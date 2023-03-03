using UnityEngine;

public class UrlOpener : MonoBehaviour
{
#if UNITY_ANDROID
    private const string moreGamesUrl = "market://search?q=pub:Image Campus";
#else
    private const string moreGamesUrl = "https://imagecampus.itch.io/";
#endif

    public void OpenMoreGamesUrl()
    {
        Application.OpenURL(moreGamesUrl);
    }
}
