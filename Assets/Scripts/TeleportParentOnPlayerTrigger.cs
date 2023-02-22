using UnityEngine;

public class TeleportParentOnPlayerTrigger : MonoBehaviour
{
    // The position to teleport the parent object to
    Vector3 teleportPosition;

    float offsetZ = 372;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the other object is the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("asd");
            teleportPosition = new Vector3(0, 0, transform.position.z + offsetZ);
            // Teleport the parent object to the given position
            transform.parent.position = teleportPosition;
        }
    }
}
