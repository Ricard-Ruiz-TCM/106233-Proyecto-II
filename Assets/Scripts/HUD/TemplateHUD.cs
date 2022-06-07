using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TemplateHUD : MonoBehaviour {

    private PlayerDrawing _player;
    private List<GameObject> _buttons;

    private void OnEnable() { FadeButtons(0); }

    void Awake(){
        _buttons = new List<GameObject>();
        LoadButtons(transform.Find("TemplateButtons"));
        _player = GameObject.FindObjectOfType<PlayerDrawing>();
    }

    private void LoadButtons(Transform parent){
        for (int i = 1; i < parent.childCount - 1; i++){
            _buttons.Add(parent.GetChild(i).gameObject);
        }
    }   
    
    private void FadeButtons(int id){
        foreach (GameObject g in _buttons){
            g.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        }
        _buttons[id].GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }
    
    public void OnClickButton(int id){
        FadeButtons(id);
        _player.NextTemplate(id);
    }

    public void LastTemplate() {
        _player.LastTemplate();
        FadeButtons(_player.TID);
    }

    public void NextTemplate() {
        _player.NextTemplate();
        FadeButtons(_player.TID);
    }

    public void OnClickBoxTemplate() { OnClickButton(0); }
    public void OnClickBombTemplate() { OnClickButton(1); }
    public void OnClickBulbTemplate() { OnClickButton(2); }
    public void OnClickKeyTemplate() { OnClickButton(3); }

}
