using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmLightBehavior : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0, 0, 1);
        foreach (Light light in gameObject.GetComponentsInChildren<Light>())
        {
            light.intensity = Mathf.PingPong(Time.time, 10);
        }
    }
}
