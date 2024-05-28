using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public float levelDuration = 10.0f;

    public Text timerText;

    public Text gameText;

    public Text scoreText;

    public AudioClip gameOverSFX;

    public AudioClip gameWinSFX;

    public static bool isGameOver = false;

    public string nextLevel;
    float countdown;

    int score = 0;


    void Start()
    {
        isGameOver = false;
        countdown = levelDuration;
        SetTimerText();
    }

    // Update is called once per frame
    void Update()
    {
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

    }

    public void LevelLost()
    {
        isGameOver = true;
        gameText.text = "You Lose!";
        gameText.gameObject.SetActive(true);

        Camera.main.GetComponent<AudioSource>().pitch = 0.5f;
        AudioSource.PlayClipAtPoint(gameOverSFX, Camera.main.transform.position);

        Invoke("loadCurrentLevel", 2);
    }

    public void LevelBeat()
    {
        isGameOver = true;
        gameText.text = "You Win!";
        gameText.gameObject.SetActive(true);

        Camera.main.GetComponent<AudioSource>().pitch = 2f;
        AudioSource.PlayClipAtPoint(gameWinSFX, Camera.main.transform.position);

        if (!string.IsNullOrEmpty(nextLevel))
        {
            Invoke("loadNextLevel", 2);

        }
    }

    void loadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);

    }

    void loadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
