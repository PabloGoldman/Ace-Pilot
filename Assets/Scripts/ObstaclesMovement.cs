using UnityEngine;

public class ObstaclesMovement : MonoBehaviour
{
    float speed;  // The speed at which the object moves forward

    private void Start()
    {
        speed = Random.Range(1, 10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
