using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour {

    [SerializeField]
    private float _parallaxMultiplier;

    private Transform _camera;
    private Vector3 _lastCameraPos;
    

    void Start() {
        _camera = Camera.main.transform;
        _lastCameraPos = _camera.position;
    }

    void FixedUpdate() {
        float deltaX = (_camera.position.x - _lastCameraPos.x) * _parallaxMultiplier;
        float deltaY = (_camera.position.y - _lastCameraPos.y) * (_parallaxMultiplier / 2.0f);
        transform.Translate(new Vector2(deltaX, deltaY));
        _lastCameraPos = _camera.position;
    }
}
