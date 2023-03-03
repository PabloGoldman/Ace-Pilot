using UnityEngine;

public class AndroidBackButton : MonoBehaviour
{
#if UNITY_ANDROID
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
#endif
}
