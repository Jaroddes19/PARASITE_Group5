using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;
    float moveSpeed;

    // public float minDistance = 0f;

    public int damageAmount;

    private static int enemyCount = 0;

    CharacterAttributes charAttrs;

    float atkCooldown;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        charAttrs = gameObject.GetComponentInParent<CharacterAttributes>();
        damageAmount = charAttrs.attackOneDmg;
        moveSpeed = charAttrs.speed;
    }

    // Update is called once per frame
    void Update()
    {

        atkCooldown -= Time.deltaTime;

        FindObjectOfType<LevelManager>().SetEnemiesText(enemyCount);
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        float step = moveSpeed * Time.deltaTime;

        transform.LookAt(player);
        transform.position = Vector3.MoveTowards(transform.position, player.position, step);

        if (Vector3.Distance(transform.position, player.position) < 5 && atkCooldown < 0)
        {
            atkCooldown = charAttrs.abilityCooldown;
            charAttrs.AttackOne();
        }
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

    //Stops enemy from skating away in a direction after a heavy collision or bounce
    void Inertia()
    {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    void OnDisable()
    {
        enemyCount--;
        if (enemyCount == 0)
        {
            FindObjectOfType<LevelManager>().LevelBeat();
        }
    }
}
