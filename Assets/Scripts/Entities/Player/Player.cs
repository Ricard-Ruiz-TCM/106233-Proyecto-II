using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PLAYER_STATE {
    PS_NO_STATE,
    PS_IDDLE,
    PS_MOVE,
    PS_DASH,
    PS_JUMP,
    PS_FALL,
    PS_WALL_FALL,
    PS_DRAW,
    PS_PLACE,
    PS_ATTACK,
    PS_DIE
}

public class Player : Entity {

    // Observer para avisar cuando cambiamos de estado
    public static event Action<PLAYER_STATE> OnChangeState;
    // Observer para avisar cuando cambiamos de arma
    public static event Action<COMBAT_STATE> OnChangeWeapon;

    public static event Action OnRespawn;

    // Observer para ver caundo cambia la vida
    public static event Action OnHealthChange;
    // Obsever para ver caudno cambia la tinta
    public static event Action OnInkChange;

    [SerializeField]
    private int _ink;
    public int Ink() { return _ink; }
    public void UseInk(int amount) { _ink -= amount; _ink = Math.Max(_ink, 0); OnInkChange?.Invoke();}
    public bool HaveInk(int amount = 1) { return (_ink >= amount); }
    public void AddInk(int amount) { _ink += amount; _ink = Mathf.Min(_ink, _maxInk); OnInkChange?.Invoke(); }
    public void FillInk() { _ink = _maxInk; OnInkChange?.Invoke(); }
    private int _maxInk;
    public int MaxInk() { return _maxInk; }

    [SerializeField]
    private int _health;
    public float Health() { return _health; }
    [SerializeField]
    private bool _canCHealth;
    public bool CanChangeHealth() { return _canCHealth; }
    public void FillHealth() { _health = _maxHealth; OnHealthChange?.Invoke(); }
    private int _maxHealth;
    public float MaxHealth() { return _maxHealth; }

    // CheckPoint 
    private Vector2 _respawnPoint;

    [SerializeField]
    private PLAYER_STATE _currentState;
    public PLAYER_STATE State() { return _currentState; }
    // Behabiour de los estados
    private IHaveStates _currentBehavriour;
    public IHaveStates CurrentStateBehaviour() { return _currentBehavriour; }

    [SerializeReference]
    private PLAYER_STATE _lastState;
    public PLAYER_STATE LastState() { return _lastState; }

    private Attack _spikeAttack;

    [SerializeField]
    private bool _canAttack;
    private bool CanAttack() { return (_canAttack && _input.MainAction()); }
    private void EnableAttack() { _canAttack = true; }
    private void DisableAttack() { _canAttack = false; }

    [SerializeField]
    private bool _canJump;
    private bool CanJump() { return (_canJump && _input.Jump()); }
    private void EnableJump() { _canJump = true; }
    private void DisableJump() { _canJump = false; }

    // Player Behaviour
    private PlayerIddle _iddle;
    private PlayerDash _dash;
    private PlayerMovement _movement;
    private PlayerJump _jump;
    private PlayerFall _fall;
    private PlayerWallFall _wallFall;
    private PlayerDrawing _drawing;
    private PlayerPlacing _placing;
    private PlayerCombat _combat;
    private PlayerDie _die;

    [SerializeField]
    private GameObject _NoInk;

    // Input
    private PlayerInput _input;

    // sprite
    private SpriteRenderer _sprite;

    public float _alpha;

