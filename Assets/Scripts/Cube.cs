using UnityEngine;

public class Cube : MonoBehaviour
{
    public int bulletsNeeded = 3; // The number of bullets needed to destroy the cube

    private TextMesh numberTextMesh; // The TextMesh component used to display the number

    void Start()
    {
        // Get the TextMesh component from the cube's child object
        numberTextMesh = GetComponentInChildren<TextMesh>();
        // Set the text to display the number of bullets needed
        numberTextMesh.text = bulletsNeeded.ToString();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
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
