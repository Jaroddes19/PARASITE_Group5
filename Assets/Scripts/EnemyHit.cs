<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHit : MonoBehaviour
{
    private static int enemyCount = 0;

    public GameObject bloodSplatter;
    public Slider healthSlider;

    public AudioClip deathSFX;

    CharacterAttributes charAttrs;


    void Start()
    {
        enemyCount++;
        FindObjectOfType<LevelManager>().SetEnemiesText(enemyCount);

        charAttrs = gameObject.GetComponentInParent<CharacterAttributes>();
        charAttrs.currentHealth = charAttrs.maxHealth;

        if (healthSlider == null)
        {
            healthSlider = gameObject.GetComponentInChildren<Slider>();
        }
        healthSlider.maxValue = charAttrs.maxHealth;
        healthSlider.value = charAttrs.currentHealth;
        if (bloodSplatter == null)
        {
            bloodSplatter = Resources.Load("BloodSplatter") as GameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            if (charAttrs.currentHealth > 0)
            {
                TakeDamage(other.gameObject.GetComponent<ProjectileAttributes>().projectileDamage, other);
            }
            Destroy(other.gameObject);
        }
        Invoke("Inertia", 0.5f);
    }

    //Stops enemy from skating away in a direction after a heavy collision or bounce
    void Inertia()
    {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    /*
     * Apply damage to the enemy. Accepts the collision object as well to provide
     * additional damage effects
     */
    public void TakeDamage(int damage, Collision collision = null)
    {
        if (charAttrs.currentHealth > 0)
        {
            charAttrs.currentHealth -= damage;
            healthSlider.value = charAttrs.currentHealth;
            // Removed because these could push the enemy through walls and other colliders
            //transform.position -= transform.forward * 2f
            //gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            if (collision != null)
            {
                gameObject.GetComponent<Rigidbody>().AddForce(collision.contacts[0].normal * 2, ForceMode.Impulse);
            }
            if (charAttrs.currentHealth <= 0)
            {
                DestroySelf();
            }
        }
    }
<<<<<<< HEAD
}
=======
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
>>>>>>> e727db38aa2f6e6ab94f60a900d04b142a236eef
=======
    void DestroySelf()
    {
        enemyCount--;
        FindObjectOfType<LevelManager>().SetEnemiesText(enemyCount);
        if (enemyCount == 0)
        {
            FindObjectOfType<LevelManager>().LevelBeat();
        }
        AudioSource.PlayClipAtPoint(deathSFX, transform.position);
        Instantiate(bloodSplatter, transform.position, transform.rotation).SetActive(true);
        gameObject.SetActive(false);
        Destroy(gameObject, 0.5f);
    }

    public void Parasitized()
    {
        enemyCount--;
    }



}
>>>>>>> 9c62eb2a5a17e4dd723d1db12bce69f157028f8a
