using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuPause;

    float _time;
    bool _pause;

    private void OnEnable() {
        Player.OnPause += Pause;
    }

    private void OnDisable() {
        Player.OnPause += Pause;
    }

    private void Start() {
        Pause(false);
        _time = 0.0f;
    }

    void Update(){
        if (!_pause) return;

        if (_time >= 1.1f){
            Time.timeScale = 0f;
        }
        _time += Time.deltaTime;

    }

    public void Continue() {
        GameObject.FindObjectOfType<Player>().Pause();
    }

    public void Pause(bool ispaused) {
        _pause = ispaused;
        if (ispaused){
            _time = 0.0f;
            menuPause.GetComponent<Animator>().SetBool("Pause", true);
        } else {
            menuPause.GetComponent<Animator>().SetBool("Pause", false);
            Time.timeScale = 1f;
        }
    }

    public void Options() {
        menuPause.GetComponent<Animator>().SetBool("Options", true);
    }

    public void OptionsClose(){
        menuPause.GetComponent<Animator>().SetBool("Options", false);
    }

    public void ExitClick(){
        SceneManager.LoadScene("MainMenu");
    }
}
