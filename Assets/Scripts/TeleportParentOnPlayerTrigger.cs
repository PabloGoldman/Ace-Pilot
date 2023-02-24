using UnityEngine;

public class TeleportParentOnPlayerTrigger : MonoBehaviour
{
    // The position to teleport the parent object to
    Vector3 teleportPosition;

    public GameObject[] car;

    public Transform[] carInitialPos;

    float offsetZ = 429f;

    private void Start()
    {
        for (int i = 0; i < car.Length; i++)
        {
            carInitialPos[i] = car[i].transform;
        }
    }

    void SetNewCarPos()
    {
        for (int i = 0; i < car.Length; i++)
        {
            car[i].transform.position = new Vector3(carInitialPos[i].position.x, carInitialPos[i].position.y, carInitialPos[i].position.z - 50f);
            car[i].transform.rotation = carInitialPos[i].rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the other object is the player
        if (other.CompareTag("Player"))
        {
            teleportPosition = new Vector3(0, 0, transform.position.z + offsetZ);
            // Teleport the parent object to the given position
            transform.parent.position = teleportPosition;
            SetNewCarPos();
        }
    }
}