    void Start(){
        _ink = 50;
        _maxInk = _ink;

        _health = 100;
        _maxHealth = _health;

        _canCHealth = true;
        _alpha = 1.0f;

        _canAttack = true;

        _currentState = PLAYER_STATE.PS_NO_STATE;

        _iddle = GetComponent<PlayerIddle>();
        _dash = GetComponent<PlayerDash>();
        _movement = GetComponent<PlayerMovement>();
        _jump = GetComponent<PlayerJump>();
        _fall = GetComponent<PlayerFall>();
        _wallFall = GetComponent<PlayerWallFall>();
        _drawing = GetComponent<PlayerDrawing>();
        _placing = GetComponent<PlayerPlacing>();
        _die = GetComponent<PlayerDie>();
        _combat = GetComponent<PlayerCombat>();

        _input = GetComponent<PlayerInput>();

        _NoInk = Resources.Load<GameObject>("Prefabs/NoInk");

        _currentBehavriour = _iddle;

        _sprite = GetComponent<SpriteRenderer>();

        _spikeAttack = Resources.Load<Attack>("ScritpableObjects/SpikeAttack");

        ChangeState(PLAYER_STATE.PS_IDDLE);

    }

    void Update(){

        // "Update" del estado actual
        CurrentStateBehaviour().OnState();

        // Controlador para cambios de estado
        switch (State()){
            case PLAYER_STATE.PS_IDDLE:
                /* TO: PS_FALL */
                if (_fall.HaveFallen()) ChangeState(PLAYER_STATE.PS_FALL);
                /* TO: PS_DASH */ 
                else if (_dash.CanDash()) ChangeState(PLAYER_STATE.PS_DASH);
                /* TO: PS_JUMP */ 
                else if (CanJump()) ChangeState(PLAYER_STATE.PS_JUMP);
                /* TO: PS_ATTACK */
                else if (CanAttack()) ChangeState(PLAYER_STATE.PS_ATTACK);
                /* TO: PS_MOVE */ 
                else if ((_input.Left() ^ _input.Right())) ChangeState(PLAYER_STATE.PS_MOVE);
                /* TO: PS_DRAW */ 
                else if (_drawing.IsEnabled()) ChangeState(PLAYER_STATE.PS_DRAW);
                // EXtra
                else {
                    _movement.ApplyFriccion();
                    _movement.ApplyRotacion();
                    if (!_input.MainAction()) EnableAttack();
                    if (!_input.Jump()) EnableJump();
                }
                break;
            case PLAYER_STATE.PS_MOVE:
                /* TO: PS_DRAW */ 
                if (_drawing.IsEnabled()) ChangeState(PLAYER_STATE.PS_DRAW);
                /* to: PS_DASH */
                else if (_dash.CanDash()) ChangeState(PLAYER_STATE.PS_DASH);
                /* TO: PS_JUMP */
                else if (CanJump()) ChangeState(PLAYER_STATE.PS_JUMP);
                /* TO: PS_FALL */
                else if (_fall.HaveFallen()) ChangeState(PLAYER_STATE.PS_FALL);
                /* TO: PS_IDDLE */
                else if (!(_input.Left() ^ _input.Right())) ChangeState(PLAYER_STATE.PS_IDDLE);
                /* TO: PS_ATTACK */
                else if (CanAttack()) ChangeState(PLAYER_STATE.PS_ATTACK);
                // EXtra
                else {
                    _movement.ApplyFriccion();
                    _movement.ApplyRotacion();
                    if (!_input.MainAction()) EnableAttack();
                    if (!_input.Jump()) EnableJump();
                }
                break;
            case PLAYER_STATE.PS_DASH:
                /* TO: PS_IDDLE */
                if (_dash.DashEnds()) ChangeState(PLAYER_STATE.PS_IDDLE);
                // Extra
                else {

                }
                break;
            case PLAYER_STATE.PS_JUMP:
                /* TO: PS_DASH */ 
                if (_dash.CanDash()) ChangeState(PLAYER_STATE.PS_DASH);
                /* TO: PS_FALL */
                else if (_jump.JumpEnds() || !_input.Jump()) ChangeState(PLAYER_STATE.PS_FALL);
                // Extra
                else {
                    _movement.ApplyRotacion();
                }
                break;
            case PLAYER_STATE.PS_FALL:
                /* TO: PS_DASH */
                if (_dash.CanDash()) ChangeState(PLAYER_STATE.PS_DASH);
                /* TO : JUMP */
                else if ((CanJump()) && (_fall.CanCoyoteJump())) ChangeState(PLAYER_STATE.PS_JUMP);
                /* TO: PS_IDDLE */
                else if (_fall.Grounded()) ChangeState(PLAYER_STATE.PS_IDDLE);
                /* TO: PS_WALL_FALL */
                else if (_fall.OnTheWall()) ChangeState(PLAYER_STATE.PS_WALL_FALL);
                // Extra
                else
                {
                    if (!_input.Jump()) EnableJump();
                    _movement.ApplyRotacion();
                }
                break;
            case PLAYER_STATE.PS_DRAW:
                /* TO: PS_IDDLE */ 
                if (!_drawing.IsEnabled()) ChangeState(PLAYER_STATE.PS_IDDLE);
                /* TO: PS_PLACE */
                else if (_drawing.IsTemplateCompleted()) ChangeState(PLAYER_STATE.PS_PLACE);
                // Extra
                else {

                }
                break;
            case PLAYER_STATE.PS_PLACE:
                /* TO: PS_IDDLE */ 
                if (_placing.IsPlaced()) ChangeState(PLAYER_STATE.PS_IDDLE); 
                // Extra
                else {

                }
                break;
            case PLAYER_STATE.PS_ATTACK:
                /* TO: PS_IDDLE */ 
                if (_combat.AttackEnds()) ChangeState(PLAYER_STATE.PS_IDDLE);
                // Extra
                else {

                }
                break;
            case PLAYER_STATE.PS_WALL_FALL:
                /* TO: PS_IDDLE */
                if (_fall.Grounded()) ChangeState(PLAYER_STATE.PS_IDDLE);
                /* TO: PS_FALL */
                else if (_fall.IsFalling() && !_fall.OnTheWall()) ChangeState(PLAYER_STATE.PS_FALL);
                /* TO: PS_JUMP */
                else if (CanJump()) ChangeState(PLAYER_STATE.PS_JUMP);
                // Extra
                else {
                    if (!_input.Jump()) EnableJump();
                }
                break;
            case PLAYER_STATE.PS_DIE:
                if (Camera.main.gameObject.GetComponent<CameraMovement>().OnBoos) GameManager.Instance.Fade();
                break;
            default: break;
        }

        if (!_dash.IsDashing()) _dash.DisableSystem();

    }

