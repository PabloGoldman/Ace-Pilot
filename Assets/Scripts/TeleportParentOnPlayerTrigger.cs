using UnityEngine;

public class TeleportParentOnPlayerTrigger : MonoBehaviour
{
    // The position to teleport the parent object to
    Vector3 teleportPosition;

    public CarBehaviour[] carList;

    float offsetZ = 353f;  //61.285 multiplicado la cantidad de chunks

    private void OnTriggerEnter(Collider other)
    {
        // Check if the other object is the player
        if (other.CompareTag("Player"))
        {
            teleportPosition = new Vector3(0.144f, 0.188f, transform.position.z + offsetZ);
            // Teleport the parent object to the given position
            transform.parent.position = teleportPosition;

            foreach (CarBehaviour car in carList)
            {
                car.SetToInitialPos();
            }
        }
    }
}
