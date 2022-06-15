using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool _paused = false;
    public GameObject menuPause;

    private void OnEnable() {
        Player.OnPause += Pause;
    }

    private void OnDisable() {
        Player.OnPause += Pause;
    }

    private void Start() {
        Pause(false);
    }

    public void Continue() {
        GameObject.FindObjectOfType<Player>().Pause();
    }

    public void Pause(bool ispaused) {
        if (ispaused){
            menuPause.SetActive(true);
            Time.timeScale = 0f;
            _paused = true;
        } else {
            menuPause.SetActive(false);
            Time.timeScale = 1f;
            _paused = false;
        }
    }

    public void LoadMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitClick(){
        Application.Quit(0);
    }
}
