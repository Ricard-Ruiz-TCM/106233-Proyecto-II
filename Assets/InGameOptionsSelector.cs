using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameOptionsSelector : MonoBehaviour {

    private int _position;
    [SerializeField]
    public List<List<Vector2>> positions;

    public GameObject _ink;
    public GameObject _brush;

    void OnEnable(){
        Player.OnPause += StartSelector;
    }

    private void OnDisable() {
        Player.OnPause -= StartSelector;
    }

    private void StartSelector(bool pause) {
        if (!pause) return;
        _position = 0;
        positions = new List<List<Vector2>>();
        positions.Add(new List<Vector2>());
        positions[0].Add(new Vector2(-230, -180));
        positions[0].Add(new Vector2(294, -180));
        positions.Add(new List<Vector2>());
        positions[1].Add(new Vector2(-170, -317));
        positions[1].Add(new Vector2(233, -318));
        positions.Add(new List<Vector2>());
        positions[2].Add(new Vector2(-124, -461));
        positions[2].Add(new Vector2(205, -461));
        ChangePosition(0);
    }

    private void ChangePosition(int pos){
        _position = pos;
        _ink.GetComponent<RectTransform>().anchoredPosition = positions[_position][0];
        _brush.GetComponent<RectTransform>().anchoredPosition = positions[_position][1];
    }

    private void Click(){
        switch(_position){
            case 0:
                GameObject.FindObjectOfType<Player>().Pause();
                break;
            case 1:
                GameObject.FindObjectOfType<PauseMenu>().Options();
                break;
            case 2:
                GameObject.FindObjectOfType<PauseMenu>().ExitClick();
                break;
        }
    }

    
}