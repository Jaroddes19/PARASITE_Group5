using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Text gameText;

    public Text waveText;

    public AudioClip gameOverSFX;

    public AudioClip gameWinSFX;

    public static bool isGameOver = false;

    public string nextLevel;

    int waves = 0;

    int enemies;

    AudioSource backgroundSFX;

    void Start()
    {
        if (gameText == null)
        {
            Debug.LogError("GameText is not set in the LevelManager");
        }
        isGameOver = false;
        gameText.color = Color.green;

        backgroundSFX = Camera.main.GetComponent<AudioSource>();
        backgroundSFX.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // if (isGameOver)
        // {
        //     FindObjectOfType<EnemySpawner>().enabled = false;
        //     foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        //     {
        //         Destroy(enemy);
        //     }
        // }
    }


    public void SetEnemiesText(int enemyCount)
    {
        enemies = enemyCount;
        waveText.text = "Waves remaining: " + waves + "!\nEnemies remaining: " + enemies;
    }

    public void SetWaveText(int waves)
    {
        this.waves = waves;
        waveText.text = "Waves remaining: " + waves + "!\nEnemies remaining: " + enemies;
    }

    void beatLevelText()
    {
        waveText.gameObject.SetActive(false);
        gameText.text = "Floor Cleared\nProceed to exit!";
        gameText.gameObject.SetActive(true);
    }

    public void LevelLost()
    {
        isGameOver = true;
        gameText.text = "You are DEAD!";
        gameText.color = Color.red;
        gameText.gameObject.SetActive(true);

        Camera.main.GetComponent<AudioSource>().pitch = -0.5f;
        // AudioSource.PlayClipAtPoint(gameOverSFX, Camera.main.transform.position);

        Invoke("loadCurrentLevel", 3);
    }

    public void LevelBeat()
    {
        if (waves == 0)
        {
            isGameOver = true;
            Invoke("beatLevelText", 1);
            Camera.main.GetComponent<AudioSource>().pitch = 3f;
            AudioSource.PlayClipAtPoint(gameWinSFX, GameObject.FindGameObjectWithTag("Exit").transform.position);
        }

    }

    public void loadNextLevel()
    {
        if (!string.IsNullOrEmpty(nextLevel))
        {
            SceneManager.LoadScene(nextLevel);

        }
    }

    void loadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
