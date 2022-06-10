using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool _paused = false;
    public GameObject menuPause;
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
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

    void Continue()
    {
        menuPause.SetActive(false);
        Time.timeScale = 1f;
        _paused = true;
    }

    void Pause()
    {
        menuPause.SetActive(true);
        Time.timeScale = 0f;
        _paused = true;
    }
}
