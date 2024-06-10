using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    public int waves = 2;
    public float spawnTime = 3f;

    public float spawnStart = 1f;

    public int spawnAmount = 5;
    public float xMin = -0;
    public float xMax = 0;
    public float yMin = -0;
    public float yMax = 0;
    public float zMin = -0;
    public float zMax = 0;

    int spawns;

    // Level 1
    // Z: 14 - 96
    // X: -21 - 10
    // Y: 2

    void Start()
    {
        spawns = waves;
        StartCoroutine(SpawnWaves());
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 enemyPosition;
            enemyPosition.x = Random.Range(xMin, xMax);
            enemyPosition.y = Random.Range(yMin, yMax);
            enemyPosition.z = Random.Range(zMin, zMax);


            GameObject spawnedEnemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], enemyPosition, transform.rotation) as GameObject;

            Debug.Log("Spawned enemy at " + enemyPosition); 

            spawnedEnemy.transform.parent = gameObject.transform;
        }
        FindObjectOfType<LevelManager>().SetWaveText(waves);


    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(spawnStart);
        for (int i = 0; i < spawns; i++)
        {
            waves--;
            SpawnEnemies();
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
