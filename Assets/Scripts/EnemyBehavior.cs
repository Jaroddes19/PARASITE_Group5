using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 10f;

    // public float minDistance = 0f;

    public int damageAmount = 20;

    private static int enemyCount = 0;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        float step = moveSpeed * Time.deltaTime;

        transform.LookAt(player);
        transform.position = Vector3.MoveTowards(transform.position, player.position, step);
    }

    private void OnEnable()
    {
        enemyCount++;
        FindObjectOfType<LevelManager>().SetEnemiesText(enemyCount);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageAmount);
            if (!LevelManager.isGameOver)
            {
                other.gameObject.GetComponent<CharacterController>().Move(-other.transform.forward * 2);
            }
        }
        Invoke("Inertia", 0.5f);
    }

    //Stops enemy from skating away in a direction after a heavy collsion or bounce
    void Inertia()
    {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    void OnDisable()
    {
        enemyCount--;
        FindObjectOfType<LevelManager>().SetEnemiesText(enemyCount);
        if (enemyCount == 0)
        {
            FindObjectOfType<LevelManager>().LevelBeat();
        }
    }
}
