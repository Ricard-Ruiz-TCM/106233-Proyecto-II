using System;
using UnityEngine;

public class PlayerFall : PlayerState, IHaveStates {

    // Observer para saber cuando cae
    public static event Action OnFalling;

    [SerializeField]
    private bool _isFalling;
    public bool IsFalling() { return _isFalling; }
    public bool CanFall() { return _jump.CanLand(); }
    public bool FallEnds() { return (Grounded() && !IsFalling()); }

    // Fallen
    [SerializeField]
    private bool _fallen;
    public bool HaveFallen() { return _fallen; }

    // Col. with Ground
    [SerializeField]
    private bool _onGround;
    public bool Grounded() { return _onGround; }
    public void OnGround(bool grounded) { _onGround = grounded; }

    // Col. with Wall
    [SerializeField]
    private bool _onWall;
    public bool OnTheWall() { return _onWall; }
    public void OnWall(bool wall) { _onWall = wall; }
    // Is Drawi Facing the Wall??
    [SerializeField]
    private bool _facingWall;
    public bool FacingWall() { return _facingWall; }
    public void OnFacingWall(bool wall) { _facingWall = wall; }

    [SerializeField]
    private float _gravity;

    // Jumping System
    private PlayerJump _jump;

    // SystemMachine
    private Player _player;

    // Unity
    void OnEnable(){
        BottomDetector.OnGround += OnGround;
        RightDetector.OnWallWhileFalling += OnWall;
        RightDetector.onFacingWall += OnFacingWall;
    }

    // Unity
    void OnDisable(){
        BottomDetector.OnGround -= OnGround;
        RightDetector.OnWallWhileFalling -= OnWall;
        RightDetector.onFacingWall -= OnFacingWall;
    }

    // Unity
    void Awake(){
        LoadState();
        ////////////
        _isFalling = false;
        _fallen = false;
        _onWall = false;
        _onGround = false;

        _gravity = 1.25f;

        _jump = GetComponent<PlayerJump>();
        _player = GetComponent<Player>();
    }

    // Unity
    void FixedUpdate(){
        if (!Grounded() && (!IsFalling() || !_jump.IsJumping())) _fallen = true;
                                                            else _fallen = false;
    }

    // PlayerFall.cs <Fall>
    private void SetFallGravity(){
        _body.gravityScale = _gravity;
    }

    public void StarFall(){
        SetFallGravity();
        _isFalling = true;
        if (!_player.LastState().Equals(PLAYER_STATE.PS_JUMP)) {
            _body.velocity = new Vector3(_body.velocity.x / 10.0f, _body.velocity.y);
        }
    }

    private void EndFall(){
        _isFalling = false;
    }

    private void ClampFallSpeed(){
        _body.velocity = new Vector2(_body.velocity.x, Mathf.Clamp(_body.velocity.y, -8.0f, 0.0f));
    }

    // IHaveStates
    public void OnEnterState(){
        EnableSystem();
        ///////////////
        StarFall();
        MusicPlayer.Instance.PlaySpecialFX("fall", 0.25f, 1.0f);
        _animator.SetBool("Fall", true);
    }

    public void OnExitState(){
        _animator.SetBool("Fall", false);
        MusicPlayer.Instance.StopFX("fall");
        ////////////////
        DisableSystem();
    }

    public void OnState(){
        if (!IsEnabled()) return;
        /////////////////////////
        if (Grounded()) EndFall();

        OnFalling?.Invoke();

        ClampFallSpeed();
        
        _jump.CheckBoost();
    }

}