<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    public GameObject lootPrefab;
    public float spawnTime = 3f;

    public float spawnStart = 1f;

    public int spawnAmount = 5;
    public float xMin = -25f;
    public float xMax = 25f;

    public float yMin = -0;
    public float yMax = 0;
    public float zMin = -25f;
    public float zMax = 25f;


    void Start()
    {
        InvokeRepeating("SpawnLoot", spawnStart, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnLoot()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 lootPosition;
            lootPosition.x = Random.Range(xMin, xMax);
            lootPosition.y = Random.Range(yMin, yMax);
            lootPosition.z = Random.Range(zMin, zMax);


            GameObject spawnedEnemy = Instantiate(lootPrefab, lootPosition, transform.rotation) as GameObject;

            spawnedEnemy.transform.parent = gameObject.transform;
        }

    }
}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    public GameObject lootPrefab;
    public float spawnTime = 3f;

    public float spawnStart = 1f;

    public int spawnAmount = 5;
    public float xMin = -25f;
    public float xMax = 25f;
    public float zMin = -25f;
    public float zMax = 25f;


    void Start()
    {
        InvokeRepeating("SpawnLoot", spawnStart, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnLoot()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 lootPosition;
            lootPosition.x = Random.Range(xMin, xMax);
            lootPosition.y = 1.3f;
            lootPosition.z = Random.Range(zMin, zMax);


            GameObject spawnedEnemy = Instantiate(lootPrefab, lootPosition, transform.rotation) as GameObject;

            spawnedEnemy.transform.parent = gameObject.transform;
        }

    }
}
>>>>>>> e727db38aa2f6e6ab94f60a900d04b142a236eef
