using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;

    float projectileSpeed;
    float projectileDelay;

    bool readyToFire = true;

    void Start()
    {
        projectileSpeed = projectilePrefab.GetComponent<ProjectileAttributes>().projectileSpeed;
        projectileDelay = projectilePrefab.GetComponent<ProjectileAttributes>().projectileDelay;
    }

    // Update is called once per frame
    void Update()
    {
        // Ensure enemy doesnt spam projectiles at player
        if (readyToFire)
        {
            Fire();
        }
    }

    void Fire()
    {
        if (readyToFire)
        {
            Debug.Log("Enemy is firing");
            readyToFire = false;
            float randomDelay = Random.Range(1, 3);
            Invoke("ResetFire", randomDelay);
           // gameObject.GetComponentInChildren<Animator>().SetTrigger("playerFire");
            //gameObject.GetComponentInChildren<Animator>().SetTrigger("enemyFire");

            GameObject projectile = Instantiate(projectilePrefab,
                transform.position + transform.forward, transform.rotation) as GameObject;
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);

            projectile.transform.SetParent(GameObject.FindGameObjectWithTag("ProjectileParent").transform);
            
        }
    }

    void ResetFire()
    {
        // wait a random amount of seconds before firing again
        readyToFire = true;
    }
}
