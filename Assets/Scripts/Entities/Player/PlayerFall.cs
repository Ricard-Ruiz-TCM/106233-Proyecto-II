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
    }

    private void EndFall(){
        _isFalling = false;
    }

    // IHaveStates
    public void OnEnterState(){
        EnableSystem();
        ///////////////
        StarFall();
        _animator.SetBool("Fall", true);
    }

    public void OnExitState(){
        _animator.SetBool("Fall", false);
        ////////////////
        DisableSystem();
    }

    public void OnState(){
        if (!IsEnabled()) return;
        /////////////////////////
        if (Grounded()) EndFall();

        OnFalling?.Invoke();
        
        _jump.CheckBoost();
    }

}