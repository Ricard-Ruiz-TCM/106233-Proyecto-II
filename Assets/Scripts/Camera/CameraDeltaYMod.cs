using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDeltaYMod : MonoBehaviour {

    [SerializeField]
    private float _deltaMod;

    private CameraMovement _camera;

    private void Awake() {
        _camera = Camera.main.GetComponent<CameraMovement>();
    }

    private void Start() {
        gameObject.name = _deltaMod.ToString();
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject == null) return;
        if (collision.gameObject.tag != "Player") return;
        _camera.SetDeltaY(_deltaMod);
    }

}
