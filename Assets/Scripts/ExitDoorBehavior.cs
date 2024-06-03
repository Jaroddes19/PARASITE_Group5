using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.black;
        gameObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(LevelManager.isGameOver);
        if (LevelManager.isGameOver)
        {
            foreach (Transform child in transform) {
                child.gameObject.SetActive(true);
            }
            gameObject.GetComponent<Renderer>().material.color = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time, 1));
            gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (LevelManager.isGameOver)
        {
            if (other.gameObject.tag == "Player")
            {
                FindObjectOfType<LevelManager>().loadNextLevel();
            }
        }
    }
}
