using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHit : MonoBehaviour
{
    public GameObject bloodSplatter;
    public Slider healthSlider;

    public int enemyHealth = 80;

    int currentHealth;

    void Start()
    {
        currentHealth = enemyHealth;
        healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
            if (currentHealth > 0)
            {
                TakeDamage(other.gameObject.GetComponent<ProjectileAttributes>().projectileDamage);

            }
            if (currentHealth <= 0)
            {
                DestroyEnemy();
            }
        }
    }
    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            healthSlider.value = currentHealth;
            transform.position -= transform.forward * 2f;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        if (currentHealth <= 0)
        {
            DestroyEnemy();
        }
    }

    void DestroyEnemy()
    {
        Instantiate(bloodSplatter, transform.position, transform.rotation);
        gameObject.SetActive(false);
        Destroy(gameObject, 0.5f);
    }
}
