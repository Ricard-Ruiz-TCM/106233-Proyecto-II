using UnityEngine;

public class PlayerDash : PlayerState, IHaveStates {

    [SerializeField]
    private bool _isDashing;
    public bool IsDashing() { return _isDashing; }
    public bool CanDash() { return !_isDashing && IsEnabled(); }
    public bool DashEnds() { return !IsDashing(); }

    // Dash Attributes
    [SerializeField]
    private float _dashStr;
    [SerializeField]
    private float _dashTime;

    // Dash Controls
    private float _dashDuration;
    private float _lastVelocity;

    // Unity
    void Awake(){ 
        LoadState();
        ////////////
        _isDashing = false;
        
        _dashStr = 500.0f;
        _dashTime = 0.5f;

        _lastVelocity = 0.0f;
        _dashDuration = 0.0f;
    }

    // PlayerDash.cs <Dash>
    public void StartDash(){
        _isDashing = true;
        _dashDuration = 0.0f;
        _lastVelocity = _body.velocity.x;
        _body.AddForce(new Vector2(transform.right.x * _dashStr, 0.0f));
    }
    
    public void EndDash(){
        _body.velocity = new Vector2(_lastVelocity, _body.velocity.y);
        _isDashing = false;
    }

    // IHaveStates
    public void OnEnterState(){
        EnableSystem();
        ///////////////
        StartDash();
        _animator.SetBool("Dash", true);
    }

    public void OnExitState(){
        _animator.SetBool("Dash", false);
        ////////////////
        DisableSystem();
    }

    public void OnState(){
        if (!IsEnabled()) return;
        /////////////////////////
        _dashDuration += Time.deltaTime;
        if (_dashDuration >= _dashTime) EndDash();
    }

}