<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        var attack = gameObject.GetComponentInParent<CharacterAttributes>();
        attackDistance = attack.attackOneRange;
        attackDamage = attack.attackOneDmg;
        attackSpeed = attack.attackOneSpeed;
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
            // do we meet the conditions to parisitize this enemy? (currently a stub method)
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
     * Preform the action of parisitizing the hit enemy. Some behavior here needs defined.
     * 
     * Adds player/camera control scripts, and removes enemy ai scripts from the hit 
     */
    void Parasitize(GameObject newPlayerObj)
    {
        AudioSource.PlayClipAtPoint(parasiteSFX, Camera.main.transform.position);
        // Debug.Log("Running parasite operations");
        // remove enemy scripts
        Destroy(newPlayerObj.GetComponent<EnemyBehavior>());
        Destroy(newPlayerObj.GetComponent<EnemyHit>());
        Destroy(newPlayerObj.GetComponent<Rigidbody>());
        // Need to remove enemy canvas

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
    bool canParisitize(GameObject hitObj)
    {
        Debug.Log(hitObj.tag);

        if (hitObj.CompareTag("Enemy"))
        {
            var hitAttrs = hitObj.GetComponent<CharacterAttributes>();
            Debug.Log((hitAttrs.currentHealth / (float)hitAttrs.maxHealth) <= hpParasiteThreshold);
            return (hitAttrs.currentHealth / (float)hitAttrs.maxHealth) <= hpParasiteThreshold;
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
=======
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
>>>>>>> e727db38aa2f6e6ab94f60a900d04b142a236eef
