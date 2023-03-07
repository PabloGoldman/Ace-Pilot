using UnityEngine;

public class AudioManager : MonoBehaviour
{
    bool isMuted;

    public void MuteAudio()
    {
        if (!isMuted)
        {
            AudioListener.volume = 0;
            isMuted = true;
        }
        else
        {
            AudioListener.volume = 1;
            isMuted = false;
        }
    }
}
