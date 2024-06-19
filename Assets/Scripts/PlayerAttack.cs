using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class PlayerAttack : MonoBehaviour
{
    // the max %hp an enemy can have and be parasitized
    public float hpParasiteThreshold = 0.5f;

    public AudioClip attackSFX;

    public AudioClip parasiteSFX;
    float attackDistance;
    int attackDamage;
    float attackSpeed;

    bool attacking = false;
    bool readyToAttack = true;

    float timeToNextAttack; //for UI only
    public Slider attackCooldownBar;

    void Start()
    {
        var attack = gameObject.GetComponentInParent<CharacterAttributes>();
        attackDistance = attack.attackOneRange;
        attackDamage = attack.attackOneDmg;
        attackSpeed = attack.attackOneSpeed;

        // attackCooldownBar = GameObject.FindGameObjectWithTag("PunchCooldown").GetComponent<Slider>();
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
            ParisitizeAttack();
        }

        //update attack cooldown bar, show if needed
        // if (timeToNextAttack <= 0)
        // {
        //     attackCooldownBar.gameObject.SetActive(false);
        // }
        // else
        // {
        //     attackCooldownBar.gameObject.SetActive(true);
        // }
        // timeToNextAttack -= Time.deltaTime;
        // attackCooldownBar.value = Mathf.Clamp(timeToNextAttack, 0, attackSpeed);
    }

    void Attack()
    {
        if (readyToAttack && !attacking)
        {
            readyToAttack = false;
            attacking = true;
            gameObject.GetComponentInChildren<Animator>().SetTrigger("playerAttack");
            AudioSource.PlayClipAtPoint(attackSFX, Camera.main.transform.position);

            Invoke("ResetAttack", attackSpeed);
            timeToNextAttack = attackSpeed;
            AttackRaycast();
        }
    }

    /*
     * Attempt to preform the parasite attack.
     */
    void ParisitizeAttack()
    {
        // Debug.Log("Start Parasitizing now");

        RaycastHit parasiteAtk;
        // if the attack hits
        if (Physics.Raycast(transform.position, transform.forward, out parasiteAtk, attackDistance))
        {
            // get the top level gameobject (in case an arm, etc is collided with)
            var root = parasiteAtk.transform.gameObject;
            if (canParisitize(root))
            {
                Parasitize(root);
            }
            else
            {
                //TODO
            }
        }
    }

    /* 
     * Preform the action of parasitizing the hit enemy.
     * 
     * Adds player/camera control scripts, and removes enemy ai scripts from the hit 
     */
    void Parasitize(GameObject newPlayerObj)
    {
        //reset FOV in case of speed FX
        Camera.main.fieldOfView = GetComponentInParent<PlayerController>().oldFOV;

        AudioSource.PlayClipAtPoint(parasiteSFX, Camera.main.transform.position);
        // remove enemy scripts
        newPlayerObj.GetComponent<EnemyHit>().Parasitized();
        Destroy(newPlayerObj.GetComponent<EnemyBehavior>());
        Destroy(newPlayerObj.GetComponent<EnemyHit>());
        Destroy(newPlayerObj.GetComponent<Rigidbody>());
        Destroy(newPlayerObj.GetComponent<EnemyAI>());
        Destroy(newPlayerObj.GetComponent<NavMeshAgent>());
        Destroy(newPlayerObj.GetComponent<EnemyNav>());


        // add player scripts
        newPlayerObj.AddComponent<PlayerController>();
        newPlayerObj.AddComponent<PlayerHealth>();
        newPlayerObj.AddComponent<CharacterController>();
        newPlayerObj.GetComponent<MeshRenderer>().enabled = false;
        MeshRenderer[] mr = newPlayerObj.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer m in mr)
        {
            m.enabled = false;
        }
        newPlayerObj.GetComponentInChildren<Canvas>().enabled = false;

        var oldPlayer = gameObject.transform.parent;
        // shift camera to new player controlled gameobj
        gameObject.transform.SetParent(newPlayerObj.transform);
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
        // fixing an issue where the camera was broken after takeover, plus a smoother transition
        newPlayerObj.transform.rotation = oldPlayer.transform.rotation;

        newPlayerObj.tag = "Player";


        // if (Need to find a way to distinguish parasite and old enemy that the player is controlling)
        // {
        Destroy(oldPlayer.gameObject);

        // }
        // else
        // {
        //     oldPlayer.gameObject.tag = "Enemy";
        //     Destroy(oldPlayer.gameObject.GetComponent<PlayerController>());
        //     Destroy(oldPlayer.gameObject.GetComponent<PlayerHealth>());
        //     Destroy(oldPlayer.gameObject.GetComponent<CharacterController>());
        //     oldPlayer.gameObject.AddComponent<EnemyBehavior>();
        //     oldPlayer.gameObject.AddComponent<EnemyHit>();
        //     oldPlayer.gameObject.AddComponent<Rigidbody>();
        //     Need to add back enemy canvas
        // }



        /*
        gameObject.tag = "Enemy"; // in case the player ever has the option to leave a victim alive

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
    bool canParisitize(GameObject hitObj)
    {
        Debug.Log(hitObj.tag);

        if (hitObj.CompareTag("Enemy"))
        {
            var hitAttrs = hitObj.GetComponent<CharacterAttributes>();
            Debug.Log((hitAttrs.currentHealth / (float)hitAttrs.maxHealth) <= hpParasiteThreshold);
            //return (hitAttrs.currentHealth / (float)hitAttrs.maxHealth) <= hpParasiteThreshold;
            return true;
        }
        return false;
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