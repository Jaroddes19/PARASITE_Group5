using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    float attackDistance;
    int attackDamage;
    float attackSpeed;

    bool attacking = false;
    bool readyToAttack = true;
    void Start()
    {
        var attack = gameObject.GetComponentInParent<AttackAttributes>();
        attackDistance = attack.attackDistance;
        attackDamage = attack.attackDamage;
        attackSpeed = attack.attackSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();

        }
    }

    void Attack()
    {
        if (readyToAttack && !attacking)
        {
            readyToAttack = false;
            attacking = true;
            gameObject.GetComponentInChildren<Animator>().SetTrigger("playerAttack");

            Invoke("ResetAttack", attackSpeed);
            AttackRaycast();
        }
    }

    void ResetAttack()
    {
        readyToAttack = true;
        attacking = false;
    }

    void AttackRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackDistance))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<EnemyHit>().TakeDamage(attackDamage);

            }
        }

    }
}
