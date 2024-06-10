using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // public AudioClip deathSFX;
    public Slider healthSlider;
    CharacterAttributes charAttrs;

    private int startingHealth;
    void Start()
    {
        charAttrs = gameObject.GetComponentInParent<CharacterAttributes>();
        healthSlider.maxValue = charAttrs.maxHealth;

        if (healthSlider == null ) {
            healthSlider = GameObject.Find("HealthBar").GetComponent<Slider>();
        }
        healthSlider.value = charAttrs.currentHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        if (charAttrs.currentHealth > 0)
        {
            charAttrs.currentHealth -= damage;
            healthSlider.value = charAttrs.currentHealth;
        }
        if (charAttrs.currentHealth <= 0)
        {
            PlayerDies();
        }

        Debug.Log("Player health is: " + charAttrs.currentHealth);
    }

    public void Heal(int health)
    {
        if (charAttrs.currentHealth < charAttrs.maxHealth)
        {
            charAttrs.currentHealth += health;
            healthSlider.value = Mathf.Clamp(charAttrs.currentHealth, 0, 100);
        }

        Debug.Log("Player health with loot: " + charAttrs.currentHealth);
    }

    void PlayerDies()
    {
        Debug.Log("Player is dead");
        // AudioSource.PlayClipAtPoint(deathSFX, transform.position);
        transform.Rotate(-90, 0, 0, Space.Self);
        FindObjectOfType<LevelManager>().LevelLost();
    }
}
