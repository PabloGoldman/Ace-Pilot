using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float lifespan = 3f; // The amount of time (in seconds) the bullet will live
    public int damage = 1; // The amount of damage the bullet deals to a cube

    void Start()
    {
        // Destroy the bullet after the specified lifespan has passed
        Destroy(gameObject, lifespan);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cube"))
        {
            Destroy(gameObject); // Destroy the bullet when it hits a cube
        }
    }
}