    private void FixedUpdate() {
        if (!_canCHealth) _alpha = Mathf.Cos(Time.realtimeSinceStartup * 10.0f) + 0.5f;
        _alpha = Mathf.Clamp(_alpha, 0.25f, 1.0f);
        if (State().Equals(PLAYER_STATE.PS_DIE)) _alpha = 0.5f;
        _sprite.color = new Color(1.0f, 1.0f, 1.0f, _alpha);
    }

    public void ToggleDrawing(){
        if (HaveInk(10)) {
            if (GameManager.Instance.REAL_PROGRESSION > 0) _drawing.ToggleSystem();
        } else {
            Instantiate(_NoInk, transform.position + new Vector3(-0.6f, 0.6f, 0.0f), Quaternion.identity, transform);
        }
    }

    public void SwapTools(){
        if (_drawing.IsEnabled()) _drawing.SwapTool();
        else {
            _combat.ChangeState();
            OnChangeWeapon?.Invoke(_combat.CombatState());
        }
    }

    public void DashTime(){
        _dash.EnableSystem();
    }

    public void TryFallGround(){
        if ((!_drawing.IsDrawing()) && (!_placing.IsEnabled())) _movement.TryFall();
    }

    public void NextTemplate(){
        if (_drawing.IsEnabled()) {
            if (GameManager.Instance.REAL_PROGRESSION > 1) {
                GameObject.FindObjectOfType<TemplateHUD>().NextTemplate();
            }
        }
    }

