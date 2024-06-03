using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHit : MonoBehaviour
{
    public GameObject bloodSplatter;
    public Slider healthSlider;

    void Start()
    {
        var charAttrs = gameObject.GetComponentInParent<CharacterAttributes>();
        charAttrs.currentHealth = charAttrs.maxHealth;

        if (healthSlider == null)
        {
            healthSlider = gameObject.GetComponentInChildren<Slider>();
        }
        healthSlider.value = charAttrs.currentHealth;
        if (bloodSplatter == null)
        {
            bloodSplatter = GameObject.Find("ObjectsToDynamicallyFind").transform.Find("BloodSplatter").gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        var charAttrs = gameObject.GetComponentInParent<CharacterAttributes>();

        if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
            if (charAttrs.currentHealth > 0)
            {
                TakeDamage(other.gameObject.GetComponent<ProjectileAttributes>().projectileDamage);
            }
            if (charAttrs.currentHealth <= 0)
            {
                DestroySelf();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        var charAttrs = gameObject.GetComponentInParent<CharacterAttributes>();

        if (charAttrs.currentHealth > 0)
        {
            charAttrs.currentHealth -= damage;
            healthSlider.value = charAttrs.currentHealth;
            transform.position -= transform.forward * 2f;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        if (charAttrs.currentHealth <= 0)
        {
            DestroySelf();
        }
    }

    void DestroySelf()
    {
        Instantiate(bloodSplatter, transform.position, transform.rotation);
        gameObject.SetActive(false);
        Destroy(gameObject, 0.5f);
    }
}
