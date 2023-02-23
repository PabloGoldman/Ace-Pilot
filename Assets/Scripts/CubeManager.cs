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

    private int minimunBulletRequirement = 2;
    private int maximumBulletRequirement = 10;


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
                cubesRow.transform.position = new Vector3(0, player.transform.position.y, currentRow * rowSpacing);

                //SPAWNEO DE POWER UP
                if (currentRowForPowerUp == rowsPerPowerUp)
                {
                    int randomNumber = Random.Range(1, 100);

                    float randomPosX = Random.Range(-4f, 3.5f);

                    Vector3 spawnPosition = new Vector3(randomPosX, player.transform.position.y, cubesRow.transform.position.z + rowSpacing / 2);

                    if (randomNumber > 10)
                    {
                        Instantiate(powerUp[0], spawnPosition, transform.rotation);
                    }
                    else
                    {
                        Instantiate(powerUp[1], spawnPosition, transform.rotation);
                    }

                    currentRowForPowerUp = 0;

                    minimunBulletRequirement += 2;
                    maximumBulletRequirement += 4;
                }

                // set random bullet requirement

                foreach (var cube in cubesRow.GetComponentsInChildren<Cube>())
                {
                    int bulletRequirement = Random.Range(minimunBulletRequirement, maximumBulletRequirement);
                    cube.GetComponent<Cube>().SetBulletRequirement(bulletRequirement);
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
