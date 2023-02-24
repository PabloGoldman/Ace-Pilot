using UnityEngine;

public class ObstaclesMovement : MonoBehaviour
{
    float speed = 10f;  // The speed at which the object moves forward

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
