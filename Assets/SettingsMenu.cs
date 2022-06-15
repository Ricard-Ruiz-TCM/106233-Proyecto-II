using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject optionMenu;
    public Toggle toggle;
    public Slider slider;
    public Slider fxSlider;
    public Slider musicSlider;

    private void Start()
    {     
        PlayerPrefs.GetFloat("Volume");
        PlayerPrefs.GetFloat("MusicVolume");
        PlayerPrefs.GetFloat("FXVolume");
    }

    public void SetVolume(float vol)
    {
        audioMixer.SetFloat("MasterVolume", vol);
        PlayerPrefs.SetFloat("Volume", vol);
    }

    public void SetFxVolume(float fx)
    {
        audioMixer.SetFloat("FXVolume", fx);
        PlayerPrefs.SetFloat("FXVolume", fx);
    }

    public void SetMusicVolume(float music)
    {
        audioMixer.SetFloat("MusicVolume", music);
        PlayerPrefs.SetFloat("MusicVolume", music);
    }

    public void SetFullscreen()
    {
        if(toggle.isOn)
        {
            Screen.fullScreen = true;
            Debug.Log("Fullscreen");

        }
        else 
        {
            Screen.fullScreen= false;
            Debug.Log("Not fullscreen");
        }
    }


    public void Back()
    {
        optionMenu.SetActive(false);
    }
}
