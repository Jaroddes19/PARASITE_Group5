using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    CharacterAttributes charAttrs;
    Vector3 input, moveDirection;

    float jumpHeight;
    float moveSpeed;

    float abilityCooldownTimer;


    public float gravity = 9.81f;
    public float airControl = 0.5f;

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();

        charAttrs = gameObject.GetComponentInParent<CharacterAttributes>();
        jumpHeight = charAttrs.jumpHeight;
        moveSpeed = charAttrs.speed;
    }

    // Update is called once per frame
    void Update() 
    {
        abilityCooldownTimer -= Time.deltaTime;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        input = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized;

        input *= moveSpeed;

        if (controller.isGrounded)
        {

            moveDirection = input;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = Mathf.Sqrt(2 * jumpHeight * gravity);
            }
            else
            {
                moveDirection.y = 0.0f;
            }
        }
        else
        {
            input.y = moveDirection.y;
            moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        // player's movement ability
        if (Input.GetButton("LeftShift") && abilityCooldownTimer <= 0)
        {
            charAttrs.EntityAbility();
            abilityCooldownTimer = charAttrs.abilityCooldown;
        }
    }
}
