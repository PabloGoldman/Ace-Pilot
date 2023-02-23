using UnityEngine;

public class Zepellin : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            ScoreManager.Instance.IncreaseScore(1);
        }
    }
}
