using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MMCanvas : MonoBehaviour {

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private GameObject _optionsPanel;

    public GameObject _option1;
    public GameObject _option2;
    public GameObject _option3;

    [SerializeField]
    private GameObject _selector;

    int current = 0;
    int total_anims = 2;

    [SerializeField]
    private Texture2D _cursor;

    // Start is called before the first frame update
    void Start()
    {
        _optionsPanel.SetActive(false);
        MusicPlayer.Instance.PlayMusic("MainTitle_music_V2/MainTitle_music", 1f, true);
        InvokeRepeating("Changeanim", 0.0f, 6.0f);

        Cursor.SetCursor(_cursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnClickNewGame(){
        _selector.transform.SetParent(_option1.transform);
        _selector.GetComponent<RectTransform>().anchoredPosition = new Vector2(-75.0f, -35.0f);
        MusicPlayer.Instance.PlayFX("Buttons_menu_SelectOption");
        SceneManager.LoadScene(cinematicchecker.Instance.NextLevel());
    }

    public void OnClickOptions(){
        _selector.transform.SetParent(_option2.transform);
        _selector.GetComponent<RectTransform>().anchoredPosition = new Vector2(-75.0f, -35.0f);
        _optionsPanel.SetActive(!_optionsPanel.gameObject.activeSelf);
        MusicPlayer.Instance.PlayFX("Buttons_menu_ChangeSelection");

    }

    public void OnClickExit(){
        _selector.transform.SetParent(_option3.transform);
        _selector.GetComponent<RectTransform>().anchoredPosition = new Vector2(-75.0f, -35.0f);
        Application.Quit(0);
    }

    public void Changeanim(){
        GetComponent<Animator>().SetBool(current.ToString(), false);
        current = Random.Range(0, total_anims);
        GetComponent<Animator>().SetBool(current.ToString(), true);
    }

}
