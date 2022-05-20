using UnityEngine;

public enum DEATH_CAUSE {
    D_DAMAGE, D_FALL
}

public class PlayerDie : PlayerState, IHaveStates {

    // Death Attributes
    private string _cause;

    // Respan Time
    [SerializeField]
    private float _respawnTime;

    // PlayerStateMachine
    private Player _player;

    // Unity
    void Awake(){ 
        LoadState();
        ////////////
        _cause = "";
        _respawnTime = 0.0f;

        _player = GetComponent<Player>();
    }

    // PlayerDie.cs <Die>
    public void SetDeathCause(DEATH_CAUSE cause){
        switch (cause){
            case DEATH_CAUSE.D_DAMAGE: _cause = "Damage"; _respawnTime = 2.5f; break;
            case DEATH_CAUSE.D_FALL: _cause = "Fall"; _respawnTime = 1.5f; break;
            default: break;
        }
    }

    // IHaveStates
    public void OnEnterState(){
        EnableSystem();
        ///////////////
        _animator.SetBool("Deathx" + _cause, true);
    }

    public void OnExitState(){
        _animator.SetBool("Deathx" + _cause, false);
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