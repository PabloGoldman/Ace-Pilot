using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab; // The prefab to use for bullets
    public Transform bulletSpawn; // The transform representing the position to spawn bullets
    public float bulletSpeed = 10f; // The speed at which bullets will travel
    public float fireRate = 0.5f; // The time (in seconds) between each shot
    public int maxBullets = 10; // The maximum number of bullets the player can have
    public int currentBullets; // The current number of bullets the player has

    private float nextFireTime; // The next time that the player is allowed to fire

    public float speed;

    private Rigidbody rb; // Reference to the player's Rigidbody component

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentBullets = maxBullets; // Set the initial number of bullets to the maximum
        nextFireTime = Time.time; // Set the initial next fire time to the current time
    }

    void Update()
    {
        if (Time.time >= nextFireTime && currentBullets > 0)
        {
            FireBullet(); // Fire a bullet if enough time has passed and the player has bullets
            currentBullets--; // Decrease the number of available bullets by 1
            nextFireTime = Time.time + fireRate; // Set the next fire time to the current time plus the fire rate
        }
    }

    private void FixedUpdate()
    {
        MovePlayerForward(speed, rb);
    }

    void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        IgnoreBulletCollisions(bullet.GetComponent<Collider>());
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = bulletSpawn.forward * bulletSpeed;
    }

    public void MovePlayerForward(float speed, Rigidbody rb)
    {
        rb.MovePosition(transform.position + transform.forward * speed);
    }

    public void IgnoreBulletCollisions(Collider bulletCollider)
    {
        Collider playerCollider = GetComponent<Collider>();
        Physics.IgnoreCollision(playerCollider, bulletCollider, true);
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
