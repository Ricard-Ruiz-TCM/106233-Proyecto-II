using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject optionMenu;
    public void SetVolume(float vol)
    {
        audioMixer.SetFloat("MasterVolume", vol);
    }

    public void SetFxVolume(float fx)
    {
        audioMixer.SetFloat("FXVolume", fx);
    }

    public void SetMusicVolume(float music)
    {
        audioMixer.SetFloat("MusicVolume", music);
    }

    public void Back()
    {
        optionMenu.SetActive(false);
    }
}
