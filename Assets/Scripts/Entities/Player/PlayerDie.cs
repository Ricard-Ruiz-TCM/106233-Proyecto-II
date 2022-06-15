using System;
using UnityEngine;

public enum DEATH_CAUSE {
    D_DAMAGE, D_FALL
}

public class PlayerDie : PlayerState, IHaveStates {

    public static event Action OnDie;

    // Death Attributes
    private string _cause;
    private DEATH_CAUSE _dCause;

    // Respan Time
    [SerializeField]
    private float _respawnTime;

    private GameObject _deathTriggerAnim;

    // PlayerStateMachine
    private Player _player;

    [SerializeField]
    private float _time;
    [SerializeField]
    public float _speed;
    [SerializeField]
    public float _rack;

    // Unity
    void Awake(){ 
        LoadState();
        ////////////
        _cause = "";
        _dCause = DEATH_CAUSE.D_DAMAGE;
        _respawnTime = 1.0f;
        _time = 0.0f;

        _speed = 2.0f;
        _rack = 2.0f;

        _player = GetComponent<Player>();
    }

    // Unity
    void Start(){
        _deathTriggerAnim = Resources.Load<GameObject>("Prefabs/ERASE_ANIM");
    }

    void Update() {
        _time += Time.deltaTime;
    }

    // PlayerDie.cs <Die>
    public void SetDeathCause(DEATH_CAUSE cause){
        _dCause = cause;
        switch (_dCause){
            case DEATH_CAUSE.D_DAMAGE: _cause = "Damage"; _respawnTime = 2.5f; break;
            case DEATH_CAUSE.D_FALL: _cause = "Fall"; _respawnTime = 2.5f; break;
            default: break;
        }
    }

    private void InstantiateHand(){
        Vector2 pos = transform.position; pos.x += 1.1f;
        Instantiate(_deathTriggerAnim, pos, Quaternion.identity, transform);
    }

    // IHaveStates
    public void OnEnterState(){
        EnableSystem();
        ///////////////
        OnDie?.Invoke();
        switch (_dCause){
            case DEATH_CAUSE.D_DAMAGE: 
                Invoke("InstantiateHand", 1.0f);
                MusicPlayer.Instance.PlayFX("Player_Death_enemy_1",1.0f);
                //ParticleInstancer.Instance.StartParticles("RespawnLokuraParticula", this.transform);
                break;
            case DEATH_CAUSE.D_FALL:
                _body.velocity = Vector2.zero;
                _body.isKinematic = true;
                ParticleInstancer.Instance.StartParticles("Plumas_Particle", this.transform);
                MusicPlayer.Instance.PlayFX("Player_Death_Fall/Player_Death_Fall", 0.5f);
                break;
            default: break;
        }
        _animator.SetBool("Deathx" + _cause, true);
    }

    public void OnExitState(){
        _animator.SetBool("DeathxFall", false);
        _animator.SetBool("DeathxDamage", false);
        _body.isKinematic = false;
        ////////////////
        DisableSystem();
    }

    public void OnState(){
        if (!IsEnabled()) return;
        /////////////////////////

        if (_dCause.Equals(DEATH_CAUSE.D_FALL)) {
            transform.position += new Vector3((Mathf.Cos(_time * _rack) * _speed) * Time.deltaTime, +Time.deltaTime * 1.5f, 0.0f);
        }

        _respawnTime -= Time.deltaTime;
        if (_respawnTime <= 0.0f) _player.Respawn();
    }

}