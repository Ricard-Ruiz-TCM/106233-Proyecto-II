using UnityEngine;

public class CameraMovement : MonoBehaviour {

    // Target, aka player
    private Transform _player;

    // Systems
    private PlayerDrawing _drawing; 
    private PlayerMovement _movement;
    private PlayerDash _dash;

    private Vector3 BossPosition;

    // Camera Movement controll
    [SerializeField]
    private float _deltaX;
    [SerializeField]
    private float _nextDeltaX;
    [SerializeField]
    private float _deltaY;
    [SerializeField]
    private float _str;

    private bool _holdY;

    private bool _OnBossRoom;

    // Temp vector3 for next pos
    [SerializeField]
    Vector3 _nextPos;

    private float _timeBaseSmooth;

    // Unity
    void Start() {
        _player = GameObject.FindObjectOfType<Player>().gameObject.transform;

        _deltaX = 0.0f;
        _nextDeltaX = 0.0f;
        _deltaY = 0.0f;
        _str = 8.0f;

        _OnBossRoom = false;

        _holdY = false;

        _timeBaseSmooth = 0.5f;

        BossPosition = new Vector3(91f, -67.25f, -20.0f);

        _drawing = _player.GetComponent<PlayerDrawing>();
        _movement = _player.GetComponent<PlayerMovement>();
        _dash = _player.GetComponent<PlayerDash>();
    }

    // Unity
    void Update() {

        if (Vector2.Distance(transform.position, BossPosition) < 5.0f) _OnBossRoom = true;

        _nextDeltaX = 2.0f * _player.transform.right.x;

        if (_drawing.IsDrawing()) return;
        if (_movement.IsMoving() || _dash.IsDashing()){
            float value = Time.deltaTime * 1.15f;
            if (_dash.IsDashing()) value += Time.deltaTime;
            if (_deltaX > _nextDeltaX) _deltaX -= value;
            else if (_deltaX < _nextDeltaX) _deltaX += value;
        }

        _nextPos = new Vector3(_player.position.x + _deltaX, _player.position.y + _deltaY, transform.position.z);
        if (_holdY) {
            _nextPos.y = transform.position.y;
        }

        if (_OnBossRoom) _nextPos = BossPosition;

        transform.position = Vector3.Lerp(transform.position, _nextPos, _str * Time.deltaTime);
    }

    public void SetDeltaY(float y) {
        _deltaY = y;
    }

    public void SetDeltaX(float x){
        _deltaX = x;
    }

    public void StopYMovement()
    {
        _holdY = true;
    }

    public void EnableYMovement()
    {
        _holdY = false;
    }

    
}