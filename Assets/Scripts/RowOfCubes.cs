using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowOfCubes : MonoBehaviour
{
    public int maxActiveObjects = 3; // maximum number of active child objects
    public float activationInterval = 1.0f; // time between activations
    public List<GameObject> childObjects; // list of child objects

    private List<GameObject> activeObjects; // list of currently active child objects

    private void Start()
    {
        activeObjects = new List<GameObject>();

        foreach (GameObject obj in childObjects)
        {
            obj.SetActive(false);
        }

        activeObjects.Clear();

        // randomly choose a number of child objects to activate
        int numActiveObjects = Random.Range(1, maxActiveObjects + 1);

        // randomly choose which child objects to activate
        List<GameObject> remainingObjects = new List<GameObject>(childObjects);

        for (int i = 0; i < numActiveObjects && remainingObjects.Count > 0; i++)
        {
            int index = Random.Range(0, remainingObjects.Count);
            GameObject obj = remainingObjects[index];
            obj.SetActive(true);

            //Spawnea el enemigo en una posicion relativa a la que esta
            obj.transform.position = new Vector3(obj.transform.position.x + Random.Range(-4, 4), transform.position.y, transform.position.z);

            activeObjects.Add(obj);
            remainingObjects.RemoveAt(index);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject, 15f);
        }
    }
}
