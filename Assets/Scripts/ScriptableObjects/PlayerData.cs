using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData/MyScriptableObject")]

public class PlayerData : MonoBehaviour
{
    public float bulletSpeed = 10f; // The speed at which bullets will travel
    public float fireRate = 0.5f; // The time (in seconds) between each shot

    public float speed;
    public float horizontalSpeed;
}
