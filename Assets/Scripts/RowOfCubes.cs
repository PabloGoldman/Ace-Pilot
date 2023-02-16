using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowOfCubes : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject, 5f);
        }
    }
}
