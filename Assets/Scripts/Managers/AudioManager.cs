using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public void MuteAudio()
    {
        if (AudioListener.volume == 1)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }
    }
}
