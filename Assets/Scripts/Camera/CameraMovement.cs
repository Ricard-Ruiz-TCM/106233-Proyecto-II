using UnityEngine;

public class CameraMovement : MonoBehaviour {

    // Target, aka player
    private Transform _player;

    // Systems
    private PlayerDrawing _drawing; 
    private PlayerMovement _movement;
    private PlayerDash _dash;
    private PlayerWallFall _wfall;

    private Vector3 BossPosition;

    // Camera Movement controll
    [SerializeField]
    private float _deltaX;
    [SerializeField]
    private float _nextDeltaX;
    [SerializeField]
    private float _deltaY;
    [SerializeField]
    private float _nextDeltaY;
    [SerializeField]
    private float _str;

    private bool _holdY;
    private bool _holdX;

    public bool OnBoos => _OnBossRoom;

    private bool _holdTop;
    private bool _OnBossRoom;

    public GameObject _bossHUD;

    // Temp vector3 for next pos
    [SerializeField]
    Vector3 _nextPos;

    // Unity
    void Start() {
        _player = GameObject.FindObjectOfType<Player>().gameObject.transform;

        _deltaX = 2.0f;
        _nextDeltaX = 2.0f;
        _deltaY = 1.5f;
        _str = 8.0f;

        _OnBossRoom = false;

        _holdY = false;
        _holdX = false;

        _holdTop = false;

        _bossHUD.SetActive(false);

        BossPosition = new Vector3(91f, -67.25f, -20.0f);

        _drawing = _player.GetComponent<PlayerDrawing>();
        _movement = _player.GetComponent<PlayerMovement>();
        _dash = _player.GetComponent<PlayerDash>();
        _wfall = _player.GetComponent<PlayerWallFall>();
    }

    // Unity
    void Update() {

        if ((Vector2.Distance(transform.position, BossPosition) < 5.0f) && (!_OnBossRoom)){
            _OnBossRoom = true;
            _bossHUD.SetActive(true);
            MusicPlayer.Instance.PlayMusic("boss", 1f, true);
        }

        _nextDeltaX = 2.0f * _player.transform.right.x;

        if (_drawing.IsDrawing()) return;
        if (_movement.IsMoving() || _dash.IsDashing()){
            float value = Time.deltaTime * 1.15f;
            if (_dash.IsDashing()) value += Time.deltaTime;
            if (_deltaX > _nextDeltaX) _deltaX -= value;
            else if (_deltaX < _nextDeltaX) _deltaX += value;
        }

        if (Mathf.Abs(_deltaY - _nextDeltaY) > 0.1f){
            float value = Time.deltaTime * 0.9f;
            if (_wfall.IsEnabled()) value += Time.deltaTime * 0.6f;
            if (_deltaY > _nextDeltaY) _deltaY -= value;
            else if (_deltaY < _nextDeltaY) _deltaY += value;
        }

        _nextPos = new Vector3(_player.position.x + _deltaX, _player.position.y + _deltaY, transform.position.z);
        
        if (_holdY) { _nextPos.y = transform.position.y; }
        if (_holdTop) { 
            _nextPos.y = transform.position.y;
            if (_player.transform.position.y + _deltaY <= _nextPos.y) _holdTop = false;
        }
        if (_holdX) { 
            _nextPos.x = transform.position.x; 
            if (_player.transform.position.x + _deltaX >= _nextPos.x) EnableXMovement();
        }

        if (_OnBossRoom) { _nextPos = BossPosition; _str = 2.5f; }

        transform.position = Vector3.Lerp(transform.position, _nextPos, _str * Time.deltaTime);
    }

    public void SetDeltaY(float y) {
        _nextDeltaY = y;
    }

    public void SetDeltaX(float x){
        _deltaX = x;
    }

    public void StopYMovement()
    {
        _holdY = true;
    }

    public void StopXMovement(){
        _holdX = true;
    }

    public void EnableXMovement(){
        _holdX = false;
    }

    public void EnableYMovement()
    {
        _holdY = false;
    }

    public void HoldTop()
    {
        _holdTop = true;
    }

    
}