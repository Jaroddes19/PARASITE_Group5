using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;

    // public float minDistance = 0f;

    public int damageAmount;


    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        var charAttrs = gameObject.GetComponent<CharacterAttributes>();
        damageAmount = charAttrs.attackOneDmg;
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.isGameOver)
        {
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        }
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        // float step = moveSpeed * Time.deltaTime;

        // transform.LookAt(player);
        // transform.position = Vector3.MoveTowards(transform.position, player.position, step);
    }

    // private void OnEnable()
    // {

    // }

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

    //Stops enemy from skating away in a direction after a heavy collision or bounce
    void Inertia()
    {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

}
