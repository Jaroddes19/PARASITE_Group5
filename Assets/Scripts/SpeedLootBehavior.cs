using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedLootBehavior : MonoBehaviour
{

    public float speedMult = 1.75f;
    public float speedDuration = 7.0f;
    public float fovChange = 10f;

    bool skip = false;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, 140 * Time.deltaTime);

        //lets the loot float a bit over the ground
        if (!skip && Physics.Raycast(transform.position, Vector3.down, Random.Range(1.5f, 4.5f)))
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
            skip = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);

            //update player's speed and FOV
            var playerControl = other.gameObject.GetComponent<PlayerController>();
            playerControl.IncreaseSpeed(speedMult, speedDuration, Camera.main.fieldOfView - fovChange);
            //AudioSource.PlayClipAtPoint(hpSFX, transform.position);

            Destroy(gameObject);
        }
    }
}
