using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
<<<<<<< HEAD
    public Text gameText;

    public Text waveText;

    public AudioClip gameOverSFX;

    public AudioSource backgroundSFX;

=======
    public float levelDuration = 10.0f;

    public Text timerText;

    public Text gameText;

    public Text scoreText;

    public AudioClip gameOverSFX;

>>>>>>> e727db38aa2f6e6ab94f60a900d04b142a236eef
    public AudioClip gameWinSFX;

    public static bool isGameOver = false;

    public string nextLevel;
<<<<<<< HEAD

    int waves = 0;

    int enemies;
=======
    float countdown;

    int score = 0;

>>>>>>> e727db38aa2f6e6ab94f60a900d04b142a236eef

    void Start()
    {
        isGameOver = false;
<<<<<<< HEAD
        gameText.color = Color.green;

        backgroundSFX = Camera.main.GetComponent<AudioSource>();
        backgroundSFX.Play();
=======
        countdown = levelDuration;
        SetTimerText();
>>>>>>> e727db38aa2f6e6ab94f60a900d04b142a236eef
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
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
=======
        if (!isGameOver)
        {
            if (countdown > 0)
            {
                countdown -= Time.deltaTime;
            }
            else
            {
                countdown = 0.0f;
                LevelLost();
            }

            SetTimerText();
        }

        // Debug.Log("Time remaining: " + countdown);
    }

    private void OnGUI()
    {
        // GUI.Box(new Rect(10, 10, 100, 20), "Time: " + countdown.ToString("0.00"));
    }

    void SetTimerText()
    {
        timerText.text = "Time: " + countdown.ToString("f2");

    }

    public void SetScoreText(int scoreValue)
    {
        if (countdown > levelDuration / 2)
        {
            score += scoreValue * 2;
        }
        else {
            score += scoreValue;
        }
        scoreText.text = "Score: " + score;

>>>>>>> e727db38aa2f6e6ab94f60a900d04b142a236eef
    }

    public void LevelLost()
    {
        isGameOver = true;
<<<<<<< HEAD
        gameText.text = "You are DEAD!";
        gameText.color = Color.red;
        gameText.gameObject.SetActive(true);

        Camera.main.GetComponent<AudioSource>().pitch = -0.5f;
        // AudioSource.PlayClipAtPoint(gameOverSFX, Camera.main.transform.position);

        Invoke("loadCurrentLevel", 3);
=======
        gameText.text = "You Lose!";
        gameText.gameObject.SetActive(true);

        Camera.main.GetComponent<AudioSource>().pitch = 0.5f;
        AudioSource.PlayClipAtPoint(gameOverSFX, Camera.main.transform.position);

        Invoke("loadCurrentLevel", 2);
>>>>>>> e727db38aa2f6e6ab94f60a900d04b142a236eef
    }

    public void LevelBeat()
    {
<<<<<<< HEAD
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
=======
        isGameOver = true;
        gameText.text = "You Win!";
        gameText.gameObject.SetActive(true);

        Camera.main.GetComponent<AudioSource>().pitch = 2f;
        AudioSource.PlayClipAtPoint(gameWinSFX, Camera.main.transform.position);

        if (!string.IsNullOrEmpty(nextLevel))
        {
            Invoke("loadNextLevel", 2);
>>>>>>> e727db38aa2f6e6ab94f60a900d04b142a236eef

        }
    }

<<<<<<< HEAD
=======
    void loadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);

    }

>>>>>>> e727db38aa2f6e6ab94f60a900d04b142a236eef
    void loadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
