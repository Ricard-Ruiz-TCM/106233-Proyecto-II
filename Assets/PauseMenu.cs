using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static bool _paused = false;
    public GameObject menuPause;

    private void Start()
    {
        menuPause.SetActive(false);
    }

    private void Update()
    {
        Keyboard kb = InputSystem.GetDevice<Keyboard>();
        if(kb.escapeKey.wasPressedThisFrame)
        {
            Debug.Log("pause");
            if(_paused)
            {
                Continue();
            }
            else
            {
                Pause();
            }
        }
    }


    public void Continue()
    {
        menuPause.SetActive(false);
        Time.timeScale = 1f;
        _paused = false;
        //GameManager.FindObjectOfType<Player>().Pause();
    }

    public void Pause()
    {
        menuPause.SetActive(true);
        Time.timeScale = 0f;
        _paused = true;
        GameManager.FindObjectOfType<Player>().Pause();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
