using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public int cubesPerRow = 5; // number of cubes in each row
    public float rowSpacing = 2.0f; // space between each row

    public int rowsPerInstantiate = 2;
    public float rowSpawningTime = 2;

    public GameObject rowOfCubes;
    public List<GameObject> powerUp;
    private List<List<GameObject>> cubes;

    PlayerController player;

    private int currentRow = 0;

    public int rowsPerPowerUp = 5;
    private int currentRowForPowerUp = 0;

    void Start()
    {
        cubes = new List<List<GameObject>>();
        player = FindObjectOfType<PlayerController>();
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

                Vector3 newCubesRowPos;

                if (player)
                {
                    newCubesRowPos = new Vector3(5f, player.transform.position.y, currentRow * rowSpacing);
                }
                else
                {
                    newCubesRowPos = new Vector3(0.4f, 0, currentRow * rowSpacing);
                }

                cubesRow.transform.position = newCubesRowPos;

                //SPAWNEO DE POWER UP
                if (currentRowForPowerUp == rowsPerPowerUp)
                {
                    int randomNumber = Random.Range(1, 100);

                    float randomPosX = Random.Range(-4f, 48f);

                    Vector3 spawnPosition = new Vector3(randomPosX, newCubesRowPos.y, cubesRow.transform.position.z + rowSpacing / 2);

                    if (randomNumber > 10)
                    {
                        Instantiate(powerUp[0], spawnPosition, transform.rotation);
                    }
                    else
                    {
                        Instantiate(powerUp[1], spawnPosition, transform.rotation);
                    }

                    currentRowForPowerUp = 0;
                }

                cubes[currentRow].Add(cubesRow);

                currentRow++;
                currentRowForPowerUp++;
            }

            // wait for some time before spawning more cubes
            yield return new WaitForSeconds(rowSpawningTime);
        }
    }
}
