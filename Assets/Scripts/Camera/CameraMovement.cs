using UnityEngine;

public class CameraMovement : MonoBehaviour {

    // Target, aka player
    private Transform _player;
    private Rigidbody2D _player_body;

    // Systems
    private PlayerDash _dash;
    private PlayerFall _fall;
    private PlayerMovement _movement;

    // Camera Movement controll
    [SerializeField]
    private float _deltaX;
    [SerializeField]
    private float _str;

    // Temp vector3 for next pos
    [SerializeField]
    Vector3 _nextPos;

    // Unity
    void Start() {
        _player = GameObject.FindObjectOfType<Player>().gameObject.transform;
        _player_body = _player.gameObject.GetComponent<Rigidbody2D>();

        _deltaX = 1.75f;
        _str = 1.0f;

        _fall = _player.GetComponent<PlayerFall>();
        _dash = _player.GetComponent<PlayerDash>();
        _movement = _player.GetComponent<PlayerMovement>();
    }

    // Unity
    void Update() {
        _nextPos = new Vector3(_player.position.x, _player.position.y, transform.position.z);

        _nextPos.x += (_deltaX * _player.transform.right.x);
        _nextPos.y += (_player_body.velocity.y / 1.8f);

        _str = 1.0f;
        if (_fall.IsFalling()) _str += 3.0f;
        if (_dash.IsDashing()) _str += 4.0f;
        if (_movement.IsMoving()) _str += 3.0f;

        transform.position = Vector3.Lerp(transform.position, _nextPos, _str * Time.deltaTime);
    }

}