    public void LastTemplate(){
        if (_drawing.IsEnabled()) {
            if (GameManager.Instance.REAL_PROGRESSION > 1) {
                GameObject.FindObjectOfType<TemplateHUD>().LastTemplate();
            }
        }
    }

    public void SaveCheckPoint(Vector2 pos) {
        _respawnPoint = pos;
        FillInk(); FillHealth();
    }

    public bool StopDrawing(){
        if (_drawing.IsEnabled()){
            if (_drawing.CanStop()) ToggleDrawing();
            return true;
        }
        return false;
    }
    public bool StopPlacing(){
        if (_placing.IsEnabled()){
            _placing.StopPlacing();
            return true;
        }
        return false;
    }

    public void TakeDamage(int amount, DEATH_CAUSE source, bool die) {
        _health -= amount;
        IJustTakeDamage(source);
        OnHealthChange?.Invoke();
    }

    public void TakeDamage(int amount, DEATH_CAUSE source = DEATH_CAUSE.D_DAMAGE) { 
        if (CanChangeHealth()) {
            _health -= amount; 
            IJustTakeDamage(source);
            OnHealthChange?.Invoke(); 
        } 
    }

    private void IJustTakeDamage(DEATH_CAUSE cause){
        _die.SetDeathCause(cause);
        _movement.PushBack();
        _canCHealth = false;
        Invencible();
        CheckDeath();
    }

    private void Invencible() {
        Invoke("NoInvencible", 1.75f);
    }

    private void NoInvencible() {
        _canCHealth = true;
        _alpha = 1.0f;
        GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    private void CheckDeath(){
        if (_health <= 0.0f) {
            ChangeState(PLAYER_STATE.PS_DIE);
            _alpha = 0.5f;
        } else {
            ParticleInstancer.Instance.StartParticles("Particulasdedano", transform);
        }
    }

    public void Respawn() {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        this.transform.position = _respawnPoint;
        _die.SetDeathCause(DEATH_CAUSE.D_DAMAGE);
        FillHealth();
        FillInk();
        _alpha = 1.0f;
        Camera.main.transform.GetComponent<CameraMovement>().EnableYMovement();
        ChangeState(PLAYER_STATE.PS_IDDLE);
        OnRespawn?.Invoke();
    }

    // State Machine Change and Check Methods
    public void ChangeState(PLAYER_STATE next){
        if (State().Equals(next)) return;

        DisableAttack();
        DisableJump();
        CurrentStateBehaviour().OnExitState();
        SoftChangeState(next);
        CurrentStateBehaviour().OnEnterState();
        
    }

    private void SoftChangeState(PLAYER_STATE next) {

        _lastState = _currentState;
        _currentState = next;

        switch (State()) {
            case PLAYER_STATE.PS_IDDLE: _currentBehavriour = _iddle; break;
            case PLAYER_STATE.PS_MOVE: _currentBehavriour = _movement; break;
            case PLAYER_STATE.PS_DASH: _currentBehavriour = _dash; break;
            case PLAYER_STATE.PS_JUMP: _currentBehavriour = _jump; break;
            case PLAYER_STATE.PS_FALL: _currentBehavriour = _fall; break;
            case PLAYER_STATE.PS_DRAW: _currentBehavriour = _drawing; break;
            case PLAYER_STATE.PS_PLACE: _currentBehavriour = _placing; break;
            case PLAYER_STATE.PS_ATTACK: _currentBehavriour = _combat; break;
            case PLAYER_STATE.PS_DIE: _currentBehavriour = _die; break;
            case PLAYER_STATE.PS_WALL_FALL: _currentBehavriour = _wallFall; break;

            default: break;
        }

        OnChangeState?.Invoke(State());
    }

    // ------------------------------

    public void Pause(){
        Debug.Log("PAUSE TIME   ");
    }

}
