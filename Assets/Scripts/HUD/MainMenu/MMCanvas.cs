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

    // Start is called before the first frame update
    void Start()
    {
        _optionsPanel.SetActive(false);
    }


    public void OnClickNewGame(){
        _selector.transform.SetParent(_option1.transform);
        _selector.GetComponent<RectTransform>().anchoredPosition = new Vector2(-75.0f, -35.0f);
        SceneManager.LoadScene("GameIntro");
    }

    public void OnClickOptions(){
        _selector.transform.SetParent(_option2.transform);
        _selector.GetComponent<RectTransform>().anchoredPosition = new Vector2(-75.0f, -35.0f);
        _optionsPanel.SetActive(!_optionsPanel.gameObject.activeSelf);
    }

    public void OnClickExit(){
        _selector.transform.SetParent(_option3.transform);
        _selector.GetComponent<RectTransform>().anchoredPosition = new Vector2(-75.0f, -35.0f);
        Application.Quit(0);
    }

}
