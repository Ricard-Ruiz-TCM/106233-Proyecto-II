using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuPause;
    public GameObject menuOptions;
    public GameObject menuSettings;

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
            OptionsClose();
            menuPause.GetComponent<Animator>().SetBool("Pause", false);
            Time.timeScale = 1f;
        }
    }

    public void Options() {
        menuOptions.GetComponent<RectTransform>().anchoredPosition = new Vector2(-530, -400);
        menuSettings.GetComponent<RectTransform>().anchoredPosition = new Vector2(1075, -40);
    }

    public void OptionsClose(){
        menuOptions.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -400);
        menuSettings.GetComponent<RectTransform>().anchoredPosition = new Vector2(1500, -40);
    }

    public void ExitClick(){
        SceneManager.LoadScene("MainMenu");
    }
}
