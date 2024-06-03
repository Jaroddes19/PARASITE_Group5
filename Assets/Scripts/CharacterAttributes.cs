using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterAttributes : MonoBehaviour
{
    // The string type that determines what character the entity that has this file attached is
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
    
    /*
     * Attributes for each characterType:
     */
    // PARASITE:
    int para_MaxHp = 50;
    int para_CurHp = 50;

    float para_Speed = 6f;
    float para_JmpHgt = 3f;

    float para_AtkOneRange = 5f;
    int para_atkOneDmg = 15;
    float para_atkOneSpeed = 5f;
    float para_AtkTwoRange = 5f;
    int para_atkTwoDmg = 5;
    float para_atkTwoSpeed = 5f;

    // LAB MANAGER (example, can change):
    int lm_MaxHp = 100;
    int lm_CurHp = 100;

    float lm_Speed = 1f;
    float lm_JmpHgt = 1f;

    float lm_AtkOneRange = 3f;
    int lm_atkOneDmg = 5;
    float lm_atkOneSpeed = 4f;
    float lm_AtkTwoRange = 10f;
    int lm_atkTwoDmg = 10;
    float lm_atkTwoSpeed = 8f;

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
        } 
        else {
            throw new ArgumentException("Invalid character type provided: " + characterType);
        }
    }
}
