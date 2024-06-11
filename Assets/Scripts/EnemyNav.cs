using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class EnemyNav : MonoBehaviour
{
    public Transform player;

    NavMeshAgent navMeshAgent;

    EnemyAI enemyAI;

    CharacterAttributes charAttrs;

    void Start()
    {
        charAttrs = gameObject.GetComponentInParent<CharacterAttributes>();
        if (gameObject.GetComponent<EnemyAI>() != null)
        {
            enemyAI = gameObject.GetComponent<EnemyAI>(); 
        }
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.speed = charAttrs.speed;

        // Animator anim = GetComponent<Animator>();

        // anim.SetInteger("animState", 1);

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyAI != null)
        {
            navMeshAgent.SetDestination(enemyAI.GetNextDestination());
        }
        else
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
            navMeshAgent.SetDestination(player.position);

        }
    }
}
