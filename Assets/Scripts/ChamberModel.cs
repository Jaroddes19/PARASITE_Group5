using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChamberModel : MonoBehaviour
{
    float rotationAmount = 30f;

    Vector3 startPosition;
    Vector3 targetPosition;

    private void Start() {
        startPosition = transform.position;
        targetPosition = transform.position + new Vector3(0, 2, 0);
    }
    // Update is called once per frame
    void Update()
    {


        transform.Rotate(Vector3.up * Time.deltaTime * rotationAmount);

        float t = Mathf.PingPong(Time.time * 0.3f, 1f);
        transform.position = Vector3.Lerp(startPosition, targetPosition, t);
    }
}
