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
    bool inSuperPowerUp;

    public float speed;
    public float horizontalSpeed;

    private Rigidbody rb; // Reference to the player's Rigidbody component

    public GameObject bulletsTarget;

    public float rotationSpeed = 0.1f;
    public float rotationOffSet = 25f;

    public ParticleSystem deadParticles;
    public GameObject airplaneModel;

    private int collectedPowerUps = 0;

    private float timeBetweenAudioPitchChanger = 0.5f;
    private float changeAudioPitchTimer;

    public AudioSource[] shootSounds;
    public AudioSource superPowerUpShootSound;
    public AudioSource powerUpTriggerSound;
    public AudioSource superPowerUpTriggerSound;
    public AudioSource crashSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        nextFireTime = Time.time; // Set the initial next fire time to the current time
    }

    void Update()
    {
        AcceleratePlayerSpeedOverTime();
        SetFireBulletTimer();
    }

    private void FixedUpdate()
    {
        MovePlayerForward(speed);
        MovePlayerWithTouch();
    }

    void SetFireBulletTimer()
    {
        if (Time.time >= nextFireTime)
        {
            FireBullet(); // Fire a bullet if enough time has passed and the player has bullets
            nextFireTime = Time.time + fireRate; // Set the next fire time to the current time plus the fire rate
        }
    }

    void AcceleratePlayerSpeedOverTime()
    {
        speed += Time.deltaTime / 1500; 
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

    public void SetShootAudio(bool isSuperPowerUp)
    {
        if (isSuperPowerUp)
        {
            shootSounds[collectedPowerUps].Stop();
            superPowerUpShootSound.Play();
        }
        else
        {
            if (superPowerUpShootSound.isPlaying)
            {
                superPowerUpShootSound.Stop();
            }

            if (collectedPowerUps >= shootSounds.Length - 1)
            {
                return;
            }
            else
            {
                if (collectedPowerUps > 0)
                {
                    shootSounds[collectedPowerUps - 1].Stop();
                    shootSounds[collectedPowerUps].Play();
                }
                else
                {
                    //Aca solo entra la primera vez
                    shootSounds[0].Play();
                }
            }
        }
    }

    void StopShootSounds()
    {
        if (collectedPowerUps < shootSounds.Length - 1)
        {
            shootSounds[collectedPowerUps].Stop();
        }
        else
        {
            shootSounds[shootSounds.Length - 1].Stop();
        }
    }

    IEnumerator SetFireRateToSuperPowerUp(float normalFireRate, float newFireRate, float superPowerUpTime)
    {
        inSuperPowerUp = true;
        fireRate = newFireRate;
        yield return new WaitForSeconds(superPowerUpTime);
        inSuperPowerUp = false;
        SetShootAudio(false);
        SetFireRateToNormal(normalFireRate);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cube"))
        {
            StopShootSounds();
            crashSound.Play();
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
            powerUpTriggerSound.Play();
            Destroy(other.gameObject); // Destroy the power-up GameObject when picked up
            fireRate *= 0.7f;
            SetShootAudio(false);

            if (collectedPowerUps < shootSounds.Length - 1)
            {
                collectedPowerUps++;
            }
        }

        if (other.CompareTag("SuperPowerUp"))
        {
            if (!inSuperPowerUp)
            {
                superPowerUpTriggerSound.Play();
                SetShootAudio(true);
                Destroy(other.gameObject); // Destroy the power-up GameObject when picked up
                StartCoroutine(SetFireRateToSuperPowerUp(fireRate, 0f, superPowerUpTime));
            }
        }
    }
}
