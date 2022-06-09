using System;
using System.Collections;
using UnityEngine;

public class PlayerFall : PlayerState, IHaveStates {

    // Observer para saber cuando cae
    public static event Action OnFalling;
    public ParticleSystem effect;

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

    private bool _speedR;
    public bool SpeedReduced() { return _speedR; }

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
    private float _fallTime;
    public bool CanCoyoteJump() { return ((_fallTime < 0.2f) && (!_player.LastState().Equals(PLAYER_STATE.PS_JUMP))); }

    [SerializeField]
    private float _gravity;

    // Jumping System
    private PlayerJump _jump;

    // SystemMachine
    private Player _player;

    //Particles
    private int _particleId;

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
        if (!Grounded() && (!IsFalling() || !_jump.IsJumping()))
        {
            _fallen = true; 
            _fallTime += Time.deltaTime;
        }
        else
        {
            _fallen = false;
            _fallTime = 0.0f;
        }
    }

    // PlayerFall.cs <Fall>
    private void SetFallGravity(){
        _body.gravityScale = _gravity;
    }

    public void StarFall(){
        SetFallGravity();
        _isFalling = true;
        _particleId = ParticleInstancer.Instance.StartSpecialParticles("ParticlesCaidaaaa", transform);
        if (!_player.LastState().Equals(PLAYER_STATE.PS_JUMP)) {
            _body.velocity = new Vector3(_body.velocity.x / 2.5f, _body.velocity.y);
            _jump.DecideBoosts();
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
        _speedR = false;
        StarFall();
        //MusicPlayer.Instance.PlaySpecialFX("fall", 0.25f, 1.0f);
        _animator.SetBool("Fall", true);
    }

    public void OnExitState(){
        _animator.SetBool("Fall", false);
        MusicPlayer.Instance.StopFX("fall");
        ////////////////
        _speedR = false;
        DisableSystem();
        ParticleInstancer.Instance.StopParticles(_particleId);
    }

    public void OnState(){
        if (!IsEnabled()) return;
        /////////////////////////
        if (CanCoyoteJump()) return;
        if (Grounded()) EndFall();
        OnFalling?.Invoke();
        ClampFallSpeed();
        _jump.CheckBoost();
    }

}