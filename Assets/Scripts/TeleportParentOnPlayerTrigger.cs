using UnityEngine;

public class TeleportParentOnPlayerTrigger : MonoBehaviour
{
    // The position to teleport the parent object to
    Vector3 teleportPosition;

    float offsetZ = 429f;  //Cantidad de building multiplicado por 78.2

    private void OnTriggerEnter(Collider other)
    {
        // Check if the other object is the player
        if (other.CompareTag("Player"))
        {
            teleportPosition = new Vector3(0, 0, transform.position.z + offsetZ);
            // Teleport the parent object to the given position
            transform.parent.position = teleportPosition;
        }
    }
}
