using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessCard : MonoBehaviour
{
    public Vector3 offset = new Vector3(0.6f, 0.3f, 1);
    public float step = 2f;
    public float destroyTime = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.SetParent(Camera.main.transform);
        gameObject.SetActive(true);
        transform.localPosition = offset;
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= transform.position * step * Time.deltaTime;
    }
}
