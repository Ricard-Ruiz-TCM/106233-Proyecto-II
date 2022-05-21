using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

    [SerializeField]
    private GameObject _templateHUD;

    void OnEnable() {
        Player.OnChangeState += OnPlayerChangeState;   
    }

    void OnDisable() {
        Player.OnChangeState -= OnPlayerChangeState;   
    }

    void Start(){
        _templateHUD.SetActive(false);
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
