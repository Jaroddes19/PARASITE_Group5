using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;

    public AudioClip shootSFX;

    float projectileSpeed;
    float projectileDelay;

    bool readyToFire = true;

    CharacterAttributes charAttrs;

    void Start()
    {
        charAttrs = gameObject.GetComponentInParent<CharacterAttributes>();
        projectileSpeed = projectilePrefab.GetComponent<ProjectileAttributes>().projectileSpeed;
        projectileDelay = charAttrs.attackTwoSpeed;
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
            AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position);

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
