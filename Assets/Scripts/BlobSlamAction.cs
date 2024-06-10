using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobSlamAction : MonoBehaviour
{

    public int slamDamage = 15;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x + transform.localScale.x * (10 * Time.deltaTime), transform.localScale.y,
            transform.localScale.z + transform.localScale.z * (10 * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(slamDamage);
            other.gameObject.GetComponent<CharacterController>().Move(-other.transform.forward * 2);
        }
        Invoke("Inertia", 0.5f);
    }
}
