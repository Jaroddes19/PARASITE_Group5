using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Text gameText;

    public Text waveText;
    public AudioClip gameWinSFX;

    public static bool isGameOver = false;

    public string nextLevel;
    public Image storyWindow;
    public GameObject storyText;
    public string mainMenuSceneName = "MainMenu";
    Image crosshair;

    int waves = 0;

    int enemies;

    AudioSource backgroundSFX;

    void Start()
    {
        storyWindow.enabled = false;

        crosshair = GameObject.Find("Crosshair").GetComponent<Image>();

        if (waveText == null)
        {
            waveText = GameObject.Find("WaveText").GetComponent<Text>();
        }
        if (gameText == null)
        {
            gameText = GameObject.Find("GameText").GetComponent<Text>();

        }
        isGameOver = false;
        gameText.color = Color.green;

        backgroundSFX = Camera.main.GetComponent<AudioSource>();
        backgroundSFX.Play();

        if (SceneManager.GetActiveScene().name == mainMenuSceneName)
        {
            HideStoryWindow();
        } else
        {
            ShowStoryWindow();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (waveText == null)
        {
            waveText = GameObject.Find("WaveText").GetComponent<Text>();
        }
        if (gameText == null)
        {
            gameText = GameObject.Find("GameText").GetComponent<Text>();
        }

        //exiting story window
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            HideStoryWindow();
        }
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

    void beatLevelText()
    {
        waveText.gameObject.SetActive(false);
        gameText.text = "Floor Cleared\nProceed to exit!";
        gameText.gameObject.SetActive(true);
    }

    public void loadNextLevel()
    {
        if (!string.IsNullOrEmpty(nextLevel))
        {
            SceneManager.LoadScene(nextLevel);

            //show the window with the story blurb
            if (nextLevel != mainMenuSceneName)
            {
                ShowStoryWindow();
            }
        }
    }

    void loadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ShowStoryWindow()
    {
        crosshair.enabled = false;
        storyWindow.enabled = true;
        storyText.SetActive(true);
        Time.timeScale = 0.0f;
        storyText.GetComponent<TextMeshProUGUI>().text = SetStoryText();
    }

    void HideStoryWindow()
    {
        crosshair.enabled = true;
        storyWindow.enabled = false;
        storyText.SetActive(false);
        Time.timeScale = 1.0f;
    }

    string SetStoryText()
    {
        string storyText = "";

        if (nextLevel == "Level2")
        {
            storyText = "You are an alien parasite, quarantined in a small lab. The scientists within" +
                "study you and other creatures within to discover what secrets you hold. Little do they know," +
                "you've been able to escape your testing chamber and will consume whatever is in your way to " +
                "escape the evil lab you're in." +
                "\n" +
                "\nUse WASD to move (and dismiss this window)." +
                "\nUse Left Shift to activate your character's special" +
                "ability." +
                "\nUse Space to jump.";
        }
        else if (nextLevel == "Level3")
        {
            storyText = "level 2";
        }
        else
        {
            storyText = "shouldn't get here";
        }
        
        return storyText;
    }
}
