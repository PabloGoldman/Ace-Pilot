using UnityEngine;

public class WebRemoveButton : MonoBehaviour
{
#if UNITY_WEBGL
    void Start()
    {
        Destroy(gameObject);
    }
#endif
}
