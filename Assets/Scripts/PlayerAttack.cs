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
        else if (Input.GetButtonDown("Q"))
        {
            Debug.Log("Attempting to parisitze");
            ParisitizeAttack();
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

    /*
     * Attempt to preform the parasite attack.
     */
    void ParisitizeAttack()
    {
        Debug.Log("Start Parasitizing now");

        RaycastHit parasiteAtk;
        // if the attack hits
        if (Physics.Raycast(transform.position, transform.forward, out parasiteAtk, attackDistance))
        {
            // do we meet the conditions to parisitize this enemy? (currently a stub method)
            if (canParisitize(parasiteAtk))
            {
                Parasitize(parasiteAtk.transform.gameObject);
            } else
            {
                //TODO
            }
        }
    }

    /* 
     * Preform the action of parisitizing the hit enemy. Some behavior here needs defined.
     * 
     * Adds player/camera control scripts, and removes enemy ai scripts from the hit 
     */
    void Parasitize(GameObject newPlayerObj)
    {
        Debug.Log("Running parasite operations");
        // remove enemy scripts
        Destroy(newPlayerObj.GetComponent<EnemyBehavior>());
        Destroy(newPlayerObj.GetComponent<EnemyHit>());
        Destroy(newPlayerObj.GetComponent<Rigidbody>());

        // add player scripts
        newPlayerObj.AddComponent<PlayerController>();
        newPlayerObj.AddComponent<PlayerHealth>();
        newPlayerObj.AddComponent<CharacterController>();

        // shift camera to new player controlled gameobj
        gameObject.transform.SetParent(newPlayerObj.transform);
        // add scripts to camera
        /*
        GameObject camera = newPlayerObj.transform.Find("MainCamera").gameObject;
        Debug.Log(camera);
        camera.AddComponent<PlayerAttack>();
        camera.AddComponent<MouseLook>();
        */

        // update respective tags
        newPlayerObj.tag = "Player";


        /*
        gameObject.tag = "Enemy"; // in case the player ever has the option to leave a vicitm alive

        // make old victim back to an enemy
        gameObject.AddComponent<EnemyBehavior>();
        gameObject.AddComponent<EnemyHit>();
        */
    }


    /*
     * Check to see if we meet the conditions to jump to this enemy
     * 
     * returns true if we can parasitize, false otherwise
     */
    bool canParisitize(RaycastHit hitObj)
    {
        return true;
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
