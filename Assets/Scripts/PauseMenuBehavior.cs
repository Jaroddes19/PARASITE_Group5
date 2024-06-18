using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PauseMenuBehavior : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenu;
    public Slider sensitivitySlider;


    // Update is called once per frame
    void Start() {
         if (PlayerPrefs.HasKey("MouseSensitivity"))
        {
            MouseLook.mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
            sensToSlider(MouseLook.mouseSensitivity);
        }
        else 
        {
            PlayerPrefs.SetFloat("MouseSensitivity", MouseLook.mouseSensitivity);
            PlayerPrefs.Save();
            sensToSlider(MouseLook.mouseSensitivity);
        }
         if (sensitivitySlider != null)
        {
            sensToSlider(MouseLook.mouseSensitivity);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        // Unpause the game
        Time.timeScale = 1f;
        GameIsPaused = false;
        PauseMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause()
    {
        // Pause the game
        Time.timeScale = 0f;
        GameIsPaused = true;
        PauseMenu.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void QuitGame()
    {
        // Quit the game
        Application.Quit();
    }

    public void LoadMenu()
    {
        // Load the main menu
        Time.timeScale = 1f;
        GameIsPaused = false;
        PauseMenu.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void senstivitySliderValueChange()
    {
        if (sensitivitySlider == null)
        {
            Debug.Log("SLider is null");
        }
        // Change the mouse senstivity
        sliderToSens(sensitivitySlider.value);
        PlayerPrefs.SetFloat("MouseSensitivity", MouseLook.mouseSensitivity);
        PlayerPrefs.Save();
    }

    public void sliderToSens(float sliderValue)
    {
        // Convert the mouse sensitivity to a value between 1 and 10
        MouseLook.mouseSensitivity = sliderValue * 100;
    }
    public void sensToSlider(float sensitivity)
    {
        // Convert the mouse sensitivity to a value between 1 and 10
        sensitivitySlider.value = sensitivity / 100;
    }
}
