using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public int cubesPerRow = 5; // number of cubes in each row
    public float rowSpacing = 2.0f; // space between each row
    public float cubeSpacing = 1.0f; // space between each cube

    public int rowsPerInstantiate = 2;

    public GameObject rowOfCubes;
    private List<List<GameObject>> cubes;

    public float rowSpawningTime = 2;

    private int currentRow = 0;

    void Start()
    {
        cubes = new List<List<GameObject>>();

        // start coroutine to spawn cubes
        StartCoroutine(SpawnCubes());
    }

    IEnumerator SpawnCubes()
    {
        while (true)
        {
            // create two rows of cubes
            for (int i = 0; i < rowsPerInstantiate; i++)
            {
                cubes.Add(new List<GameObject>());

                // instantiate row
                GameObject cubesRow = Instantiate(rowOfCubes, transform);
                cubesRow.transform.position = new Vector3(i * cubeSpacing, 0, currentRow * rowSpacing);

                // set random bullet requirement

                foreach (var cube in cubesRow.GetComponentsInChildren<Cube>())
                {
                    int bulletRequirement = Random.Range(2, 11);
                    cube.GetComponent<Cube>().SetBulletRequirement(bulletRequirement);
                }

                cubes[currentRow].Add(cubesRow);

                currentRow++;
            }

            // wait for some time before spawning more cubes
            yield return new WaitForSeconds(rowSpawningTime);
        }
    }
}
