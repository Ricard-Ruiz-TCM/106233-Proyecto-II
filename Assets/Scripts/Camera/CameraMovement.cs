using UnityEngine;

public class CameraMovement : MonoBehaviour {

    // Target, aka player
    private Transform _player;
    private Rigidbody2D _player_body;

    // Camera Movement controll
    [SerializeField]
    private float _deltaX;
    [SerializeField]
    private float _smooth;

    // Temp vector3 for next pos
    [SerializeField]
    Vector3 _nextPos;

    // Unity
    void Start() {
        _player = GameObject.FindObjectOfType<Player>().gameObject.transform;
        _player_body = _player.gameObject.GetComponent<Rigidbody2D>();

        _deltaX = 1.25f;
        _smooth = 1.0f;
    }

    // Unity
    void Update() {
        _nextPos = new Vector3(_player.position.x, _player.position.y, transform.position.z);

        _nextPos.x += (_deltaX * _player.transform.right.x);
        _nextPos.y += (_player_body.velocity.y / 2.0f);

        transform.position = Vector3.Lerp(transform.position, _nextPos, _smooth * Time.deltaTime);
    }

}