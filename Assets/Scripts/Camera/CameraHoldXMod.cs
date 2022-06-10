using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHoldXMod : MonoBehaviour
{


    private CameraMovement _camera;

    private void Awake()
    {
        _camera = Camera.main.GetComponent<CameraMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == null) return;
        if (collision.gameObject.tag != "Player") return;
        _camera.StopXMovement();
    }

}
