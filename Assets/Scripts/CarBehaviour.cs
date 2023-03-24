using UnityEngine;

public class CarBehaviour : MonoBehaviour
{
    float speed;  // The speed at which the object moves forward

    Vector3 initialPos;

    private void Start()
    {
        speed = Random.Range(1, 10);

        initialPos = transform.localPosition;
    }

    public void SetToInitialPos()
    {
        transform.localPosition = initialPos;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
