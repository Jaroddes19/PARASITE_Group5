using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    CharacterAttributes charAttrs;
    Vector3 input, moveDirection;

    float abilityCooldownTimer;

    float jumpHeight;
    float moveSpeed;
    public float gravity = 9.81f;
    public float airControl = 0.5f;

    float speedMult = 1f;
    public float oldFOV;
    float newFOV;

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();

        charAttrs = gameObject.GetComponentInParent<CharacterAttributes>();
        jumpHeight = charAttrs.jumpHeight;
        moveSpeed = charAttrs.speed * 1.1f;

        newFOV = oldFOV = Camera.main.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        if (abilityCooldownTimer > 0)
        {
            abilityCooldownTimer -= Time.deltaTime;
        }
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        input = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized * speedMult;
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

        if (charAttrs.characterType != "FlyingFlotus") {
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        } else
        {
            //FF moves in direction of camera, projects the input vector onto camera's xy plane (relative to the direction it's pointed), then normalizes+scales it
            controller.Move(Vector3.ProjectOnPlane(input, 
                Vector3.Cross(Camera.main.transform.forward, Camera.main.transform.right)).normalized * speedMult * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftShift) && abilityCooldownTimer <= 0) {
            charAttrs.EntityAbility();
            abilityCooldownTimer = charAttrs.abilityCooldown;
        }

        //play with FOV when speed boosted
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, newFOV, 8 * Time.deltaTime);
    }

    public void IncreaseSpeed(float newMult, float duration, float fovChange)
    {
        speedMult = newMult;
        newFOV = fovChange;
        Invoke("EndSpeedMult", duration);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, (10* Camera.main.transform.forward) + transform.position);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, (10 * Camera.main.transform.right) + transform.position);
    }

    void EndSpeedMult()
    {
        speedMult = 1.0f;
        newFOV = oldFOV;
    }
}