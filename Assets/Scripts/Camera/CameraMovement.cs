using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private Camera _camera;
    private Transform _player;

    [SerializeField]
    private Vector3 _offset;

    void Start() {
        _camera = GetComponent<Camera>();
        _player = GameObject.FindObjectOfType<Player>().gameObject.transform;
    }

    void FixedUpdate() {
        _camera.transform.position = new Vector3(_player.position.x, _player.position.y, _camera.transform.position.z);
    }

}