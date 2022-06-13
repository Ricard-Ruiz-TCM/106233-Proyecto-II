using UnityEngine;

public class PlayerWallFall : PlayerState, IHaveStates {

    [SerializeField]
    private float _gravity;

    [SerializeField]
    private bool _facingWall;
    public bool FacingWall() { return _facingWall; }
    public void OnFacingWall(bool wall) { _facingWall = wall; }

    // Unity
    void OnEnable() {
        RightDetector.onFacingWall += OnFacingWall;
    }

    // Unity
    void OnDisable() {
        RightDetector.onFacingWall -= OnFacingWall;
    }

    void Awake(){
        LoadState();
        ////////////

        _gravity = 0.2f;
    }

    private void SetWallFallGravity(){
        _body.velocity = new Vector2(_body.velocity.x, _body.velocity.y / 10.0f);
        _body.gravityScale = _gravity;
    }

    // IHaveStates
    public void OnEnterState(){
        EnableSystem();
        ///////////////
        SetWallFallGravity();
        if (!FacingWall()) FaceWall();
        _animator.SetBool("FallWall", true);
    }

    public void FaceWall() {
        transform.Rotate(new Vector2(0.0f, 180.0f));
    }

    public void OnExitState(){
        _animator.SetBool("FallWall", false);
        ////////////////
        DisableSystem();
    }

    public void OnState(){
        if (!IsEnabled()) return;
        //////////////////////////
    }

}