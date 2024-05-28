using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnTime = 3f;

    public float spawnStart = 1f;

    public int spawnAmount = 5;
    public float xMin = -25f;
    public float xMax = 25f;
    public float zMin = -25f;
    public float zMax = 25f;


    void Start()
    {
        InvokeRepeating("SpawnEnemies", spawnStart, spawnTime);
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
            enemyPosition.y = 1.3f;
            enemyPosition.z = Random.Range(zMin, zMax);


            GameObject spawnedEnemy = Instantiate(enemyPrefab, enemyPosition, transform.rotation) as GameObject;

            spawnedEnemy.transform.parent = gameObject.transform;
        }

    }
}
