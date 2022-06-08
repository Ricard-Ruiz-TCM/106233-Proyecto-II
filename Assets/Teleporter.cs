using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    private Transform _camera;
    private Transform _player;

    private Transform _destiny;

    private bool _teleporting;
    private void Start() {
        _teleporting = false;
        _camera = Camera.main.transform;
        _player = GameObject.FindObjectOfType<Player>().transform;

        _destiny = transform.GetChild(0).transform;
    }

    private void OnEnable()
    {
        Fader.OnFullAlpha += GoLVL2;
    }

    private void OnDisable()
    {
        Fader.OnFullAlpha -= GoLVL2;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject == null) return;
        if (collision.gameObject.tag != "Player") return;

        if (!_teleporting) Teleport();
    }

    private void Teleport() {
        _teleporting = true;
        GameManager.Instance.Fade();
    }

    public void GoLVL2() {
        _player.position = new Vector3(_destiny.position.x, _destiny.position.y, _player.position.z);
        _camera.position = new Vector3(_destiny.position.x, _destiny.position.y, _camera.position.z);
        GameObject.FindObjectOfType<ShadowIntensity>().StopSunshines();
        GameManager.Instance.GOTOLVL2();
    }
}
