using UnityEngine;

public class PlayerMovement : PlayerState, IHaveStates {

    [SerializeField]
    private bool _isMoving;
    public bool IsMoving() { return _isMoving; }

    [SerializeField]
    public float _speed;

    // Col. with Walls??
    [SerializeField]
    private bool _wallFree;
    private bool WallFree() { return _wallFree; }
    private void CanMoveRight(bool col) { _wallFree = col; }

    public bool NotFacingWall() { return WallFree(); }

    // Unity
    private void OnEnable(){
        RightDetector.CanMoveRight += CanMoveRight;
    }

    // Unity
    private void OnDisable(){
        RightDetector.CanMoveRight -= CanMoveRight;
    }

    // Unity
    void Awake() {
        LoadState();
        ////////////
        _isMoving = false;
        _speed = 5.0f;

        _wallFree = false;
    }

    // Unity
    public void ApplyRotacion() {
        // Rotación
        if (Input().Right() && !Input().Left()) transform.localEulerAngles = new Vector2(0.0f, 0.0f);
        if (Input().Left() && !Input().Right()) transform.localEulerAngles = new Vector2(0.0f, 180.0f);
    }

    // PlayerMovement.cs <Movement>
    public void ApplyFriccion(){
        _body.velocity = new Vector2(_body.velocity.x / 1.5f, _body.velocity.y);
    }

    public void PushBack() {
        _body.velocity = Vector2.zero;
        _body.AddForce(new Vector2(-(transform.right.x * 125.0f), 200.0f));
    }

    public void ExtraUpPush()
    {
        _body.AddForce(new Vector2(0.0f, 250.0f));
    }

    public void TryFall(){
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 1.0f, LayerMask.GetMask("Platform"));
        if (hit.collider != null) hit.collider.GetComponent<FadedGround>().DisableCol();
    }

    // PlayerMovement.cs <Audio>
    public void Steps() {
        MusicPlayer.Instance.PlayFX("Steps/step" + ((int)Random.Range(0, 6)).ToString(), 0.3f);
        Vector2 POS = transform.position; POS.y-=0.35f;
        ParticleInstancer.Instance.StartParticles("Pasos_Particulas", POS);

    }

    // IHaveStates
    public void OnEnterState(){
        EnableSystem();
        InvokeRepeating("Steps", 0.0f, 0.5f);
        ///////////////
        _animator.SetBool("Run", true);
    }

    public void OnExitState(){
        _animator.SetBool("Run", false);
        _isMoving = false;
        if (IsInvoking("Steps")) CancelInvoke("Steps");
        ////////////////
        DisableSystem();
    }

    public void OnState() {
        if (!IsEnabled()) return;
        /////////////////////////

        _isMoving = Input().Right() ^ Input().Left();

        if (IsMoving() && (WallFree())) _animator.SetBool("Run", true);
                                   else _animator.SetBool("Run", false);

        if (!WallFree()) return;
        if (Input().Right() && !Input().Left()){ _body.velocity = new Vector2(_speed, _body.velocity.y); } 
        if (!Input().Right() && Input().Left()){ _body.velocity = new Vector2(-_speed, _body.velocity.y); }

    }


}
