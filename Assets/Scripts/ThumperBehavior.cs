using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThumperBehavior : EnemyBehavior
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Invoke("Thump", Random.Range(1, 5));
    }

    void Thump()
    {
        // Causes Mob to jump towards player
        Debug.Log("Thumper is thumping");
        gameObject.GetComponentInChildren<Animator>().SetTrigger("Thump");
        
    }
}
