using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab; // The prefab to use for bullets
    public Transform bulletSpawn; // The transform representing the position to spawn bullets
    public float bulletSpeed = 10f; // The speed at which bullets will travel
    public float fireRate = 0.5f; // The time (in seconds) between each shot

    private float nextFireTime; // The next time that the player is allowed to fire

    public float speed;
    public float horizontalSpeed;

    private Rigidbody rb; // Reference to the player's Rigidbody component

    public GameObject bulletsTarget;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        nextFireTime = Time.time; // Set the initial next fire time to the current time
    }

    void Update()
    {
        speed += Time.deltaTime / 1800; //Este numero cuanto mas chico es mas acelera xd

        if (Time.time >= nextFireTime)
        {
            FireBullet(); // Fire a bullet if enough time has passed and the player has bullets
            nextFireTime = Time.time + fireRate; // Set the next fire time to the current time plus the fire rate
        }
    }

    private void FixedUpdate()
    {
        MovePlayerForward(speed);
        MovePlayerWithTouch();
    }

    void MovePlayerWithTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x < Screen.width / 2)
            {
                // Touch is on the left side of the screen, move player left
                rb.AddForce(transform.right * horizontalSpeed);
            }
            else
            {
                // Touch is on the right side of the screen, move player right
                rb.AddForce(-transform.right * horizontalSpeed);
            }
        }
    }

    void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        bullet.transform.LookAt(bulletsTarget.transform.position);

        IgnoreBulletCollisions(bullet.GetComponent<Collider>());
    }

    public void MovePlayerForward(float speed)
    {
        rb.MovePosition(transform.position + transform.forward * -speed);
    }

    public void IgnoreBulletCollisions(Collider bulletCollider)
    {
        Collider playerCollider = GetComponent<Collider>();
        Physics.IgnoreCollision(playerCollider, bulletCollider, true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject); // Destroy the power-up GameObject when picked up
            fireRate *= 0.7f;
        }

        if (other.CompareTag("SuperPowerUp"))
        {
            Destroy(other.gameObject); // Destroy the power-up GameObject when picked up
            StartCoroutine(SetFireRateToSuperPowerUp(fireRate, 0f, 4));
        }
    }

    void SetFireRateToNormal(float normalFireRate)
    {
        fireRate = normalFireRate;
    }

    IEnumerator SetFireRateToSuperPowerUp(float normalFireRate, float newFireRate, float superPowerUpTime)
    {
        fireRate = newFireRate;
        yield return new WaitForSeconds(superPowerUpTime);
        SetFireRateToNormal(normalFireRate);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cube"))
        {
            Destroy(gameObject); // Destroy the bullet when it hits a cube
            RestartGame();
        }
    }
}
