using System.Collections;
using UnityEngine;

public class Zepellin : MonoBehaviour
{
    Rigidbody rb;

    float force = 500;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        StartCoroutine(AddForceCoroutine());
    }

    void AddRandomForce()
    {
        int random = Random.Range(1, 3);

        if (random == 1)
        {
            rb.AddForce(Vector3.right * force, ForceMode.Force);
        }
        else
        {
            rb.AddForce(Vector3.right * -force, ForceMode.Force);

        }
    }

    IEnumerator AddForceCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            AddRandomForce();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            ScoreManager.Instance.IncreaseScore(1);
        }
    }
}
