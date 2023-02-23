using UnityEngine;

public class FloorMovement : MonoBehaviour
{
    PlayerController player;

    public float offsetZ;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z + offsetZ);
        }
    }
}
