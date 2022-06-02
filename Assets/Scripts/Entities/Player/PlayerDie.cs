using UnityEngine;

public enum DEATH_CAUSE {
    D_DAMAGE, D_FALL
}

public class PlayerDie : PlayerState, IHaveStates {

    // Death Attributes
    private string _cause;
    private DEATH_CAUSE _dCause;

    // Respan Time
    [SerializeField]
    private float _respawnTime;

    private GameObject _deathTriggerAnim;

    // PlayerStateMachine
    private Player _player;

    // Unity
    void Awake(){ 
        LoadState();
        ////////////
        _cause = "";
        _dCause = DEATH_CAUSE.D_DAMAGE;
        _respawnTime = 1.0f;

        _player = GetComponent<Player>();
    }

    // Unity
    void Start(){
        _deathTriggerAnim = Resources.Load<GameObject>("Prefabs/ERASE_ANIM");
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
        switch (_dCause){
            case DEATH_CAUSE.D_DAMAGE: Invoke("InstantiateHand", 1.0f); break;
            default: break;
        }
        _animator.SetBool("Deathx" + _cause, true);
    }

    public void OnExitState(){
        _animator.SetBool("DeathxFall", false);
        _animator.SetBool("DeathxDamage", false);
        ////////////////
        DisableSystem();
    }

    public void OnState(){
        if (!IsEnabled()) return;
        /////////////////////////

        _respawnTime -= Time.deltaTime;
        if (_respawnTime <= 0.0f) _player.Respawn();
    }

}