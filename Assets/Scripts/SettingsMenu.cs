using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider sensitivitySlider;
    // Start is called before the first frame update
    void Start()
    {
        if (sensitivitySlider == null)
        {
            Debug.Log("SLider is null");
        }
        if (PlayerPrefs.HasKey("MouseSensitivity"))
        {
            MouseLook.mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
        }
        else 
        {
            PlayerPrefs.SetFloat("MouseSensitivity", MouseLook.mouseSensitivity);
            PlayerPrefs.Save();
        }
         if (sensitivitySlider != null)
        {
            sensToSlider(MouseLook.mouseSensitivity);
        }
    }
    public void senstivitySliderValueChange()
    {
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
