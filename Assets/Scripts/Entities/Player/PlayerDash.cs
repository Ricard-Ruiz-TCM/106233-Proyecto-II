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

    private float _dashMaxTime;

    // Dash Controls
    [SerializeField]
    private float _dashDuration;
    private float _lastVelocity;

    // Layers para evitar coll
    private LayerMask _layers;
    [SerializeField]
    private float _dashSafeDistance;

    // FallSystem
    private PlayerFall _fall;

    // Unity
    void Awake(){ 
        LoadState();
        ////////////
        _isDashing = false;
        
        _dashStr = 400.0f;
        _dashMaxTime = _dashTime = 0.25f;

        _lastVelocity = 0.0f;
        _dashDuration = 0.0f;

        _dashSafeDistance = 0.35f;

        _layers = LayerMask.GetMask("Wall", "Ground", "WallFall", "Door", "FallingPlatform", "Object");

        _fall = GetComponent<PlayerFall>();
    }

    // PlayerDash.cs <Dash>
    public void StartDash(){
        _isDashing = true;
        _dashDuration = 0.0f;
        _lastVelocity = _body.velocity.x;
        if (_fall.IsFalling()) _dashTime = 0.2f;
        _body.AddForce(new Vector2(transform.right.x * _dashStr, 0.0f));
    }
    
    public void EndDash(){
        _body.velocity = new Vector2(_lastVelocity, _body.velocity.y);
        _isDashing = false;
        _dashDuration = 0.0f;
        _dashTime = _dashMaxTime;
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
        EndDash();
        ////////////////
        DisableSystem();
    }

    public void OnState(){
        if (!IsEnabled()) return;
        /////////////////////////

        // Comprobación para que no se meta en una pared
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, _dashSafeDistance, _layers);
        if (hit.collider != null) { _lastVelocity = 0.0f; EndDash(); }

        _dashDuration += Time.deltaTime;
        if (_dashDuration >= _dashTime) EndDash();
    }

}