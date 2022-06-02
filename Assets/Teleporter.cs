using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    private Transform _camera;
    private Transform _player;

    private Transform _destiny;

    private void Start() {
        _camera = Camera.main.transform;
        _player = GameObject.FindObjectOfType<Player>().transform;

        _destiny = transform.GetChild(0).transform;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject == null) return;
        if (collision.gameObject.tag != "Player") return;

        Teleport();
    }

    private void Teleport() {
        _player.position = new Vector3(_destiny.position.x, _destiny.position.y, _player.position.z);
        _camera.position = new Vector3(_destiny.position.x, _destiny.position.y, _camera.position.z);
    }
}
