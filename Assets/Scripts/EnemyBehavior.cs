using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;
    float moveSpeed;

    // public float minDistance = 0f;

    public int damageAmount;

    private static int enemyCount = 0;

    void Start()
    {
        enemyCount++;
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        var charAttrs = gameObject.GetComponentInParent<CharacterAttributes>();
        damageAmount = charAttrs.attackOneDmg;
        moveSpeed = charAttrs.speed;
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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageAmount);
            other.gameObject.GetComponent<CharacterController>().Move(-other.transform.forward * 2);
        }
        Invoke("Inertia", 0.5f);
    }

    //Stops enemy from skating away in a direction after a heavy collision or bounce
    void Inertia()
    {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
