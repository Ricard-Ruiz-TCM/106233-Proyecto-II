using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

    [SerializeField]
    private GameObject _templateHUD;

    private GameObject _player;

    void OnEnable() {
        Player.OnChangeState += OnPlayerChangeState;   
    }

    void OnDisable() {
        Player.OnChangeState -= OnPlayerChangeState;   
    }

    void Start(){
        _templateHUD.SetActive(false);
        _player = GameObject.FindObjectOfType<Player>().gameObject;
    }

    public void OnPlayerChangeState(PLAYER_STATE state){
        if (state == PLAYER_STATE.PS_DRAW) ShowTemplateHUD();
        else if (_templateHUD.activeSelf) HideTemplateHUD();
    }

    public void ShowTemplateHUD(){
        _templateHUD.SetActive(true);
    }

    public void HideTemplateHUD(){
        _templateHUD.SetActive(false);
    }

}
