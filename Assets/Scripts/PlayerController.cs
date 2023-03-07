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

    public float superPowerUpTime = 4f;

    public float speed;
    public float horizontalSpeed;

    private Rigidbody rb; // Reference to the player's Rigidbody component

    public GameObject bulletsTarget;

    public float rotationSpeed = 0.1f;
    public float rotationOffSet = 25f;

    public ParticleSystem deadParticles;
    public GameObject airplaneModel;

    private int collectedPowerUps = 0;

    public AudioSource[] shootSounds;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        nextFireTime = Time.time; // Set the initial next fire time to the current time

        SetShootAudio();
    }

    void Update()
    {
        speed += Time.deltaTime / 1500; //Este numero cuanto mas chico es mas acelera xd

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
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.position.x < Screen.width / 2)
                {
                    // Touch is on the left side of the screen, move player left
                    rb.AddForce(transform.right * horizontalSpeed);

                    Quaternion targetRotation = Quaternion.Euler(transform.rotation.x, 180, -rotationOffSet);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                }
                else
                {
                    // Touch is on the right side of the screen, move player right
                    rb.AddForce(-transform.right * horizontalSpeed);

                    Quaternion targetRotation = Quaternion.Euler(transform.rotation.x, 180, rotationOffSet);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                }
            }
            else
            {
                //rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
                Quaternion targetRotation = Quaternion.Euler(transform.rotation.x, 180, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
        else
        {
            //PARA PC
            if (Input.GetMouseButton(0))
            {
                if (Input.mousePosition.x < Screen.width / 2)
                {
                    // Mouse click is on the left side of the screen, move player left
                    rb.AddForce(transform.right * horizontalSpeed);

                    Quaternion targetRotation = Quaternion.Euler(transform.rotation.x, 180, -rotationOffSet);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                }
                else
                {
                    // Mouse click is on the right side of the screen, move player right
                    rb.AddForce(-transform.right * horizontalSpeed);

                    Quaternion targetRotation = Quaternion.Euler(transform.rotation.x, 180, rotationOffSet);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                }
            }
            else
            {
                //rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
                Quaternion targetRotation = Quaternion.Euler(transform.rotation.x, 180, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }

    }

    void FireBullet()
    {
        //Sonido del disparo

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

    void SetFireRateToNormal(float normalFireRate)
    {
        //Aca volves a la normalidad del super power up
        fireRate = normalFireRate;
    }

    public void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }

    void TriggerEndGame()
    {
        Destroy(gameObject);
        ScoreManager.Instance.DisplayFinalScore();
    }

    void SetShootAudio()
    {
        if (collectedPowerUps > 4)
        {
            return;
        }

        else if (collectedPowerUps > 0)
        {
            shootSounds[collectedPowerUps - 1].Stop();
            shootSounds[collectedPowerUps].Play();

        }
        else
        {
            shootSounds[collectedPowerUps].Play();
        }
    }

    IEnumerator SetFireRateToSuperPowerUp(float normalFireRate, float newFireRate, float superPowerUpTime)
    {
        fireRate = newFireRate;
        yield return new WaitForSeconds(superPowerUpTime);
        SetFireRateToNormal(normalFireRate);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cube"))
        {
            //Aca sonido de muerte
            deadParticles.Play();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            airplaneModel.SetActive(false);
            Invoke(nameof(TriggerEndGame), 1.2f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            //Sonido power up normal
            Destroy(other.gameObject); // Destroy the power-up GameObject when picked up
            fireRate *= 0.7f;
            collectedPowerUps++;
            SetShootAudio();
        }

        if (other.CompareTag("SuperPowerUp"))
        {
            //Sonido super power up
            Destroy(other.gameObject); // Destroy the power-up GameObject when picked up
            StartCoroutine(SetFireRateToSuperPowerUp(fireRate, 0f, superPowerUpTime));
        }
    }
}
