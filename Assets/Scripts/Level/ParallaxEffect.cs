using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour {

    [SerializeField]
    private float _parallaxMultiplier;

    [SerializeField]
    private float _parallaxEffect;

    private Transform _camera;
    private Vector3 _lastCameraPos;
    

    void Start() {
        _camera = Camera.main.transform;
        _lastCameraPos = _camera.position;

        _parallaxEffect = 0.0f;

        Invoke("SetEffect", 1.0f);
    }

    public void SetEffect(){
        _parallaxEffect = _parallaxMultiplier;
    }

    void FixedUpdate() {
        float deltaX = (_camera.position.x - _lastCameraPos.x) * _parallaxEffect;
        transform.Translate(new Vector2(deltaX, 0.0f));
        _lastCameraPos = _camera.position;
    }
}
