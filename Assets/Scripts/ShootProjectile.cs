using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootProjectile : MonoBehaviour
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
        if (Input.GetButtonDown("Fire2"))
        {
            Fire();

        }
    }

    void Fire()
    {
        if (readyToFire)    
        {
            readyToFire = false;
            Invoke("ResetFire", projectileDelay);
            gameObject.GetComponentInChildren<Animator>().SetTrigger("playerFire");
            GameObject projectile = Instantiate(projectilePrefab,
                transform.position + transform.forward, transform.rotation) as GameObject;
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);

            projectile.transform.SetParent(GameObject.FindGameObjectWithTag("ProjectileParent").transform);
            
        }
    }

    void ResetFire()
    {
        readyToFire = true;
    }
}
