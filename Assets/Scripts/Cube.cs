using UnityEngine;
using TMPro;

public class Cube : MonoBehaviour
{
    private int bulletsNeeded = 3; // The number of bullets needed to destroy the cube

    private TextMeshProUGUI numberTextMesh; // The TextMesh component used to display the number

    void Start()
    {
        // Get the TextMesh component from the cube's child object
        numberTextMesh = GetComponentInChildren<TextMeshProUGUI>();
        // Set the text to display the number of bullets needed
        numberTextMesh.text = bulletsNeeded.ToString();
    }

    public void SetBulletRequirement(int setBulletNumber)
    {
        bulletsNeeded = setBulletNumber;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            bulletsNeeded--; // Decrease the number of bullets needed by 1
            numberTextMesh.text = bulletsNeeded.ToString(); // Update the displayed number
            if (bulletsNeeded <= 0)
            {
                Destroy(gameObject); // Destroy the cube when the required number of bullets have been hit
            }
        }
    }
}
