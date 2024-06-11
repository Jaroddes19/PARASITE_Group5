using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;

public class CharacterAttributes : MonoBehaviour
{
    // The string type that determines what character the entity that has this file attached is
    //should really be made into an enumeration eventually
    public string characterType = "Parasite";

    /*
     * Attributes to be accessed by the entity this script is attached to:
     */
    //health
    // NOTE: current health must = max health in this file so when the player takes over they receive the enemy's current hp
    public int currentHealth;
    public int maxHealth;

    //movement
    public float speed;
    public float jumpHeight;

    //attacking
    //first attack
    public float attackOneRange;
    public int attackOneDmg;
    public float attackOneSpeed;
    //second attack
    public float attackTwoRange;
    public int attackTwoDmg;
    public float attackTwoSpeed;

    //ability 
    public float abilityCooldown;
    
    /*
     * Attributes for each characterType:
     */
    // PARASITE:
    int para_MaxHp = 50;
    int para_CurHp = 50;

    float para_Speed = 6f;
    float para_JmpHgt = 3.5f;

    float para_AtkOneRange = 5f;
    int para_atkOneDmg = 15;
    float para_atkOneSpeed = 2f;
    float para_AtkTwoRange = 5f;
    int para_atkTwoDmg = 5;
    float para_atkTwoSpeed = 3f;

    float para_ablCooldown = 4f;

    // LAB MANAGER (example, can change):
    int lm_MaxHp = 100;
    int lm_CurHp = 100;

    float lm_Speed = 2.5f;
    float lm_JmpHgt = 2f;

    float lm_AtkOneRange = 3f;
    int lm_atkOneDmg = 5;
    float lm_atkOneSpeed = 4f;
    float lm_AtkTwoRange = 10f;
    int lm_atkTwoDmg = 10;
    float lm_atkTwoSpeed = 8f;

    // card swipe doesn't need any real cooldown
    float lm_ablCooldown = 0.5f;

    // mega blob - big and slow
    int blob_MaxHp = 300;
    int blob_CurHp = 300;

    float blob_Speed = 1.25f;
    float blob_JmpHgt = 1.25f;

    float blob_AtkOneRange = 5f;
    int blob_atkOneDmg = 8;
    float blob_atkOneSpeed = 5f;
    float blob_AtkTwoRange = 3f;
    int blob_atkTwoDmg = 10;
    float blob_atkTwoSpeed = 8f;

    // big ground slam
    float blob_ablCooldown = 10f;

    // flying flotus - fast and ranged
    int ff_MaxHp = 30;
    int ff_CurHp = 30;

    float ff_Speed = 10f;
    float ff_JmpHgt = 4f;

    float ff_AtkOneRange = 3f;
    int ff_atkOneDmg = 5;
    float ff_atkOneSpeed = 2f;
    float ff_AtkTwoRange = 10f; 
    int ff_atkTwoDmg = 10;
    float ff_atkTwoSpeed = 2f;


    // NEXT ENEMY TYPE HERE

    // Assigns values to the public, readable variables at creation
    public void Awake() {
        if (characterType == "Parasite") {
            //health
            currentHealth = para_CurHp;
            maxHealth = para_MaxHp;

            //movement
            speed = para_Speed;
            jumpHeight = para_JmpHgt;

            //attacking
            //first attack
            attackOneRange = para_AtkOneRange;
            attackOneDmg = para_atkOneDmg;
            attackOneSpeed = para_atkOneSpeed;
            //second attack
            attackTwoRange = para_AtkTwoRange;
            attackTwoDmg = para_atkTwoDmg;
            attackTwoSpeed = para_atkTwoSpeed;

            abilityCooldown = para_ablCooldown;
        } 
        else if (characterType == "LabManager") {
            //health
            currentHealth = lm_CurHp;
            maxHealth = lm_MaxHp;

            //movement
            speed = lm_Speed;
            jumpHeight = lm_JmpHgt;

            //attacking
            //first attack
            attackOneRange = lm_AtkOneRange;
            attackOneDmg = lm_atkOneDmg;
            attackOneSpeed = lm_atkOneSpeed;
            //second attack
            attackTwoRange = lm_AtkTwoRange;
            attackTwoDmg = lm_atkTwoDmg;
            attackTwoSpeed = lm_atkTwoSpeed;

            abilityCooldown = lm_ablCooldown;
        }
        else if (characterType == "Blob")
        {
            //health
            currentHealth = blob_CurHp;
            maxHealth = blob_MaxHp;

            //movement
            speed = blob_Speed;
            jumpHeight = blob_JmpHgt;

            //attacking
            //first attack
            attackOneRange = blob_AtkOneRange;
            attackOneDmg = blob_atkOneDmg;
            attackOneSpeed = blob_atkOneSpeed;
            //second attack
            attackTwoRange = blob_AtkTwoRange;
            attackTwoDmg = blob_atkTwoDmg;
            attackTwoSpeed = blob_atkTwoSpeed;

            abilityCooldown = blob_ablCooldown;
        }
        else if (characterType == "FlyingFlotus") {
            currentHealth = ff_CurHp;
            maxHealth = ff_MaxHp;

            speed = ff_Speed;
            jumpHeight = ff_JmpHgt;

            attackOneRange = ff_AtkOneRange;
            attackOneDmg = ff_atkOneDmg;
            attackOneSpeed = ff_atkOneSpeed;

            attackTwoRange = ff_AtkTwoRange;
            attackTwoDmg = ff_atkTwoDmg;
            attackTwoSpeed = ff_atkTwoSpeed;
        }
        else {
            throw new ArgumentException("Invalid character type provided: " + characterType);
        }
    }

    public async void AttackOne()
    {
        if (characterType == "Parasite")
        {
            //TODO
        }
        // card swipe to activate objects, open doors
        else if (characterType == "LabManager")
        {
            //TODO
        }
        else if (characterType == "Blob")
        {
            //slam attack for blob
            gameObject.transform.position += Vector3.up * 2.5f;
            GameObject slamEffect = Resources.Load("BlobSlamHitEffects") as GameObject;

            await Task.Delay(1000);
            GameObject blob = Instantiate(slamEffect, transform.position + Vector3.down * 0.9f, Quaternion.Euler(0, 0, 0));
            blob.SetActive(true);
        }
        else
        {
            throw new ArgumentException("Invalid character type provided: " + characterType);
        }
    }

    public void EntityAbility()
    {
        // dash ability
        if (characterType == "Parasite")
        {
            Vector3 dashDirection;
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            dashDirection = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized;
            gameObject.GetComponent<CharacterController>().Move(dashDirection * 2f);
        } 
        // card swipe to activate objects, open doors
        else if (characterType == "LabManager")
        {
            Debug.Log("Triggering ability for lab manager");
        }
        else if (characterType == "Blob")
        {
            Debug.Log("Triggering ability for blob");
        }
        else {
            throw new ArgumentException("Invalid character type provided: " + characterType);
        }
    }
}
