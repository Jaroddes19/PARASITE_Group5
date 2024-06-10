using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{

    public enum FSMStates
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Dead,
    }

    public FSMStates currentState;

    public float chaseDistance = 15.0f;

    public AudioClip attackSFX;

    public AudioClip deathSFX;

    public GameObject player;
    public GameObject projectilePrefab;

    public GameObject deadVFX;


    public GameObject stinger;


    GameObject[] wanderPoints;
    Vector3 nextDestination;
    Animator anim;

    float distanceToPlayer;
    float elapsedTime = 0.0f;

    CharacterAttributes charAttrs;


    int health;

    Transform deadTransform;
    bool isDead = false;



    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.CompareTag("Stinger"))
            {
                stinger = child.gameObject;
            }
        }
        charAttrs = gameObject.GetComponent<CharacterAttributes>();
        projectilePrefab.GetComponent<EnemyProjectileBehavior>().attackDamage = charAttrs.attackTwoDmg;
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (deadVFX == null)
        {
            deadVFX = Resources.Load("BloodSplatter") as GameObject;

        }


        health = charAttrs.currentHealth;
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.isGameOver)
        {
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        }
        else
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            health = charAttrs.currentHealth;
            switch (currentState)
            {
                case FSMStates.Patrol:
                    UpdatePatrolState();
                    break;
                case FSMStates.Chase:
                    UpdateChaseState();
                    break;
                case FSMStates.Attack:
                    UpdateAttackState();
                    break;
                case FSMStates.Dead:
                    UpdateDeadState();
                    break;
            }
            elapsedTime += Time.deltaTime;

            if (health <= 0)
            {
                currentState = FSMStates.Dead;
            }

        }
    }

    void Initialize()
    {
        currentState = FSMStates.Patrol;

        findNextPoint();
    }

    void UpdatePatrolState()
    {
        // Debug.Log("Patroling");
        // anim.SetInteger("animState", 1);
        // Debug.Log(Vector3.Distance(transform.position, new Vector3(nextDestination.x, 0, nextDestination.z)));
        if (Vector3.Distance(transform.position, new Vector3(nextDestination.x, transform.position.y, nextDestination.z)) < 1f)
        {
            findNextPoint();
        }
        else if (distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.Chase;
        }

        // FaceTarget(nextDestination);

        // transform.position = Vector3.MoveTowards(transform.position, nextDestination, charAttrs.speed * Time.deltaTime);



    }

    void UpdateChaseState()
    {
        // Debug.Log("Chasing");
        // anim.SetInteger("animState", 2);

        nextDestination = player.transform.position;

        if (distanceToPlayer <= charAttrs.attackTwoRange)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            currentState = FSMStates.Patrol;
        }

        // FaceTarget(nextDestination);

        // transform.position = Vector3.MoveTowards(transform.position, nextDestination, charAttrs.speed * Time.deltaTime);
    }

    void UpdateAttackState()
    {
        // Debug.Log("Attacking");

        nextDestination = transform.position;

        if (distanceToPlayer <= charAttrs.attackTwoRange)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > charAttrs.attackTwoRange && distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.Chase;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            currentState = FSMStates.Patrol;
        }

        transform.LookAt(player.transform.position);

        // anim.SetInteger("animState", 3);
        EnemyRangedAttack();

    }

    void UpdateDeadState()
    {
        // anim.SetInteger("animState", 4);

        isDead = true;
        deadTransform = transform;

        Destroy(gameObject, 3.0f);
    }

    void findNextPoint()
    {
        nextDestination = wanderPoints[UnityEngine.Random.Range(0, wanderPoints.Length)].transform.position;
        Debug.Log("Next Point is at:" + nextDestination);

    }

    void FaceTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }

    void EnemyRangedAttack()
    {
        if (!isDead)
        {
            if (elapsedTime >= charAttrs.attackTwoSpeed)
            {
                // var animDuration = anim.GetCurrentAnimatorStateInfo(0).length;
                RangedAttack();
                elapsedTime = 0.0f;

            }
        }
    }

    void RangedAttack()
    {
        Debug.Log("Ranged Attack");

        if (attackSFX != null)
        {
            AudioSource.PlayClipAtPoint(attackSFX, transform.position);
        }

        var projectile = Instantiate(projectilePrefab, stinger.transform.position, stinger.transform.rotation) as GameObject;

        projectile.GetComponent<ProjectileAttributes>().senderID = 1;

        projectile.transform.SetParent(GameObject.FindGameObjectWithTag("ProjectileParent").transform);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 10);
    }

    private void OnDestroy()
    {
        if (deathSFX != null)
        {
            AudioSource.PlayClipAtPoint(deathSFX, transform.position);
        }

        Instantiate(deadVFX, deadTransform.position, deadTransform.rotation);
    }

    public Vector3 GetNextDestination()
    {
        return nextDestination;
    }
}
