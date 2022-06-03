using System;
using UnityEngine;

public class PlayerJump : PlayerState, IHaveStates {

    // Observer para saber cuando salta
    public static event Action OnJump;

    [SerializeField]
    private bool _isJumping;
    public bool IsJumping() { return _isJumping; }

    // Jump Attributes
    [SerializeField]
    private float _jumpStr;
    
    // Jump Controls
    private bool _wallFree;
    private float _jumpTime;
    private float _lastVelY;
    public bool CanJump() { return !_isJumping; }
    public bool CanLand() { return (_jumpTime > 0.1f); }
    public bool JumpEnds() { return (!IsJumping()); }

    // Boost Attributes
    [SerializeField]
    private int _boost; // -1 (left) | 0 (none) | 1 (right) | 2 (both)
    [SerializeField]
    private float _boostStr;
    [SerializeField]
    private float _gravity;

    // Fall & WallFall Systems
    private PlayerFall _fall;

    // Unity
    private void OnEnable(){
        RightDetector.CanMoveRight += (bool wall) => { _wallFree = wall; };
    }
    private void OnDisable(){
        RightDetector.CanMoveRight -= (bool wall) => { _wallFree = wall; };
    }

    // Unity
    void Awake(){
        LoadState();
        ////////////
        _isJumping = false;

        _jumpStr = 200.0f;
        _jumpTime = 0.0f;
        _lastVelY = 0.0f;

        _boost = 0;
        _boostStr = 150.0f;
        _gravity = 0.5f;

        _fall = GetComponent<PlayerFall>();
    }

    // Unity
    void FixedUpdate(){
        if (!_fall.Grounded()) _jumpTime += Time.deltaTime;
                          else _jumpTime = 0.0f;
    }
    
    // PlayerJump.cs <Jump>
    private bool PeakReached(){
        bool reached = ((_lastVelY * _body.velocity.y) < 0);
        _lastVelY = _body.velocity.y;
        return (reached && CanLand());
    }

    private void SetJumpGravity(){
        _body.gravityScale = _gravity;
    }

    private void Jump(float force, float xforce = 0.0f) {
        SetJumpGravity();

        _boost = 2;
        _lastVelY = 0.0f;

        if (_body.velocity.x < -1.0f) _boost = 1;
        if (_body.velocity.x > 1.0f) _boost = -1;

        if (_boost != 2) force = force * 0.825f;
        _body.AddForce(new Vector2(xforce, force));

        _body.velocity = new Vector2(_body.velocity.x * 0.7f, _body.velocity.y);

        _isJumping = true;
    }

    private void StartJump(float force) {
        if (_fall.OnTheWall() && _fall.IsFalling()) {
            _body.velocity = Vector2.zero;
            float v = (force * 0.75f) * (_fall.FacingWall() ? -transform.right.x : transform.right.x);
            Jump(force * 0.9f, v);
            // Rotación
            if (v > 0.0f) transform.localEulerAngles = new Vector2(0.0f, 0.0f);
            if (v < 0.0f) transform.localEulerAngles = new Vector2(0.0f, 180.0f);
            _boost = 0;
        } else {
            Jump(force);
        }
        OnJump?.Invoke();
    }

    private void EndJump(){ 
        _isJumping = false; 
    }

    // PlayerJump.cs <Boost>
    private bool RightBoost(){ return ((_boost > 0) || (_boost == 2)); }
    private bool LeftBoost(){ return ((_boost < 0) || (_boost == 2)); }
    public void ResetBoost(){ _boost = 0; }

    public void Boost(int side){
        _body.AddForce(new Vector2(_boostStr * side, 0.0f));
        _boost = 0;
    }

    public void CheckBoost(){
        if (_fall.Grounded() || !_wallFree) return;

        if (Input().Left()) if (LeftBoost()) Boost(-1);
        if (Input().Right()) if (RightBoost()) Boost(1);
    }

    // IHaveStates
    public void OnEnterState(){
        EnableSystem();
        ///////////////
        StartJump(_jumpStr);
        MusicPlayer.Instance.PlayFX("jump", 0.4f);
        _animator.SetBool("Jump", true);
    }

    public void OnExitState(){
        _animator.SetBool("Jump", false);
        EndJump();
        ////////////////
        DisableSystem();
    }

    public void OnState() {
        if (!IsEnabled()) return;
        /////////////////////////
        if (PeakReached()) EndJump();
        if (IsJumping()) CheckBoost();
    }

}