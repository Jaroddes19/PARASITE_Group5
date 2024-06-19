using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabDoor : MonoBehaviour
{
    public float step = 5f;
    bool open = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            transform.position += Vector3.down * Time.deltaTime * step;
        } 
    }

    public void Open()
    {
        open = true;
        Destroy(gameObject, 4f);
    }
}
