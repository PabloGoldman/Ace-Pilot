using UnityEngine;

public class AndroidRemoveButton : MonoBehaviour
{
#if UNITY_ANDROID
    void Start()
    {
        Destroy(gameObject);
    }
#endif
}
