using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    // public AudioClip deathSFX;
    public Slider healthSlider;

    int currentHealth;

    void Start()
    {
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            healthSlider.value = currentHealth;
        }
        if (currentHealth <= 0)
        {
            PlayerDies();
        }

        Debug.Log("Player health is: " + currentHealth);
    }

    public void Heal(int health)
    {
        if (currentHealth < 100)
        {
            currentHealth += health;
            healthSlider.value = Mathf.Clamp(currentHealth, 0, 100);
        }

        Debug.Log("Player health with loot: " + currentHealth);
    }

    void PlayerDies()
    {
        Debug.Log("Player is dead");
        // AudioSource.PlayClipAtPoint(deathSFX, transform.position);
        transform.Rotate(-90, 0, 0, Space.Self);
    }
}
