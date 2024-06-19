using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessCard : MonoBehaviour
{
    public Vector3 offset = new Vector3(0.6f, 0.3f, 1);
    public float step = 4f;
    public float destroyTime = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.SetParent(Camera.main.transform);
        transform.localPosition = offset;
        transform.rotation = Camera.main.transform.rotation;
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (transform.rotation * Vector3.down).normalized * step * Time.deltaTime;
    }
}
