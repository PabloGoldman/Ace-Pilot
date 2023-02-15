using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab; // The prefab to use for bullets
    public Transform bulletSpawn; // The transform representing the position to spawn bullets
    public float bulletSpeed = 10f; // The speed at which bullets will travel
    public int maxBullets = 10; // The maximum number of bullets the player can have
    public int currentBullets; // The current number of bullets the player has

    void Start()
    {
        currentBullets = maxBullets; // Set the initial number of bullets to the maximum
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentBullets > 0)
        {
            FireBullet(); // Fire a bullet if the spacebar is pressed and the player has bullets
            currentBullets--; // Decrease the number of available bullets by 1
        }
    }

    void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = bulletSpawn.forward * bulletSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BulletPowerUp"))
        {
            currentBullets += 5; // Increase the number of available bullets by 5 when a power-up is picked up
            if (currentBullets > maxBullets)
            {
                currentBullets = maxBullets; // Ensure that the number of available bullets doesn't exceed the maximum
            }
            Destroy(other.gameObject); // Destroy the power-up GameObject when picked up
        }
    }
}
