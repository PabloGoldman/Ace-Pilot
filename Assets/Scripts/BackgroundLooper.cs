using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    public GameObject[] backgroundObjects; // an array of all the 3D background objects to be looped
    public float scrollSpeed = 1.0f; // the speed at which the background objects will scroll

    void Update()
    {
        foreach (GameObject obj in backgroundObjects)
        {
            // move the object to the left by the scrollSpeed multiplied by Time.deltaTime
            obj.transform.Translate(Vector3.forward * scrollSpeed * Time.deltaTime);

            // check if the object has moved offscreen to the left
            if (obj.transform.position.z < -20.0f)
            {
                // move the object to the right of the screen
                obj.transform.position = new Vector3(20.0f, obj.transform.position.y, obj.transform.position.z);
            }
        }
    }
}
