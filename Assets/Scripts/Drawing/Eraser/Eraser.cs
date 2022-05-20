using UnityEngine;
using System.Collections.Generic;

public class Eraser : DrawElement {

    [SerializeField]
    private bool _erasing;
    private bool IsErasing() { return _erasing; }
    private void StartErasing() { _erasing = true; }
    private void StopErasing() { _erasing = false; }

    // Strokes Control
    private int _strokeID;
    public int GetStroke() { return _strokeID; }
    public bool CanErase() { return (_strokeID != -1); }

    // PlayerDrawing System
    private PlayerDrawing _player;

    void Awake() {
        _camera = Camera.main;
        _position = new Vector2(0.0f, 0.0f);
        _offset = new Vector2(0.5f, 0.375f);

        _erasing = false;
        _strokeID = -1;

        _player = GameObject.FindObjectOfType<PlayerDrawing>();
    }
   
    // Erasing Action
    public override void MainAction(bool action){
        if (!IsErasing() && action) {
            Show();
            StartErasing();
        } else if (IsErasing() && action) {
            Erasing();
        } else if (IsErasing() && !action) {
            StopErasing();
        }
    }

    private void Erasing() {
        bool erase = false;
        int i = 0;
        List<GameObject> _strokes = _player.Strokes();
        while (i < _strokes.Count && !erase) { 
            foreach (Vector2 pt in _strokes[i].GetComponent<Stroke>().ColliderPoints()) {
                if (Vector2.Distance(pt, ((Vector2)transform.position) - _offset) < 0.5f) {
                    erase = true;
                }
            }
            i++;
        }
        _strokeID = (erase ? (i - 1) : -1);
    }

}